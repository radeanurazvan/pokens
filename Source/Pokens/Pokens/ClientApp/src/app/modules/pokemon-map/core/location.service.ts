import { Injectable } from '@angular/core';
import { CoordinatesModel } from '../models/coordinates.model';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  constructor() { }

  public getPosition(): Promise<any> {
    return new Promise((resolve, reject) => {

      navigator.geolocation.getCurrentPosition(resp => {
        resolve({ lng: resp.coords.longitude, lat: resp.coords.latitude });
      },
        err => {
          reject(err);
        });
    });
  }

  public getRandomCoordinates(long: number, lat: number): CoordinatesModel {
    const longitude = ((Math.random() * (0.1)) + long - 0.1) * 1;
    const latitude = ((Math.random() * (0.1)) + lat - 0.1) * 1;
    return new CoordinatesModel(longitude, latitude);
  }
}
