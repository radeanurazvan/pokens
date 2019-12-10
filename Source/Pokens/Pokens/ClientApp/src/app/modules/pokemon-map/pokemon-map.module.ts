import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { PokemonMapRoutingModule } from './pokemon-map-routing.module';
import { PokemonMapComponent } from './pages/pokemon-map/pokemon-map.component';
import { LocationService } from './pages/core/location.service';
import { MatProgressSpinnerModule } from '@angular/material';



@NgModule({
  declarations: [
    PokemonMapComponent
  ],
  imports: [
    CommonModule,
    SharedModule.forChild(),
    PokemonMapRoutingModule,
    MatProgressSpinnerModule
  ],
  providers: [LocationService]
})
export class PokemonMapModule { }
