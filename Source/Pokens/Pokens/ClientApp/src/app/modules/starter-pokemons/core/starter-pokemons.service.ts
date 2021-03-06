import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

import { StarterPokemonModel } from '../models/starter-pokemon.model';


@Injectable({
  providedIn: 'root'
})
export class StarterPokemonsService {
  private pokemonEndPoint = environment.apiPokensUrl + '/starters';
  private trainingEndPoint = environment.apiTrainingUrl + '/me/starter';

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${JSON.parse(localStorage.getItem('currentUserToken'))}`
    })
  };

  constructor(private http: HttpClient) { }

  public getPokemons(): Observable<StarterPokemonModel[]> {
    return this.http.get<StarterPokemonModel[]>(this.pokemonEndPoint, this.httpOptions);
  }

  public setStarterPokemon(id: string): Observable<any[]> {
    return this.http.patch<any[]>(this.trainingEndPoint, {pokemonId: id}, this.httpOptions);
  }
}
