import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Matchup } from 'src/app/interfaces/matchup';
import { MatchupService } from 'src/services/matchup.service';

@Component({
  selector: 'app-match',
  templateUrl: './match.component.html',
  styleUrls: ['./match.component.css']
})
export class MatchComponent implements OnInit {
  match$: Observable<Matchup>;

  constructor(private matchupService: MatchupService) {
    this.match$ = this.matchupService.matchDetails$;
   }

  ngOnInit(): void {
  }


}
