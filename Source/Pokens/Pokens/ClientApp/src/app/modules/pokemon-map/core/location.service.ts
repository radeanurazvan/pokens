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
    const longitude = ((Math.random() * (0.0015)) + long - 0.0015);
    const latitude = ((Math.random() * (0.0015)) + lat - 0.0015);
    return new CoordinatesModel(longitude, latitude);
  }
}
