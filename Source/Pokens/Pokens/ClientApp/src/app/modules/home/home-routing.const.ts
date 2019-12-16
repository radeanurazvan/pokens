import { Routes } from '@angular/router';
import { AuthGuard } from 'src/app/shared/core/auth.guard';

import { MapComponent } from './pages/map/map.component';
import { PokedexComponent } from './pages/pokedex/pokedex.component';
import { ProfileComponent } from './pages/profile/profile.component';

export const routes: Routes = [
  {
    path: 'map',
    component: HomeComponent,
    loadChildren: () => import('../pokemon-map/pokemon-map.module').then(m => m.PokemonMapModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'profile',
    component: HomeComponent,
    loadChildren: () => import('../profile/profile.module').then(m => m.ProfileModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'pokedex',
    component: HomeComponent,
    loadChildren: () => import('../pokedex/pokedex.module').then(m => m.PokedexModule),
    canActivate: [AuthGuard]
  },
  {
    path: '',
    redirectTo: 'map'
  }]
