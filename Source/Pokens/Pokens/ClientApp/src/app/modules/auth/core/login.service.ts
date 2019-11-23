import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { LoginModel } from '../models/login.model';
import { RegisterModel } from '../models/register.model';


@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient) { }

  public login(loginModel: LoginModel) {
    console.log('login!');
    return true;
  }

  public register(registerModel: RegisterModel) {
    console.log('registered!');
    return true;
  }
}
