import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule, MatButtonModule } from '@angular/material';

import { ArenaComponent } from './pages/arena/arena/arena.component';
import { SharedModule } from '../../shared/shared.module';
import { ArenaRoutingModule } from './arena-routing.module';
import { ArenaDetailsComponent } from './pages/arena-details/arena-details.component';
import { ChallengePopupComponent } from './pages/arena-details/challenge-popup/challenge-popup.component';
import { ChallengesComponent } from './pages/arena/challenges/challenges.component';
import { CurrentBattleComponent } from './pages/arena/current-battle/current-battle.component';



@NgModule({
  declarations: [ArenaComponent, ArenaDetailsComponent, ChallengePopupComponent, ChallengesComponent, CurrentBattleComponent],
  entryComponents: [ChallengePopupComponent],
  imports: [
    CommonModule,
    SharedModule.forChild(),
    ArenaRoutingModule,
    MatCardModule,
    MatButtonModule
  ]
})
export class ArenaModule { }
