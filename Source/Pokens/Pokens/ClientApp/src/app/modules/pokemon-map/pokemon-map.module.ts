import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { PokemonMapRoutingModule } from './pokemon-map-routing.module';
import { PokemonMapComponent } from './pages/pokemon-map/pokemon-map.component';



@NgModule({
  declarations: [
    PokemonMapComponent
  ],
  imports: [
    CommonModule,
    SharedModule.forChild(),
    PokemonMapRoutingModule
  ]
})
export class PokemonMapModule { }
