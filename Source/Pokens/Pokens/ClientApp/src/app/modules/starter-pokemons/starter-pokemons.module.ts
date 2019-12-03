import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StarterPokemonsRoutingModule } from './starter-pokemons-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { StarterPokemonsComponent } from './pages/starter-pokemons/starter-pokemons.component';
import { StarterPokemonsService } from './core/starter-pokemons.service';



@NgModule({
  declarations: [
    StarterPokemonsComponent
  ],
  imports: [
    CommonModule,
    [SharedModule.forChild()],
    StarterPokemonsRoutingModule,
    // AuthModule
  ],
  providers: [StarterPokemonsService]
})
export class StarterPokemonsModule { }
