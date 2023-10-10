import { Component, OnInit } from '@angular/core';
import { InMemoryDatabase } from "brackets-memory-db";
import { BracketsManager } from "brackets-manager";
import { StageType } from 'brackets-model';

@Component({
  selector: 'app-tournament-details',
  templateUrl: './tournament-details.component.html',
  styleUrls: ['./tournament-details.component.css']
})
export class TournamentDetailsComponent implements OnInit {
  TOURNAMENT_ID = 0;
  storage = new InMemoryDatabase();
  manager = new BracketsManager(this.storage);

  constructor() { }

  ngOnInit(): void {
    window.bracketsViewer.addLocale('en', {
      common: {
        'group-name-winner-bracket': '{{stage.name}}',
      },
      'origin-hint': {
        'winner-bracket': 'WB {{round}}.{{position}}',
        'winner-bracket-semi-final': 'Semi final {{position}}',
        'winner-bracket-final': 'Grand final',
      },
    });

    // window.bracketsViewer.setParticipantImages(data.participant.map(participant => ({
    //   participantId: participant.id,
    //   imageUrl: 'https://github.githubassets.com/pinned-octocat.svg',
    // })));

    this.process(new Dataset()).then((data) => window.bracketsViewer.render(data));
      // { data },
      // {
      //   participantOriginPlacement: 'before',
      //   separatedChildCountLabel: true,
      //   showSlotsOrigin: true,
      //   highlightParticipantOnHover: true,
      // }));
  }

  async process(dataset: Dataset) {
    this.storage.setData({
      participant: dataset.roster.map((player) => ({
        ...player,
        tournament_id: this.TOURNAMENT_ID,
      })),
      stage: [],
      group: [],
      round: [],
      match: [],
      match_game: [],
    });

    await this.manager.create.stage({
      name: dataset.title,
      tournamentId: this.TOURNAMENT_ID,
      type: dataset.type,
      seeding: dataset.roster.map((player) => player.name),
      settings: {
        seedOrdering: ['inner_outer'],
        size: this.getNearestPowerOfTwo(dataset.roster.length),
      },
    });

    await this.manager.update.match({
      id: 0, // First match of winner bracket (round 1)
      opponent1: { score: 4, result: 'win' },
      opponent2: { score: 2 },
    });

    const data = await this.manager.get.stageData(0);

    return {
      stages: data.stage,
      matches: data.match,
      matchGames: data.match_game,
      participants: data.participant,
    };
  }

  getNearestPowerOfTwo(input: number): number {
    return Math.pow(2, Math.ceil(Math.log2(input)));
  }
}

export class Dataset {
  title = '8 competitor tournament';
  type: StageType = 'single_elimination';
  roster = [
    { id: 7, name: 'Team 1' },
    { id: 55, name: 'Team 2' },
    { id: 53, name: 'Team 3' },
    { id: 523, name: 'Team 4' },
    { id: 123, name: 'Team 5' },
    { id: 353, name: 'Team 6' },
    { id: 354, name: 'Team 7' },
    { id: 355, name: 'Team 8' },
  ];
};
