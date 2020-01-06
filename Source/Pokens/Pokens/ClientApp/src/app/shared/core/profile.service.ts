import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  private endPointBase = environment.apiTrainingUrl + '/me/pokemons';

  constructor(private http: HttpClient) { }

  public getAllPokemons(): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${JSON.parse(localStorage.getItem('currentUserToken'))}`
      })
  };

    return this.http.get<any>(this.endPointBase, httpOptions);
  }
}
