import { Component, OnInit } from '@angular/core';

import { Map } from 'ol';
import { OSM } from 'ol/source';
import { Tile as TileLayer } from 'ol/layer';
import { View } from 'ol';

import * as proj from 'ol/proj'

import { LocationService } from '../core/location.service';

@Component({
  selector: 'app-pokemon-map',
  templateUrl: './pokemon-map.component.html',
  styleUrls: ['./pokemon-map.component.scss']
})
export class PokemonMapComponent implements OnInit {

  public map: Map;

  constructor(
    private locationService: LocationService
  ) { }

  public ngOnInit() {
    this.locationService.getPosition().then(pos => {
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


  public get olMap(): Map {
    return this.map;
  }
}
