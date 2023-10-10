import { Component } from '@angular/core';
import { AuthenticationService } from 'src/services/authentication.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'TourneyPlanner';
  isLoggedIn$: Observable<boolean>;
  test = false;

  constructor(private authService: AuthenticationService, private router: Router, private matDialog: MatDialog) {
    this.isLoggedIn$ = this.authService.loggedIn$;
    this.authService.loggedIn$.subscribe(x => {
      this.test = !x;
    })
  };

  Logout() {
    this.authService.removeToken();
    this.router.navigateByUrl("/")
  };
};
