import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class StarterPokemonsService {

  private pokemonEndPoint = 'https://localhost:44379/api/v1/pokemons/starters';

  private headerDict = {
    'Content-Type': 'application/json',
    'Accept': 'application/json',
    'Access-Control-Allow-Headers': 'Content-Type',
    'Access-Control-Allow-Origin': '*'
  }
  
  private requestOptions = {                                                                                                                                                                                 
    headers: new HttpHeaders(this.headerDict), 
  };

  constructor(
    private http: HttpClient
  ) { }

  public getPokemons() {
    return this.http.get(this.pokemonEndPoint);
  }
}
