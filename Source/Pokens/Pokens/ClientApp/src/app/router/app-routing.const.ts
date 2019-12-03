import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'auth',
        loadChildren: () => import('../modules/auth/auth.module').then(m => m.AuthModule)
    },
    {
        path: 'home',
        loadChildren: () => import('../modules/home/home.module').then(m => m.HomeModule)
    },
    {
        path: 'starter',
        loadChildren: () => import('../modules/starter-pokemons/starter-pokemons.module').then(m => m.StarterPokemonsModule)
    },
    {
        path: '**',
        redirectTo: 'home',
        pathMatch: 'full'
    }
]
