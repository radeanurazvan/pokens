import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private jwtHelperService: JwtHelperService) { }

  public getUserName(): string {
    const token = localStorage.getItem('currentUserToken');

    return this.jwtHelperService.decodeToken(token).TrainerName;
  }

  public getUserId(): string {
    const token = localStorage.getItem('currentUserToken');

    return this.jwtHelperService.decodeToken(token).TrainerId;
  }
}
