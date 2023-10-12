import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Tournament } from 'src/app/interfaces/tournament';
import { TournamentService } from 'src/services/tournament.service';
import { TournamentUpdateComponent } from 'src/app/components/tournament-update/tournament-update.component';
import { TournamentCreateComponent } from 'src/app/components/tournament-create/tournament-create.component';
import { Router } from '@angular/router';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-user-tournament',
  templateUrl: './user-tournament.component.html',
  styleUrls: ['./user-tournament.component.css']
})
export class UserTournamentComponent {
  loggedIn = false;
  tournaments: MatTableDataSource<Tournament> = new MatTableDataSource();
  displayedColumns: Array<string> = ["Name", "Start date", "Game type", "Tournament type"];

  constructor(private tournamentService: TournamentService, private matDialog: MatDialog, private router: Router, private userService: UserService) {
    this.tournamentService.getMyTournaments();

    this.tournamentService.myTournaments$.subscribe(x => {
      this.tournaments.data = x;
      this.tournaments._updateChangeSubscription();
    });
  };

  tournamentDetails(tournament: Tournament) {
    this.tournamentService.getTournamentDetails(tournament.id);
    this.userService.setTournamentUserId(tournament.id);
    this.router.navigateByUrl('/Tournament');
  };

  createTournament() {
    this.matDialog.open(TournamentCreateComponent, {
      width: '50%',
      disableClose: true
    });
  };

  editTournament(tournament: Tournament) {
    this.matDialog.open(TournamentUpdateComponent, {
      width: '50%',
      disableClose: true,
      data: tournament
    });
  };

  deleteTournament(tournament: Tournament) {
    this.tournamentService.deleteTournament(tournament.id);
  };
}
