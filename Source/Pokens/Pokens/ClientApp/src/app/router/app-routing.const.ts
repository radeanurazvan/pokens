import { Routes } from '@angular/router';
import { FirstLoginGuard } from '../shared/core/first-login.guard';
import { HasPokemonsGuard } from '../shared/core/has-pokemons.guard';
import { AuthGuard } from '../shared/core/auth.guard';

export const routes: Routes = [
    {
        path: 'auth',
        loadChildren: () => import('../modules/auth/auth.module').then(m => m.AuthModule)
    },
    {
        path: 'home',
        loadChildren: () => import('../modules/home/home.module').then(m => m.HomeModule),
        canActivate: [AuthGuard, FirstLoginGuard]
    },
    {
        path: 'starter',
        loadChildren: () => import('../modules/starter-pokemons/starter-pokemons.module').then(m => m.StarterPokemonsModule),
        canActivate: [AuthGuard, HasPokemonsGuard]
    },
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full'
    }
]
