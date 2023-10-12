import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { Auth } from 'src/app/interfaces/auth';
import { Token } from 'src/app/interfaces/token';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  url: string = environment.config.apiUrl + "/api/auth";
  registerEndpoint: string = "/register";
  loginEndpoint: string = "/login";

  private loggedInSubject$: Subject<boolean> = new BehaviorSubject<boolean>(false);
  loggedIn$: Observable<boolean> = this.loggedInSubject$.asObservable();

  constructor(private httpClient: HttpClient) { };

  getToken(login: Auth) {
    this.httpClient.post<Token>(this.url + this.loginEndpoint, login).subscribe(x => {
      this.checkResponse(x);
    });
  };

  removeToken() {
    sessionStorage.removeItem("token");
    this.loggedInSubject$.next(false);
  }

  register(login: Auth) {
    this.httpClient.post<Token>(this.url + this.registerEndpoint, login).subscribe(x => {
      this.checkResponse(x);
    });
  };

  checkResponse(x: Token) {
    if (x.tokenString != null || x.tokenString != undefined) {
      sessionStorage.setItem("token", x.tokenString);
      this.loggedInSubject$.next(true);
    }
  }
};
