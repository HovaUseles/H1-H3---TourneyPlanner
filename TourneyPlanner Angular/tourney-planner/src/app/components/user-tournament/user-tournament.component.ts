import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Tournament } from 'src/app/interfaces/tournament';
import { TournamentService } from 'src/services/tournament.service';
import { UpsertTournamentComponent } from '../upsert-tournament/upsert-tournament.component';
import { Observable } from 'rxjs';
import { TournamentUpdateComponent } from 'src/app/components/tournament-update/tournament-update.component';
import { TournamentCreateComponent } from 'src/app/components/tournament-create/tournament-create.component';

@Component({
  selector: 'app-user-tournament',
  templateUrl: './user-tournament.component.html',
  styleUrls: ['./user-tournament.component.css']
})
export class UserTournamentComponent {
  loggedIn = false;
  tournaments: MatTableDataSource<Tournament> = new MatTableDataSource();
  displayedColumns: Array<string> = ["Name", "Start date", "Game type", "Tournament type", "Edit", "Delete"];

  constructor(private tournamentService: TournamentService, private matDialog: MatDialog) {
    this.tournamentService.getTournaments();

    this.tournamentService.tournaments$.subscribe(x => {
      this.tournaments.data = x;
      this.tournaments._updateChangeSubscription();
    });
  };

  CreateTournament() {
    this.matDialog.open(TournamentCreateComponent, {
      width: '50%',
      disableClose: true,
      data: null
    });
  };

  EditTournament(tournament: Tournament) {
    this.matDialog.open(TournamentUpdateComponent, {
      width: '50%',
      disableClose: true,
      data: tournament
    });
  };

  DeleteTournament(tournament: Tournament) {
    this.tournamentService.deleteTournament(tournament);
  };
}
