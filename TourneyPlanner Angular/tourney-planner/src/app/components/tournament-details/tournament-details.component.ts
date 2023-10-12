import { Component } from '@angular/core';
import { Participant, Result, Status, Match, ParticipantResult, Id, Stage, StageType, StageSettings, InputStage } from 'brackets-model';
import { TournamentService } from 'src/services/tournament.service';
import { Tournament } from 'src/app/interfaces/tournament';
import { Team } from 'src/app/interfaces/team';
import { Matchup } from 'src/app/interfaces/matchup';
import { Observable } from 'rxjs';
import { BracketService } from 'src/services/bracket.service';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { MatchupService } from 'src/services/matchup.service';
import { TeamService } from 'src/services/team.service';

@Component({
  selector: 'app-tournament-details',
  templateUrl: './tournament-details.component.html',
  styleUrls: ['./tournament-details.component.css']
})
export class TournamentDetailsComponent {
  displayedColumns: Array<string> = ["Id", "Round", "Home team", "Away team"];
  dataSource: MatTableDataSource<Matchup> = new MatTableDataSource();
  matchups: Matchup[] = [];
  teams: Team[] = [];
  tournamentName = "";

  constructor(private tournamentService: TournamentService, private bracketService: BracketService, private router: Router, private matchupService: MatchupService, private teamService: TeamService) {
    this.tournamentService.tournamentDetails$.subscribe(x => {
      this.dataSource.data = [];
      this.tournamentName = x.name;
      x.matchups.forEach(element => {
        if (element.teams.length > 0) {
          if (element.teams.filter((team) => {team.id == element.teams[0].id}).length == 0) {
            this.teams.push(element.teams[0]);
          }
          else if (element.teams.filter((team) => {team.id == element.teams[1].id}).length == 0) {
            this.teams.push(element.teams[1]);
          }
          this.matchups.push(element);
        }
      });
      this.teamService.setAttendingTeamList(this.teams);
      this.dataSource.data = this.matchups;
      this.dataSource._updateChangeSubscription();
    });
  };

  async bracketViewerConfiguration() {
    const data = await this.bracketService.getStoredData();
    window.bracketsViewer.setParticipantImages(this.teams.map(participant => ({
      participantId: participant.id,
      imageUrl: 'https://github.githubassets.com/pinned-octocat.svg',
    })));

    window.bracketsViewer.onMatchClicked = async (match: Match) => {
      console.log(match)
    };

    window.bracketsViewer.render({
      stages: data.stage,
      matches: data.match,
      matchGames: data.match_game,
      participants: data.participant
    }, {
      clear: true,
      participantOriginPlacement: 'before',
      separatedChildCountLabel: true,
      showSlotsOrigin: true,
      highlightParticipantOnHover: true,
    });
  };

  goToMatchPage(match: Matchup) {
    this.matchupService.setMatch(match);
    this.teamService.SetMatchTeamList(match.teams);
    this.router.navigateByUrl("/Match");
  };
};
