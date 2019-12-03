import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { StarterPokemonModel } from '../models/starter-pokemon.model';

@Injectable({
  providedIn: 'root'
})
export class StarterPokemonsService {
  private pokemonEndPoint = 'https://localhost:44379/api/v1/pokemons/starters'; 

  constructor(private http: HttpClient) { }

  public getPokemons(): Observable<StarterPokemonModel[]> {
    return this.http.get<StarterPokemonModel[]>(this.pokemonEndPoint);
  }
}
