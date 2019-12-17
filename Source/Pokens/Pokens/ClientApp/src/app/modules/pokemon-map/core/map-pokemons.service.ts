import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MapPokemonModel } from '../models/map-pokemon.model';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MapPokemonsService {

  private pokemonRouletteEndPoint = environment.apiPokensUrl + '/roulette';
  private trainingCatchEndPoint = environment.apiTrainingUrl + '/me/catch-pokemon'

  constructor(
    private http: HttpClient
  ) { }

  public getRandomPokemons(): Observable<MapPokemonModel[]> {
    return this.http.get<MapPokemonModel[]>(this.pokemonRouletteEndPoint);
  }

  public catchPokemon(id: string | number): Observable<string | number> {
    return this.http.patch<string | number>(this.trainingCatchEndPoint, { pokemonId: id });
  }
}
