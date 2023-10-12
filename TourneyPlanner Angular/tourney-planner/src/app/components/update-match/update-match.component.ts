import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Matchup } from 'src/app/interfaces/matchup';
import { MatchupService } from 'src/services/matchup.service';

@Component({
  selector: 'app-update-match',
  templateUrl: './update-match.component.html',
  styleUrls: ['./update-match.component.css']
})
export class UpdateMatchComponent implements OnInit {
  homeTeamScore: FormControl = new FormControl([0, Validators.required]);
  awayTeamScore: FormControl = new FormControl([0, Validators.required]);

  match: Matchup = {
    id: 0,
    round: 0,
    teams: [],
    nextMatchupId: 0
  }

  constructor(@Inject(MAT_DIALOG_DATA) private data: Matchup, private matchupService: MatchupService) {
    this.match = this.data;
  }

  ngOnInit(): void {
  }

  submit() {
    this.match.teams[0].score = this.homeTeamScore.value;
    this.match.teams[1].score = this.awayTeamScore.value;

    // this.matchupService.
  }
}