import { Routes } from '@angular/router';

import { ArenaComponent } from './pages/arena/arena/arena.component';
import { ArenaDetailsComponent } from './pages/arena-details/arena-details.component';

export const routes: Routes = [
    {
        path: '',
        component: ArenaComponent
    },
    {
        path: 'details',
        component: ArenaDetailsComponent
    }
];
