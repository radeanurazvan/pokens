import { Routes } from '@angular/router';

import { ArenaComponent } from './pages/arena/arena/arena.component';
import { ArenaDetailsComponent } from './pages/arena-details/arena-details.component';
import { IsInArenaGuard } from 'src/app/shared/core/is-in-arena.guard';
import { ChallengesComponent } from './pages/arena/challenges/challenges.component';

export const routes: Routes = [
    {
        path: '',
        component: ArenaComponent,
        canActivate: [IsInArenaGuard]
    },
    {
        path: 'details',
        component: ArenaDetailsComponent
    },
    {
      path: ':id/challenges',
      component: ChallengesComponent
  }
];
