import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { Matchup } from 'src/app/interfaces/matchup';

@Injectable({
  providedIn: 'root'
})
export class MatchupService {
  matchDetails = (): Matchup => ({
    id: 0,
    teams: [],
    round: 0,
    nextMatchupId: 0
  });
  private matchDetailsSubject$: Subject<Matchup> = new BehaviorSubject<Matchup>(this.matchDetails());
  matchDetails$: Observable<Matchup> = this.matchDetailsSubject$.asObservable();

  constructor() { }

  setMatch(match: Matchup) {
    this.matchDetailsSubject$.next(match);
  };
};
