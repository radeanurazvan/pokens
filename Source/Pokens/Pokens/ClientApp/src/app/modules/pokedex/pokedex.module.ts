import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';

import { PokedexComponent } from './pages/pokedex/pokedex.component';
import { PokedexRoutingModule } from './pokedex-routing.module';
import { PokedexService } from './core/pokedex.service';

@NgModule({
  declarations: [PokedexComponent],
  imports: [
    CommonModule,
    SharedModule.forChild(),
    PokedexRoutingModule
  ],
  providers: [PokedexService]
})
export class PokedexModule { }
