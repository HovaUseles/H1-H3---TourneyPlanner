import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { Team } from 'src/app/interfaces/team';

@Injectable({
  providedIn: 'root'
})
export class TeamService {
  private attendingTeams: Array<Team> = [];
  private attendingTeamsSubject$: Subject<Team[]> = new BehaviorSubject<Team[]>(this.attendingTeams);
  attendingTeams$: Observable<Team[]> = this.attendingTeamsSubject$.asObservable();

  private teamsPerMatch: Array<Team> = [];
  private teamsPerMatchSubject$: Subject<Team[]> = new BehaviorSubject<Team[]>(this.teamsPerMatch);
  teamsPerMatch$: Observable<Team[]> = this.teamsPerMatchSubject$.asObservable();

  constructor() { }

  SetMatchTeamList(teams: Team[]) {
    this.teamsPerMatchSubject$.next(teams);
  };

  setAttendingTeamList(teams: Team[]) {
    this.attendingTeamsSubject$.next(teams);
  };
};
