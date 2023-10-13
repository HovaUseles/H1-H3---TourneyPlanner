import { Injectable } from '@angular/core';
import { InputStage, Match, Participant, ParticipantResult, Result, StageSettings, StageType, Status } from 'brackets-model';
import { Matchup } from 'src/app/interfaces/matchup';
import { Team } from 'src/app/interfaces/team';
import { Tournament } from 'src/app/interfaces/tournament';
import { InMemoryDatabase } from "brackets-memory-db";
import { BracketsManager, StageCreator } from "brackets-manager";

@Injectable({
  providedIn: 'root'
})
export class BracketService {
  storage = new InMemoryDatabase();
  manager = new BracketsManager(this.storage);
  teams: Team[] = [];
  matches: Match[] = [];
  participants: Participant[] = [];

  constructor() { }

  async getStoredData(): Promise<any>  {
    return await this.manager.get.stageData(0);
  }

  async convertToBracketObjects(tournament: Tournament) {
    this.setTeamArray(tournament.id, tournament.matchups);

    this.setStorageData(tournament.id, tournament.name);

    for (let match of tournament.matchups) {
      if (match.teams.length > 0) {
        this.setMatchups(match);
      };
    };

    await this.setMatchResults(this.matches);
  }

  setTeamArray(tournamentId: number, matchups: Matchup[]) {
    for (let matchup of matchups) {
      for (let teamData of matchup.teams) {
        if (this.teams.filter((x) => { x.id == teamData.id }).length == 0) {
          this.teams.push({ id: teamData.id, teamName: teamData.teamName, score: 0, players: teamData.players });
          this.participants.push({ id: teamData.id, name: teamData.teamName, tournament_id: tournamentId })
        };
      };
    };
  };

  async setStorageData(tournamentId: number, tournamentName: string) {
    let stageType: StageType = 'single_elimination';
    let stageSettings: StageSettings = {balanceByes: true, grandFinal: 'simple', consolationFinal: true, size: this.getNearestPowerOfTwo(this.teams.length), seedOrdering: ['inner_outer']};
    let stage: InputStage = {tournamentId: tournamentId, type: stageType, name: tournamentName, number: 0, settings: stageSettings};

    this.storage.setData({
      participant: this.participants.map((team) => ({
        ...team,
        tournament_id: tournamentId,
      })),
      stage: [],
      group: [],
      round: [],
      match: this.matches,
      match_game: [],
    });

    await this.createStage(stage);
  };

  async createStage(stage: InputStage) {
    await this.manager.create.stage(stage);
  }

  setMatchups(match: Matchup) {
    if (match.teams.length > 0) {
      let result1: Result = 'draw';
      let result2: Result = 'draw';
      if (match.teams[0].score != undefined && match.teams[1].score != undefined) {
        if (match.teams[0].score! > match.teams[1].score) {
          result1 = 'win';
          result2 = 'loss';
        }
        else if (match.teams[0].score! < match.teams[1].score) {
          result1 = 'loss';
          result2 = 'win';
        };
      };

      let opponent1: ParticipantResult = {
        id: match.teams[0].id,
        score: match.teams[0].score == undefined ? 0 : match.teams[0].score,
        result: result1
      }

      let opponent2: ParticipantResult = {
        id: match.teams[1].id,
        score: match.teams[1].score == undefined ? 0 : match.teams[1].score,
        result: result2
      }

      let temp: Match = {
        id: match.id,
        status: Status.Completed,
        round_id: match.round,
        child_count: 0,
        group_id: 0,
        number: 0,
        stage_id: 0,
        opponent1: opponent1,
        opponent2: opponent2
      };

      this.matches.push(temp);
    };
  };

  async setMatchResults(matches: Match[]) {
    for (let match of matches) {
      await this.storage.update<Match>('match', Number.parseInt(match.id.toString()), match);
    }
  }

  getNearestPowerOfTwo(input: number): number {
    return Math.pow(2, Math.ceil(Math.log2(input)));
  }
}
