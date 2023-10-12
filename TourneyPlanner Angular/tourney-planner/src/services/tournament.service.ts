import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { CreateTournament } from 'src/app/interfaces/create-tournament';
import { Tournament } from 'src/app/interfaces/tournament';
import { GetUserId } from 'src/app/utility/get-user-id';
import { SetHttpHeader } from 'src/app/utility/set-http-header';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TournamentService {
  url: string = environment.config.apiUrl + "/api/Tournament";
  private tournaments: Array<Tournament> = [];
  private tournamentSubject$: Subject<Tournament[]> = new BehaviorSubject<Tournament[]>(this.tournaments);
  tournaments$: Observable<Tournament[]> = this.tournamentSubject$.asObservable();

  private myTournaments: Array<Tournament> = [];
  private myTournamentSubject$: Subject<Tournament[]> = new BehaviorSubject<Tournament[]>(this.myTournaments);
  myTournaments$: Observable<Tournament[]> = this.myTournamentSubject$.asObservable();

  tournamentDetails = (): Tournament => ({
    id: 0,
    name: '',
    gameType: '',
    matchups: [],
    startDate: new Date(),
    type: '',
  });
  private tournamentDetailsSubject$: Subject<Tournament> = new BehaviorSubject<Tournament>(this.tournamentDetails());
  tournamentDetails$: Observable<Tournament> = this.tournamentDetailsSubject$.asObservable();

  constructor(public httpClient: HttpClient, private setHttpHeader: SetHttpHeader, private getUserId: GetUserId) { }

  getTournaments() {
    const headers = this.setHttpHeader.setAuthHeader();
    const httpOptions = {
      headers: headers
    };
    this.httpClient.get<Tournament[]>(this.url, httpOptions).subscribe(x => {
      this.tournamentSubject$.next(x);
    });
  };

  getMyTournaments() {
    const headers = this.setHttpHeader.setAuthHeader();
    const httpOptions = {
      headers: headers
    };
    let id = this.getUserId.getUserId();
    this.httpClient.get<Tournament[]>(this.url + '/GetMyTournaments/' + id, httpOptions).subscribe(x => {
      console.log(x);
      this.myTournamentSubject$.next(x);
    });
  };

  getTournamentDetails(id: string) {
    const headers = this.setHttpHeader.setAuthHeader();
    const httpOptions = {
      headers: headers
    };
    this.httpClient.get<Tournament>(this.url + '/' + id, httpOptions).subscribe(x => {
      this.tournamentDetailsSubject$.next(x);
    });
  };

  createTournament(tournament: CreateTournament) {
    this.httpClient.post<Tournament[]>(this.url, tournament, { headers: this.setHttpHeader.setAuthHeader() },).subscribe(x => {
      this.tournamentSubject$.next(x);
    });
  };

  updateTournament(tournament: Tournament) {
    const headers = this.setHttpHeader.setAuthHeader();
    const httpOptions = {
      headers: headers
    };
    this.httpClient.put<Tournament[]>(this.url, tournament, httpOptions).subscribe(x => {
      this.tournamentSubject$.next(x);
    });
  };

  deleteTournament(id: number) {
    const headers = this.setHttpHeader.setAuthHeader();
    const httpOptions = {
      headers: headers
    };
    this.httpClient.delete<Tournament[]>(this.url + '/' + id, httpOptions).subscribe(x => {
      this.tournamentSubject$.next(x);
    });
  };
}
