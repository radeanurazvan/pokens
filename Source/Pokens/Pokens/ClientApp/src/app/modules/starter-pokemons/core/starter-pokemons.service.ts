import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

import { StarterPokemonModel } from '../models/starter-pokemon.model';


@Injectable({
  providedIn: 'root'
})
export class StarterPokemonsService {
  private pokemonEndPoint = environment.apiPokensUrl + '/pokemons/starters';

  constructor(private http: HttpClient) { }

  public getPokemons(): Observable<StarterPokemonModel[]> {
    return this.http.get<StarterPokemonModel[]>(this.pokemonEndPoint);
  }
}
