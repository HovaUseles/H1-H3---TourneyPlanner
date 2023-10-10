import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Tournament } from 'src/app/interfaces/tournament';
import { TournamentService } from 'src/services/tournament.service';
import { UpsertTournamentComponent } from '../upsert-tournament/upsert-tournament.component';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tournament',
  templateUrl: './tournament.component.html',
  styleUrls: ['./tournament.component.css']
})
export class TournamentComponent {
  tournaments: MatTableDataSource<Tournament> = new MatTableDataSource();
  displayedColumns: Array<string> = ["Name", "Start date", "Game type", "Tournament type"];

  constructor(private tournamentService: TournamentService, private router: Router) {
    this.tournamentService.getTournaments();

    this.tournamentService.tournaments$.subscribe(x => {
      this.tournaments.data = x;
      this.tournaments._updateChangeSubscription();
    });
  };

  tournamentDetails(tournament: Tournament) {
    this.router.navigateByUrl('/Tournament', {state: {data: tournament.id}});
  };
};
