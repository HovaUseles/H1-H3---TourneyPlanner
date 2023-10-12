import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { CreateTournament } from 'src/app/interfaces/create-tournament';
import { Player } from 'src/app/interfaces/player';
import { Team } from 'src/app/interfaces/team';
import { TournamentService } from 'src/services/tournament.service';

@Component({
  selector: 'app-tournament-create',
  templateUrl: './tournament-create.component.html',
  styleUrls: ['./tournament-create.component.css']
})
export class TournamentCreateComponent {
  tournamentDetailsFormGroup = this.formBuilder.group({
    name: new FormControl('', [Validators.required]),
    gameType: new FormControl('', [Validators.required]),
    tournamentType: new FormControl('', [Validators.required]),
    startDate: new FormControl(null, [Validators.required]),
    randomize: new FormControl(true, [Validators.required])
  });

  playerData: FormGroup = this.formBuilder.group({ firstName: new FormControl(null, [Validators.required]), lastName: new FormControl('') });
  teamData: FormGroup = this.formBuilder.group({ name: new FormControl(null, [Validators.required]), players: this.formBuilder.array([this.playerData]) });
  // teamsFormGroup = this.formBuilder.group({
  //   teams: this.formBuilder.array([this.teamData])
  //   // this.formBuilder.array(this.teamData.map(
  //   //   teams => this.formBuilder.group(teams)
  //   // ))
  // });

  teamsFormGroup = this.formBuilder.group({
    name1: new FormControl(null, [Validators.required]),
    firstName1: new FormControl(null, [Validators.required]),
    lastName1: new FormControl(null, [Validators.required]),
    name2: new FormControl(null, [Validators.required]),
    firstName2: new FormControl(null, [Validators.required]),
    lastName2: new FormControl(null, [Validators.required]),
    name3: new FormControl(null, [Validators.required]),
    firstName3: new FormControl(null, [Validators.required]),
    lastName3: new FormControl(null, [Validators.required]),
    name4: new FormControl(null, [Validators.required]),
    firstName4: new FormControl(null, [Validators.required]),
    lastName4: new FormControl(null, [Validators.required]),
  })

  constructor(private formBuilder: FormBuilder, private tournamentService: TournamentService, private matDialogRef: MatDialogRef<TournamentCreateComponent>) { }

  getPlayers(index: number): FormArray {
    (<FormArray>(this.teamsFormGroup.get("teams." + index.toString() + '.players'))).push(this.formBuilder.control({ firstName: new FormControl("ss", [Validators.required]), lastName: new FormControl('') }));

    return (<FormArray>this.teamsFormGroup.get("teams." + index.toString() + '.players'));
  }

  addPlayerField(index: number) {
    (<FormArray>(<FormGroup>this.teamsFormGroup.get("teams." + index.toString())).get('players')).push(this.formBuilder.group({ firstName: new FormControl('mikkel', [Validators.required]), lastName: new FormControl('') }));
  }

  removePlayerField(parentIndex: number, childIndex: number): void {
    if (this.getPlayers(parentIndex).length > 1) {
      this.getPlayers(parentIndex).removeAt(childIndex);
    }
    else {
      this.getPlayers(parentIndex).patchValue([this.playerData]);
    }
  }

  resetPlayers(index: number): void {
    this.getPlayers(index).reset();
    this.getPlayers(index).clear();
    this.addPlayerField(index);
  }

  getTeams(): FormArray {
    console.log(this.teamsFormGroup.get('teams'))
    return this.teamsFormGroup.get('teams') as FormArray;
  }

  addTeamField() {
    (<FormArray>(this.teamsFormGroup.get("teams"))).push(this.formBuilder.group({ name: new FormControl(null, [Validators.required]), players: this.formBuilder.array([this.formBuilder.group({ firstName: new FormControl(null, [Validators.required]), lastName: new FormControl('') })]) }));
  }

  removeTeamField(index: number): void {
    if (this.getTeams().length > 1) this.getTeams().removeAt(index);
    else this.getTeams().patchValue([this.teamData]);
  }

  resetTeams(): void {
    this.teamsFormGroup.reset();
    this.getTeams().clear();
    this.addTeamField();
  }

  createTournament() {
    let players: Player[] = [
      {
        id: 0,
        firstName: this.teamsFormGroup.get("firstName1")?.value,
        lastName: this.teamsFormGroup.get("lastName1")?.value
      },
      {
        id: 0,
        firstName: this.teamsFormGroup.get("firstName2")?.value,
        lastName: this.teamsFormGroup.get("lastName2")?.value
      },
      {
        id: 0,
        firstName: this.teamsFormGroup.get("firstName3")?.value,
        lastName: this.teamsFormGroup.get("lastName3")?.value
      },
      {
        id: 0,
        firstName: this.teamsFormGroup.get("firstName4")?.value,
        lastName: this.teamsFormGroup.get("lastName4")?.value
      }];
    let teams: Team[] = [{id: 0, teamName: this.teamsFormGroup.get("name1")?.value, players: [players[0]]},
    {id: 0, teamName: this.teamsFormGroup.get("name2")?.value, players: [players[0]]},
    {id: 0, teamName: this.teamsFormGroup.get("name3")?.value, players: [players[0]]},
    {id: 0, teamName: this.teamsFormGroup.get("name4")?.value, players: [players[0]]}];
    // for (let index = 0; index < (<FormArray>(this.teamsFormGroup.get("teams"))).length; index++) {
    //   for (let idx = 0; idx < this.getPlayers(index).length; idx++) {
    //     let player: Player = {
    //       id: 0,
    //       firstName: this.teamsFormGroup.get("teams." + index + ".players" + idx + "firstName")?.value,
    //       lastName: this.teamsFormGroup.get("teams." + index + ".players" + idx + "firstName")?.value,
    //     }
    //     players.push(player)
    //   }
    //   teams.push({ id: 0, teamName: this.teamsFormGroup.get("teams." + index + ".name")?.value, players: players });
    // }

    let tournament: CreateTournament = {
      name: this.tournamentDetailsFormGroup.get("name")?.value,
      gameTypeId: 1,
      tournamentTypeId: 1,
      randomnizeTeams: this.tournamentDetailsFormGroup.get("randomize")?.value,
      startDate: this.tournamentDetailsFormGroup.get("startDate")?.value,
      teams: teams
    }

    console.log(tournament)

    this.tournamentService.createTournament(tournament);
    // this.matDialogRef.close(true);
  }

  closeDialog() {
    this.matDialogRef.close(false);
  }
}
