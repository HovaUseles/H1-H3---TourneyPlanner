import { Injectable } from '@angular/core';
import { Subject, BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private userIdSubject$: Subject<number> = new BehaviorSubject<number>(0);
  userId$: Observable<number> = this.userIdSubject$.asObservable();

  constructor() { }

  setTournamentUserId(id: number) {
    this.userIdSubject$.next(id);
  };
};
