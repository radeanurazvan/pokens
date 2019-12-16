import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MapPokemonModel } from '../models/map-pokemon.model';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MapPokemonsService {

  private pokemonEndPoint = environment.apiPokensUrl + '/roulette';

  constructor(
    private http: HttpClient
  ) { }
  
  public getRandomPokemons(): Observable<MapPokemonModel[]> {
    return this.http.get<MapPokemonModel[]>(this.pokemonEndPoint);
  }
}
