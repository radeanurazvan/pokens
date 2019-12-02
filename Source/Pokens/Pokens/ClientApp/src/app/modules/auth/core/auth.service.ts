import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';

import { LoginModel } from '../models/login.model';
import { RegisterModel } from '../models/register.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private endPointBase = 'http://localhost:5000/api/v1/trainers';

  constructor(
    private http: HttpClient,
    private jwtHelperService: JwtHelperService) { }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem('currentUserToken');

    return !this.jwtHelperService.isTokenExpired(token);
  }

  public logout(): void {
    if (localStorage.getItem('currentUserToken')) {
      localStorage.removeItem('currentUserToken');
    }
  }

  public login(loginModel: LoginModel): Observable<any> {
    return this.http.post<LoginModel>(`${this.endPointBase}/token`, loginModel);
  }

  public register(registerModel: RegisterModel): Observable<any> {
    return this.http.post(`${this.endPointBase}`, registerModel);
  }
}
