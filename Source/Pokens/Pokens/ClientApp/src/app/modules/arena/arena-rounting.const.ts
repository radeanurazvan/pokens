import { Routes } from '@angular/router';

import { ArenaComponent } from './pages/arena/arena/arena.component';
import { ArenaDetailsComponent } from './pages/arena-details/arena-details.component';
import { IsInArenaGuard } from 'src/app/shared/core/is-in-arena.guard';
import { ChallengesComponent } from './pages/arena/challenges/challenges.component';
import { CurrentBattleGuard } from '../../shared/core/current-battle.guard';
import { CurrentBattleComponent } from './pages/arena/current-battle/current-battle.component';

export const routes: Routes = [
    {
        path: '',
        component: ArenaComponent,
        canActivate: [IsInArenaGuard]
    },
    {
        path: 'details',
        component: ArenaDetailsComponent,
        canActivate: [CurrentBattleGuard]
    },
    {
      path: ':id/challenges',
      component: ChallengesComponent
    },
    {
      path: 'current-battle',
      component: CurrentBattleComponent
    }
];
