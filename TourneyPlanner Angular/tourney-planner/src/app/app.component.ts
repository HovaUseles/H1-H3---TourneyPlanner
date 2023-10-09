import { Component } from '@angular/core';
import { AuthenticationService } from 'src/services/authentication.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'TourneyPlanner';
  isLoggedIn = false;

  constructor(private authService: AuthenticationService, private router: Router, private matDialog: MatDialog) {
    this.authService.loggedIn$.subscribe(x => {
      this.isLoggedIn = x;
    });
  };

  Logout() {
    this.authService.removeToken();
    this.router.navigateByUrl("/")
  };
};
