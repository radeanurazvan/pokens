import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BattlesService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${JSON.parse(localStorage.getItem('currentUserToken'))}`
    })
  };

  constructor(private http: HttpClient) { }

  public getCurrentBattle(): Observable<any> {
    return this.http.get<any>(`${environment.apiArenaUrl.read}/battles/me/current`, this.httpOptions);
  }

  public getMyBattles(): Observable<any> {
    return this.http.get<any>(`${environment.apiArenaUrl.read}/battles/me`, this.httpOptions);
  }

  public attack(battleId: string, abilityId: string): Observable<any> {
    return this.http.patch<any>(`${environment.apiArenaUrl.write}/trainers/me/battles/${battleId}`, { abilityId: abilityId }, this.httpOptions);
  }
}
