import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { MatCardModule } from '@angular/material';

import { PokedexComponent } from './pages/pokedex/pokedex.component';
import { PokedexRoutingModule } from './pokedex-routing.module';

@NgModule({
  declarations: [PokedexComponent],
  imports: [
    CommonModule,
    SharedModule.forChild(),
    PokedexRoutingModule,
    MatCardModule
  ]
})
export class PokedexModule { }
