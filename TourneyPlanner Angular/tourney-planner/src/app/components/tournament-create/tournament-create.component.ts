import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { CreateTournament } from 'src/app/interfaces/create-tournament';
import { Matchup } from 'src/app/interfaces/matchup';
import { Team } from 'src/app/interfaces/team';
import { Tournament } from 'src/app/interfaces/tournament';
import { TournamentService } from 'src/services/tournament.service';

@Component({
  selector: 'app-tournament-create',
  templateUrl: './tournament-create.component.html',
  styleUrls: ['./tournament-create.component.css']
})
export class TournamentCreateComponent implements OnInit {
  tournamentDetailsFormGroup: FormGroup = this.formBuilder.group({
    name: ['', Validators.required],
    gameType: ['', Validators.required],
    tournamentType: ['', Validators.required],
    startDate: [null, Validators.required],
    randomize: [true, [Validators.required]]
  });

  teamData = [{ name: '', players: [{ firstName: '', lastName: ''}] }];
  playerData = [{ firstName: '', lastName: '' }];
  teamsFormGroup: FormGroup = this.formBuilder.group({
    teams: this.formBuilder.array(this.teamData.map(
      teams => this.formBuilder.group(teams)
    )),
    child: this.formBuilder.group({
      players: this.formBuilder.array(this.playerData.map(
        players => this.formBuilder.group(players)
      ))
    })
  });

  // playersFormGroup: FormGroup = this.formBuilder.group({
  //   players: this.formBuilder.array(this.playerData.map(
  //     players => this.formBuilder.group(players)
  //   ))
  // });

  constructor(private formBuilder: FormBuilder, private tournamentService: TournamentService, private matDialogRef: MatDialogRef<TournamentCreateComponent>) {}

  ngOnInit() {
  }

  get players(): FormArray {
    return this.teamsFormGroup.get('child.players') as FormArray;
  }

  buildPlayers(players: {firstName: string; lastName: string;}[] = []) {
    return this.formBuilder.array(players.map(player => this.formBuilder.group(player)));
  }

  addPlayerField() {
    this.players.push(this.formBuilder.group({firstName: null, lastName: null}))
  }

  removePlayerField(index: number): void {
    if (this.players.length > 1) this.players.removeAt(index);
    else this.players.patchValue([{firstName: null, lastName: null}]);
  }

  resetPlayers(): void {
    // this.playersFormGroup.reset();
    this.players.clear();
    this.addPlayerField();
  }

  get teams(): FormArray {
    return this.teamsFormGroup.get('teams') as FormArray;
  }

  buildTeams(teams: {name: string;}[] = []) {
    return this.formBuilder.array(teams.map(team => this.formBuilder.group(team)));
  }

  addTeamField() {
    this.teams.push(this.formBuilder.group({name: null}))
  }

  removeTeamField(index: number): void {
    if (this.teams.length > 1) this.teams.removeAt(index);
    else this.teams.patchValue([{name: null}]);
  }

  resetTeams(): void {
    this.teamsFormGroup.reset();
    this.teams.clear();
    this.addTeamField();
  }

  createTournament() {
    for (let index = 0; index < this.teams.length; index++) {

    }

    // let teams: Team[] = {

    // }

    // let tournament: CreateTournament = {
    //   name: this.tournamentDetailsFormGroup.value.name,
    //   gameTypeId: 1,
    //   tournamentTypeId: 1,
    //   randomnizeTeams: true,
    //   startDate: new Date(),
    //   teams: teams
    // }

    // this.tournamentService.createTournament(tournament);
    this.matDialogRef.close(true);
  }

  closeDialog() {
    this.matDialogRef.close(false);
  }
}
