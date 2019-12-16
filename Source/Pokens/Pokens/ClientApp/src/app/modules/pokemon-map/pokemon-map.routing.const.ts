import { Routes } from '@angular/router';
import { PokemonMapComponent } from './pages/pokemon-map/pokemon-map.component';
import { AuthGuard } from 'src/app/shared/core/auth.guard';


export const routes: Routes = [{
  path: '',
  component: PokemonMapComponent,
  canActivate: [AuthGuard]
}];
