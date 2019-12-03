import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { SharedModule } from 'src/app/shared/shared.module';

import { StarterPokemonsComponent } from './pages/starter-pokemons/starter-pokemons.component';
import { StarterPokemonsService } from './core/starter-pokemons.service';
import { StarterPokemonsRoutingModule } from './starter-pokemons-routing.module';

@NgModule({
  declarations: [
    StarterPokemonsComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    SharedModule.forChild(),
    StarterPokemonsRoutingModule
  ],
  providers: [StarterPokemonsService]
})
export class StarterPokemonsModule { }
