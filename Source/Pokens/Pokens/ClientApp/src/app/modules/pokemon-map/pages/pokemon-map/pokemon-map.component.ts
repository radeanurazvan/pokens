import { Component, OnInit } from '@angular/core';

import { Feature } from 'ol';
import { Map } from 'ol';
import { OSM, Vector as SourceVector } from 'ol/source';
import { Point } from 'ol/geom';
import { Tile as TileLayer, Vector as LayerVector } from 'ol/layer';
import { View } from 'ol';

import * as proj from 'ol/proj'

import { LocationService } from '../../core/location.service';
import { CoordinatesModel } from '../../models/coordinates.model';

@Component({
  selector: 'app-pokemon-map',
  templateUrl: './pokemon-map.component.html',
  styleUrls: ['./pokemon-map.component.scss']
})
export class PokemonMapComponent implements OnInit {

  public map: Map;

  private loaded = false;

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
          zoom: 19,
          minZoom: 18,
          maxZoom: 20
        })
      });
        this.setMarkers(pos.lng, pos.lat);

      this.loaded = true;
    })
  }

  private setMarkers(long: number, lat: number): void {
    for (let i = 0; i < 5; i++) {
      // const coordinates = this.locationService.getRandomCoordinates(long, lat);
      const coordinates = new CoordinatesModel(long - 0.1, lat - 0.1);
      let marker = new Feature({
        geometry: new Point(
          proj.fromLonLat([coordinates.longitudue, coordinates.latitude])
        ),
      });
      let vectorSource = new SourceVector({
        features: [marker]
      });

      this.map.addLayer(new LayerVector({
        source: vectorSource
      }));
    }
  }

  public get isLoaded(): boolean {
    return this.loaded;
  }
}
