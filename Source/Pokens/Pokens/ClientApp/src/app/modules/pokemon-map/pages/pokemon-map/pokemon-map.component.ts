import { Component, OnInit } from '@angular/core';

import { Map } from 'ol';
import { OSM } from 'ol/source';
import { Tile as TileLayer } from 'ol/layer';
import { View } from 'ol';

import * as proj from 'ol/proj'
import { element } from 'protractor';

@Component({
  selector: 'app-pokemon-map',
  templateUrl: './pokemon-map.component.html',
  styleUrls: ['./pokemon-map.component.scss']
})
export class PokemonMapComponent implements OnInit {

  public map: Map;

  constructor() { }

  public ngOnInit() {
    this.getPosition().then(pos => {
      this.map = new Map({
        target: 'map',
        layers: [
          new TileLayer({
            source: new OSM()
          })
        ],
        view: new View({
          center: proj.fromLonLat([pos.lng, pos.lat]),
          zoom: 20,
          minZoom: 18,
          maxZoom: 20
        })
      });
    })

  }

  private getPosition(): Promise<any> {
    return new Promise((resolve, reject) => {

      navigator.geolocation.getCurrentPosition(resp => {

        resolve({ lng: resp.coords.longitude, lat: resp.coords.latitude });
      },
        err => {
          reject(err);
        });
    });

  }

  public get olMap(): Map {
    return this.map;
  }
}
