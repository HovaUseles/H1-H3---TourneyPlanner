// Modules
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { LayoutModule } from '@angular/cdk/layout';
import { HttpClientModule } from '@angular/common/http';
import { MatRadioModule } from '@angular/material/radio';

//Angular matrials
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button'
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatNativeDateModule, MatRippleModule } from '@angular/material/core';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressBarModule } from '@angular/material/progress-bar'
import { MatTooltipModule } from '@angular/material/tooltip'
import { MatCheckboxModule } from '@angular/material/checkbox'
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatStepperModule } from '@angular/material/stepper';
import { ClipboardModule } from '@angular/cdk/clipboard';
import { MatTreeModule } from '@angular/material/tree';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSliderModule } from '@angular/material/slider';

// Utility
import { SetHttpHeader } from './utility/set-http-header';
import { GetUserId } from './utility/get-user-id';

// Components
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { TournamentComponent } from './components/tournament/tournament.component';
import { TournamentDetailsComponent } from './components/tournament-details/tournament-details.component';
import { SettingsComponent } from './components/settings/settings.component';
import { TeamComponent } from './components/team/team.component';
import { UserTournamentComponent } from './components/user-tournament/user-tournament.component';
import { RegisterComponent } from './components/register/register.component';
import { TournamentUpdateComponent } from './components/tournament-update/tournament-update.component';
import { TournamentCreateComponent } from './components/tournament-create/tournament-create.component';
import { MatchComponent } from './components/match/match.component';
import { AsyncPipe } from '@angular/common';
import { UpdateMatchComponent } from './components/update-match/update-match.component';

declare global {
  interface Window {
    bracketsViewer?: any | undefined;
  }
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    TournamentComponent,
    TournamentDetailsComponent,
    SettingsComponent,
    TeamComponent,
    UserTournamentComponent,
    RegisterComponent,
    TournamentUpdateComponent,
    TournamentCreateComponent,
    MatchComponent,
    UpdateMatchComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatDialogModule,
    MatSortModule,
    MatPaginatorModule,
    MatTableModule,
    MatNativeDateModule,
    MatToolbarModule,
    MatProgressSpinnerModule,
    MatSelectModule,
    MatCheckboxModule,
    MatTooltipModule,
    MatProgressBarModule,
    MatMenuModule,
    MatSidenavModule,
    MatDatepickerModule,
    MatStepperModule,
    ClipboardModule,
    MatTreeModule,
    MatAutocompleteModule,
    LayoutModule,
    MatRadioModule,
    MatSlideToggleModule,
    MatSliderModule,
    MatCardModule,
    HttpClientModule,
  ],
  providers: [SetHttpHeader, GetUserId, AsyncPipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
