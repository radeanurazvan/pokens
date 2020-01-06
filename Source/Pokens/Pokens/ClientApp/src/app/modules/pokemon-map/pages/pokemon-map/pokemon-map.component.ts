import { Component, OnInit } from '@angular/core';

import { Feature, Map, Overlay, View } from 'ol';
import { OSM, Vector as SourceVector } from 'ol/source';
import { Point } from 'ol/geom';
import { Tile as TileLayer, Vector as LayerVector } from 'ol/layer';

import * as proj from 'ol/proj'

import { LocationService } from '../../core/location.service';
import { MapPokemonsService } from '../../core/map-pokemons.service';
import { Subscription } from 'rxjs';
import { MapPokemonModel } from '../../models/map-pokemon.model';

@Component({
  selector: 'app-pokemon-map',
  templateUrl: './pokemon-map.component.html',
  styleUrls: ['./pokemon-map.component.scss']
})
export class PokemonMapComponent implements OnInit {

  private subscription = new Subscription();
  private loaded = false;
  private roulettePokemons: MapPokemonModel[];

  public map: Map;

  constructor(
    private locationService: LocationService,
    private mapPokemonsService: MapPokemonsService
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

      if (this.roulettePokemons) {
        this.setMarkers(pos.lng, pos.lat);
      }

      this.loaded = true;
    })

    this.subscription.add(this.mapPokemonsService.getRandomPokemons().subscribe((res: MapPokemonModel[]) => {
      this.roulettePokemons = res;
    }));
  }

  private setMarkers(long: number, lat: number): void {
    for (let i = 0; i < this.roulettePokemons.length; i++) {
      const coordinates = this.locationService.getRandomCoordinates(long, lat);
      let marker = new Feature({
        geometry: new Point(
          proj.fromLonLat([coordinates.longitudue, coordinates.latitude])
        )
      });

      marker.setId(this.roulettePokemons[i].id);

      marker.setProperties({
        name: this.roulettePokemons[i].name
      });

      let vectorSource = new SourceVector({
        features: [marker]
      });

      let layerVector = new LayerVector({
        source: vectorSource,
      });

      this.map.addLayer(layerVector);
    }

    this.setPokemonsToMarkers();
  }

  private setPokemonsToMarkers(): void {

    const tooltip = document.getElementById('pokemon-tooltip');
    const overlay = new Overlay({
      element: tooltip,
      offset: [10, 0]
    });
    this.map.addOverlay(overlay);

    this.map.on('singleclick', (evt) => {
      const feature = this.map.forEachFeatureAtPixel(evt.pixel, (feature) => {
        return feature;
      })

      tooltip.style.display = feature ? 'flex' : 'none';

      if (feature) {
        overlay.setPosition(evt.coordinate);
        tooltip.innerHTML = feature.get('name');
        tooltip['value'] = feature.getId();
      }

    });
  }

  public catch(): void {
    const tooltip = document.getElementById('pokemon-tooltip');
    const pokemonId = tooltip['value'];
    this.subscription.add(this.mapPokemonsService.catchPokemon(pokemonId).subscribe(data => {
      console.log(data);
    }));
  }

  public get isLoaded(): boolean {
    return this.loaded;
  }
}
