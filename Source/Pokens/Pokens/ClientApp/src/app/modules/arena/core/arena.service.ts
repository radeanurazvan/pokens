import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ArenaModel } from '../models/arena.model';

@Injectable({
  providedIn: 'root'
})
export class ArenaService {
  private arenaReadEndPoint = environment.apiArenaUrl.read;
  private arenaWriteEndPoint = environment.apiArenaUrl.write;
  private trainingEndPoint = environment.apiTrainingUrl;

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${JSON.parse(localStorage.getItem('currentUserToken'))}`
    })
  };

  constructor(private http: HttpClient) { }

  public getArenas(): Observable<ArenaModel[]> {
    return this.http.get<ArenaModel[]>(this.arenaReadEndPoint, this.httpOptions);
  }

  public joinArena(id: string): Observable<any> {
    return this.http.patch(`${this.arenaWriteEndPoint}/${id}/enrollments`, null, this.httpOptions);
  }

  public getArenaDetails(): Observable<ArenaModel> {
    return this.http.get<ArenaModel>(`${this.arenaReadEndPoint}/me`, this.httpOptions);
  }

  public getAllPokemons(ids: string[]): Observable<any> {
    let params = new HttpParams();
    params = params.append('trainersIds', ids.join(', '));
    const options = {
      headers: this.httpOptions.headers,
      params: params
    };
    return this.http.get<any>(`${this.trainingEndPoint}/pokemons`, options);
  }
}
