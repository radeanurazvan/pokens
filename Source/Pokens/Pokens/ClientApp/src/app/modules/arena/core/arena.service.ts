import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ArenaModel } from '../models/arena.model';

@Injectable({
  providedIn: 'root'
})
export class ArenaService {
  private arenaReadEndPoint = `${environment.apiArenaUrl.read}/arenas`;
  private arenaWriteEndPoint = `${environment.apiArenaUrl.write}/arenas`;
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

  public getReceivedChallenges(): Observable<any> {
    return this.http.get<ArenaModel>(`${environment.apiArenaUrl.read}/trainers/me/challenges/received`, this.httpOptions);
  }

  public challenge(arena: ArenaModel, challengedId: string, challengedPokemon: string, challengerPokemon: string): Observable<any> {
    const url = `${this.arenaWriteEndPoint}/${arena.id}/trainers/${challengedId}/challenges`;
    const body = {
      challengerPokemonId: challengerPokemon,
      challengedPokemonId: challengedPokemon
    };

    return this.http.patch(url, body, this.httpOptions);
  }

  public acceptChallenge(challenge): Observable<any> {
    const url = `${this.arenaWriteEndPoint}/${challenge.arenaId}/trainers/me/challenges/${challenge.id}`;
    return this.http.patch(url, {}, this.httpOptions);
  }

  public getAllPokemons(ids: string[]): Observable<any> {
    let params = new HttpParams();
    ids.forEach(x => params = params.append('trainersIds', x));
    const options = {
      headers: this.httpOptions.headers,
      params: params
    };
    return this.http.get<any>(`${this.trainingEndPoint}/pokemons`, options);
  }
}
