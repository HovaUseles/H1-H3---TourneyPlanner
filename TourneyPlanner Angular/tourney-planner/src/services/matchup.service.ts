import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { Matchup } from 'src/app/interfaces/matchup';
import { SetHttpHeader } from 'src/app/utility/set-http-header';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MatchupService {
  url: string = environment.config.apiUrl + "/api/Matchup";

  matchDetails = (): Matchup => ({
    id: 0,
    teams: [],
    round: 0,
    nextMatchupId: 0
  });
  private matchDetailsSubject$: Subject<Matchup> = new BehaviorSubject<Matchup>(this.matchDetails());
  matchDetails$: Observable<Matchup> = this.matchDetailsSubject$.asObservable();

  constructor(private setHttpHeader: SetHttpHeader, private httpClient: HttpClient) { }

  setMatch(match: Matchup) {
    this.matchDetailsSubject$.next(match);
  };

  updateMatch(match: Matchup) {
    const headers = this.setHttpHeader.setAuthHeader();
    const httpOptions = {
      headers: headers
    };

    this.httpClient.put<Matchup>(this.url + '/ChangeScore/' + match.id, match, httpOptions).subscribe(x => {
      this.matchDetailsSubject$.next(x);
    });
  };

  matchNotifications(decider: boolean, matchId: number) {
    const headers = this.setHttpHeader.setAuthHeader();
    const httpOptions = {
      headers: headers
    };
    if (decider == true) {
      this.httpClient.post(this.url + '/FollowMatchup', matchId, httpOptions).subscribe(x => {

      });
    }
    else {
      this.httpClient.post(this.url + '/UnfollowMatchup', matchId, httpOptions).subscribe(x => {

      });;
    };
  };
};
