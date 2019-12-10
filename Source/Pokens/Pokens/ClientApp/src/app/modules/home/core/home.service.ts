import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  private endPointBase = environment.apiTrainersUrl + '/trainers/me/pokemons';

  constructor(private http: HttpClient) { }

  public getMyPokemons(): Observable<any> {
    return this.http.get<any>(this.endPointBase);
  }
}
