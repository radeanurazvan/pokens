import { Routes } from '@angular/router';

import { ArenaComponent } from './pages/arena/arena/arena.component';
import { ArenaDetailsComponent } from './pages/arena-details/arena-details.component';
import { IsInArenaGuard } from 'src/app/shared/core/is-in-arena.guard';

export const routes: Routes = [
    {
        path: '',
        component: ArenaComponent,
        canActivate: [IsInArenaGuard]
    },
    {
        path: 'details',
        component: ArenaDetailsComponent
    }
];
