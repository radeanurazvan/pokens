import { Routes } from '@angular/router';
import { AuthGuard } from 'src/app/shared/core/auth.guard';

import { MapComponent } from './pages/map/map.component';
import { PokedexComponent } from './pages/pokedex/pokedex.component';
import { ProfileComponent } from './pages/profile/profile.component';

export const routes: Routes = [{
  path: 'map',
  component: MapComponent,
  canActivate: [AuthGuard]
},
{
  path: 'profile',
  component: ProfileComponent,
  canActivate: [AuthGuard]
},
{
  path: 'pokedex',
  component: PokedexComponent,
  canActivate: [AuthGuard]
},
{
  path: '',
  redirectTo: 'map'
}]