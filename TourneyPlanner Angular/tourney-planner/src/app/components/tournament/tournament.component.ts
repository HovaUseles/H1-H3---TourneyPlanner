import { Component, OnDestroy, OnInit } from '@angular/core';
import { Tournament } from 'src/app/interfaces/tournament';
import { TournamentService } from 'src/services/tournament.service';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tournament',
  templateUrl: './tournament.component.html',
  styleUrls: ['./tournament.component.css']
})
export class TournamentComponent {
  tournaments: MatTableDataSource<Tournament> = new MatTableDataSource();
  displayedColumns: Array<string> = ["Name", "Start date", "Game type", "Tournament type", "Favorite"];

  constructor(private tournamentService: TournamentService, private router: Router) {
    this.tournamentService.getTournaments();

    this.tournamentService.tournaments$.subscribe(x => {
      this.tournaments.data = x;
      this.tournaments._updateChangeSubscription();
    });
  };

  tournamentDetails(tournament: Tournament) {
    this.tournamentService.getTournamentDetails(tournament.id.toString());
    this.router.navigateByUrl('/Tournament', {state: {data: tournament.id}});
  };
};
