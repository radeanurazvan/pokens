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

  public getRandomCoordinates(minLong: number, maxLong: number, minLat: number, maxLat: number): CoordinatesModel {
    const longitude = Math.random() * (maxLong - minLong) + minLong;
    const latitude = Math.random() * (maxLat - minLat) + minLat;
    return new CoordinatesModel(longitude, latitude);
  }
}
