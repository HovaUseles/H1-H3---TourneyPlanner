import { Component } from '@angular/core';
import { InMemoryDatabase } from "brackets-memory-db";
import { BracketsManager } from "brackets-manager";
import { Participant, Result, Status, Match, ParticipantResult, Id } from 'brackets-model';
import { TournamentService } from 'src/services/tournament.service';
import { Tournament } from 'src/app/interfaces/tournament';
import { Team } from 'src/app/interfaces/team';
import { Matchup } from 'src/app/interfaces/matchup';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-tournament-details',
  templateUrl: './tournament-details.component.html',
  styleUrls: ['./tournament-details.component.css']
})
export class TournamentDetailsComponent {
  storage = new InMemoryDatabase();
  manager = new BracketsManager(this.storage);
  tournament?: Tournament;
  teams: Team[] = [];
  matches: Match[] = [];
  participants: Participant[] = [];
  // tournament$: Observable<Tournament>;

  constructor(private tournamentService: TournamentService) {
    // this.tournament$ = this.tournamentService.tournamentDetails$;

    this.tournamentService.tournamentDetails$.subscribe(x => {
      if (x.matchups.length > 0) {
        this.tournament = x;
        this.participants = [];
        this.teams = [];
        this.convertToBracketObjects(this.tournament);
      };
    });
  };

  async convertToBracketObjects(tournament: Tournament) {
    this.setTeamArray(tournament.id, tournament.matchups);

    this.setStorageData(tournament.id)

    await this.createStage(tournament.name, tournament.id);

    for (let match of tournament.matchups) {
      if (match.teams.length > 0) {
        console.log(match.teams)
        this.setMatchups(match);
      };
    };

    await this.setMatchResults(this.matches);

    await this.bracketViewerConfiguration();
  }

  setTeamArray(tournamentId: number, matchups: Matchup[]) {
    for (let matchup of matchups) {

      for (let teamData of matchup.teams) {

        if (this.teams.filter((x) => { x.id == teamData.id }).length == 0) {
          this.teams.push({ id: teamData.id, teamName: teamData.teamName, players: teamData.players });
          this.participants.push({ id: teamData.id, name: teamData.teamName, tournament_id: tournamentId })
        };
      };
    };
  }

  setStorageData(tournamentId: number) {
    this.storage.setData({
      participant: this.participants.map((team) => ({
        ...team,
        tournament_id: tournamentId,
      })),
      stage: [],
      group: [],
      round: [],
      match: [],
      match_game: [],
    });
  };

  async createStage(tournamentName: string, tournamentId: number) {
    await this.manager.create.stage({
      name: tournamentName,
      tournamentId: tournamentId,
      type: 'single_elimination',

      seeding: this.teams.map((team) => team.teamName),
      settings: {
        seedOrdering: ['inner_outer'],
        size: this.getNearestPowerOfTwo(this.teams.length),
      },
    });
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

  // async setMatchResults(matches: Matchup[]) {
  //   for (let match of matches) {
  //     let result1: Result = 'draw';
  //     let result2: Result = 'draw';

  //     // console.log(match.teams[0].score);
  //     // console.log(match.teams[1].score);

  //     let opponent1: ParticipantResult = {
  //       id: match.teams[0].id,
  //       score: match.teams[0].score == undefined ? 0 : match.teams[0].score,
  //       result: result1
  //     }

  //     let opponent2: ParticipantResult = {
  //       id: match.teams[1].id,
  //       score: match.teams[1].score == undefined ? 0 : match.teams[1].score,
  //       result: result2
  //     }

  //     let temp: Match = {
  //       id: match.id,
  //       status: Status.Completed,
  //       round_id: match.round,
  //       child_count: 0,
  //       group_id: 0,
  //       number: 0,
  //       stage_id: 0,
  //       opponent1: opponent1,
  //       opponent2: opponent2
  //     };

  //     if (match.teams.length > 0) {
  //       if (match.teams[0].score != undefined && match.teams[1].score != undefined) {
  //         if (match.teams[0].score! > match.teams[1].score) {
  //           result1 = 'win';
  //           result2 = 'loss';
  //         }
  //         else if (match.teams[0].score! < match.teams[1].score) {
  //           result1 = 'loss';
  //           result2 = 'win';
  //         };
  //       };

  //       this.storage.update<Match>('match', match.id, temp);
  //     };
  //   };

  //   const data = await this.manager.get.stageData(0);

  //   console.log(data);
  // };

  async bracketViewerConfiguration() {
    window.bracketsViewer.addLocale('en', {
      // common: {
      //   'group-name-winner-bracket': '{{stage.name}}',
      // },
      // 'origin-hint': {
      //   'winner-bracket-semi-final': 'Semi final {{position}}',
      //   'winner-bracket-final': 'Grand final',
      // },
    });

    window.bracketsViewer.setParticipantImages(this.teams.map(participant => ({
      participantId: participant.id,
      imageUrl: 'https://github.githubassets.com/pinned-octocat.svg',
    })));

    const data = await this.manager.get.stageData(0);

    window.bracketsViewer.render({
      stages: data.stage,
      matches: data.match,
      matchGames: data.match_game,
      participants: data.participant
    }, {
      onMatchClick: () => { this.goToMatchPage() },
      // participantOriginPlacement: 'before',
      // separatedChildCountLabel: true,
      // showSlotsOrigin: true,
      highlightParticipantOnHover: true,
    })
  }

  getNearestPowerOfTwo(input: number): number {
    return Math.pow(2, Math.ceil(Math.log2(input)));
  }

  goToMatchPage() {
    console.log("test")
  }
}
