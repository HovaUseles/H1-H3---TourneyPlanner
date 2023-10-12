import { AsyncPipe } from '@angular/common';
import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { Matchup } from 'src/app/interfaces/matchup';
import { MatchupService } from 'src/services/matchup.service';
import { UpdateMatchComponent } from '../update-match/update-match.component';

@Component({
  selector: 'app-match',
  templateUrl: './match.component.html',
  styleUrls: ['./match.component.css']
})
export class MatchComponent {
  match$: Observable<Matchup>;
  notifications = false;
  id = 0;

  constructor(private matchupService: MatchupService, private asyncPipe: AsyncPipe, private matDialog: MatDialog) {
    this.match$ = this.matchupService.matchDetails$;
    this.id = this.asyncPipe.transform(this.match$)!.id;
  };

  matchNotifications() {
    this.notifications = !this.notifications;
    this.matchupService.matchNotifications(this.notifications, this.id);
  };

  updateMatch() {
    let match = this.asyncPipe.transform(this.match$);
    this.matDialog.open(UpdateMatchComponent, {
      data: match
    });
  };
};
