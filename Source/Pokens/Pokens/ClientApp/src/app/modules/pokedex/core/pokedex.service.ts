import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PokedexService {
  private endPointBase = environment.apiPokensUrl + '/all';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${JSON.parse(localStorage.getItem('currentUserToken'))}`,
    })
  }
  constructor(private http: HttpClient) { }
  
  public getAllPokemons(): Observable<any> {
    return this.http.get<any>(this.endPointBase, this.httpOptions);
  }
}
