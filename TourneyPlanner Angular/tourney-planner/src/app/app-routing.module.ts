import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TournamentComponent } from './components/tournament/tournament.component';
import { TournamentDetailsComponent } from './components/tournament-details/tournament-details.component';
import { TeamComponent } from './components/team/team.component';
import { SettingsComponent } from './components/settings/settings.component';
import { UserTournamentComponent } from './components/user-tournament/user-tournament.component';
import { LoginComponent } from './components/login/login.component';
import { MatchComponent } from './components/match/match.component';

const routes: Routes = [
  {
    path: '',
    component: TournamentComponent
  },
  {
    path: 'Tournament',
    component: TournamentDetailsComponent
  },
  {
    path: 'Match',
    component: MatchComponent
  },
  {
    path: 'Team',
    component: TeamComponent
  },
  {
    path: 'Login',
    component: LoginComponent
  },
  {
    path: 'Settings',
    component: SettingsComponent
  },
  {
    path: 'User/Tournaments',
    component: UserTournamentComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
