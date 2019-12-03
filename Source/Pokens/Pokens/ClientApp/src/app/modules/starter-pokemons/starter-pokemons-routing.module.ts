import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { routes } from './starter-pokemons.routing.const';

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StarterPokemonsRoutingModule { }

