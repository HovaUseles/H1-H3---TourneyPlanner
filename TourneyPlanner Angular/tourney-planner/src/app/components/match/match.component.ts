import { AsyncPipe } from '@angular/common';
import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { Matchup } from 'src/app/interfaces/matchup';
import { MatchupService } from 'src/services/matchup.service';
import { UpdateMatchComponent } from '../update-match/update-match.component';
import { GetUserId } from 'src/app/utility/get-user-id';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-match',
  templateUrl: './match.component.html',
  styleUrls: ['./match.component.css']
})
export class MatchComponent {
  match$: Observable<Matchup>;
  userId$: Observable<number>;
  notifications = false;
  id = 0;
  userId = 0;

  constructor(private matchupService: MatchupService, private asyncPipe: AsyncPipe, private matDialog: MatDialog, private getUserId: GetUserId, private userService: UserService) {
    this.match$ = this.matchupService.matchDetails$;
    this.userId$ = this.userService.userId$;
    this.id = this.asyncPipe.transform(this.match$)!.id;
    this.userId = getUserId.getUserId();
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
