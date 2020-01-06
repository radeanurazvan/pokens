import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MapPokemonModel } from '../models/map-pokemon.model';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MapPokemonsService {

  private pokemonRouletteEndPoint = environment.apiPokensUrl + '/roulette';
  private trainingCatchEndPoint = environment.apiTrainingUrl + '/me/pokemons'
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${JSON.parse(localStorage.getItem('currentUserToken'))}`,
    })
  }

  constructor(
    private http: HttpClient
  ) { }

  public getRandomPokemons(): Observable<MapPokemonModel[]> {
    return this.http.get<MapPokemonModel[]>(this.pokemonRouletteEndPoint, this.httpOptions);
  }

  public catchPokemon(id: string | number): Observable<string | number> {
    return this.http.patch<string | number>(this.trainingCatchEndPoint, { pokemonId: id }, this.httpOptions);
  }
}
