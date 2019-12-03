import { Routes } from '@angular/router';
import { StarterPokemonsComponent } from './pages/starter-pokemons/starter-pokemons.component';
import { AuthGuard } from 'src/app/shared/core/auth.guard';


export const routes: Routes = [{
  path: '',
  component: StarterPokemonsComponent,
  canActivate: [AuthGuard]
}];
