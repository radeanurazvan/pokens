import { Component, OnInit } from '@angular/core';

import { Feature, Map, Overlay, View } from 'ol';
import { OSM, Vector as SourceVector, ImageStatic } from 'ol/source';
import { Point } from 'ol/geom';
import { Tile as TileLayer, Vector as LayerVector } from 'ol/layer';
import { Icon, Style } from 'ol/style';

import { LocationService } from '../../core/location.service';
import { MapPokemonsService } from '../../core/map-pokemons.service';
import { Subscription } from 'rxjs';
import { MapPokemonModel } from '../../models/map-pokemon.model';
import BaseLayer from 'ol/layer/Base';
import { tap } from 'rxjs/operators';
import { ToastrService } from '../../../../shared/core/toastr.service';
import ImageLayer from 'ol/layer/Image';

@Component({
  selector: 'app-pokemon-map',
  templateUrl: './pokemon-map.component.html',
  styleUrls: ['./pokemon-map.component.scss']
})
export class PokemonMapComponent implements OnInit {

  private subscription = new Subscription();
  private loaded = false;
  private roulettePokemons: MapPokemonModel[];
  private initialPosition = [3070838.01062542, 5966683.426528152];

  public map: Map;

  constructor(
    private locationService: LocationService,
    private mapPokemonsService: MapPokemonsService,
    private toastrService: ToastrService
  ) { }

  public ngOnInit() {
    this.map = new Map({
      target: 'map',
      layers: [
        new TileLayer({
          source: new OSM()
        })
      ],
      view: new View({
        center: [this.initialPosition[0], this.initialPosition[1]],
        zoom: 18,
        minZoom: 18,
        maxZoom: 20
      })
    });

    this.loaded = true;


    this.subscription.add(this.mapPokemonsService.getRandomPokemons().subscribe((res: MapPokemonModel[]) => {
      this.addMapImage(this.initialPosition[0], this.initialPosition[1]);
      console.log(this.map.getView().calculateExtent(this.map.getSize()));
      this.roulettePokemons = res;
      if (this.roulettePokemons) {
        this.setMarkers();
      }
    }));
  }

  private addMapImage(long: number, lat: number): void {
    const mapExtent = this.map.getView().calculateExtent(this.map.getSize());
    this.map.setView(new View({
      center: [long, lat],
      zoom: 18,
      minZoom: 18,
      maxZoom: 20,
      extent: mapExtent
    }));

    const imageLayer = new ImageLayer({
      source: new ImageStatic({
        url: '../../../../../assets/pictures/map.png',
        imageExtent: mapExtent
      })
    });

    this.map.addLayer(imageLayer);
  }

  private resetMap(): void {
    const layers = this.map.getLayers().getArray().filter((l: BaseLayer) => l.getProperties().layerId);
    layers.forEach(l => this.map.removeLayer(l));
    this.setMarkers();
  }

  private setMarkers(): void {
    const mapBounds = this.map.getView().calculateExtent(this.map.getSize());
    for (let i = 0; i < this.roulettePokemons.length; i++) {
      const coordinates = this.locationService.getRandomCoordinates(mapBounds[0], mapBounds[2], mapBounds[1], mapBounds[3]);
      const marker = new Feature({
        geometry: new Point(
          [coordinates.longitudue, coordinates.latitude]
        )
      });

      const markerIcon = new Icon({
        scale: 0.2,
        src: `data:image/png;base64,${this.roulettePokemons[i].images[0]['contentImage']}`
      })

      const markerStyle = new Style();
      markerStyle.setImage(markerIcon);

      marker.setStyle(markerStyle);

      marker.setId(this.roulettePokemons[i].id);

      marker.setProperties({
        name: this.roulettePokemons[i].name,
        img: this.roulettePokemons[i].images[0]
      });

      console.log(marker.getGeometry()['flatCoordinates']);

      let vectorSource = new SourceVector({
        features: [marker]
      });

      let layerVector = new LayerVector({
        source: vectorSource,
      });

      layerVector.set('layerId', marker.getId());

      this.map.addLayer(layerVector);
    }

    this.setPokemonsToMarkers();
  }

  private setPokemonsToMarkers(): void {
    this.map.on('singleclick', (evt) => {

      const feature = this.map.forEachFeatureAtPixel(evt.pixel, (feature) => {
        return feature;
      })

      console.log(evt.coordinate);

      if (feature) {
        this.catch(feature.getId());
      } else {
        this.resetMap();
      }
    });
  }

  public catch(pokemonId): void {
    this.subscription.add(this.mapPokemonsService.catchPokemon(pokemonId).pipe(
      tap(() => this.toastrService.openToastr("You successfully caught this pokemon!"))
    ).subscribe(() => { }, () => this.toastrService.openToastr("You didn't catch this pokemon. Bettter luck next timeZ!")));
    this.map.getLayers().forEach((layer: BaseLayer) => {
      if (layer.getProperties()['layerId'] === pokemonId) {
        this.map.removeLayer(layer);
      }
    });
  }

  public get isLoaded(): boolean {
    return this.loaded;
  }
}