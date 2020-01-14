import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ArenaModel } from '../models/arena.model';

@Injectable({
  providedIn: 'root'
})
export class ArenaService {
  private arenaReadEndPoint = environment.apiArenaUrl.read;
  private arenaWriteEndPoint = environment.apiArenaUrl.write;

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${JSON.parse(localStorage.getItem('currentUserToken'))}`
    })
  }

  constructor(private http: HttpClient) { }

  public getArenas(): Observable<ArenaModel[]> {
    return this.http.get<ArenaModel[]>(this.arenaReadEndPoint);
  }

  public joinArena(id: string): Observable<any> {
    return this.http.patch(`${this.arenaWriteEndPoint}/${id}/enrollments`, this.httpOptions);
  }

  public getArenaDetails(): Observable<ArenaModel> {
    return this.http.get<ArenaModel>(`${this.arenaReadEndPoint}/me`);
  }
}
