import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        loadChildren: () => import('../modules/auth/auth.module').then(m => m.AuthPageModule)
    },
    {
        path: 'home',
        loadChildren: () => import('../modules/home/home.module').then(m => m.HomePageModule)
    },
    {
        path: '**',
        redirectTo: '',
        pathMatch: 'full'
    }
]
