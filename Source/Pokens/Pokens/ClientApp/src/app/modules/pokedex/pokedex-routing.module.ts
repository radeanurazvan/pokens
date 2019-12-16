import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { routes } from './pokedex-routing.const';

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PokedexRoutingModule { }