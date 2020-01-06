import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArenaComponent } from './pages/arena/arena/arena.component';
import { SharedModule } from '../../shared/shared.module';
import { ArenaRoutingModule } from './arena-routing.module';



@NgModule({
  declarations: [ArenaComponent],
  imports: [
    CommonModule,
    SharedModule.forChild(),
    ArenaRoutingModule
  ]
})
export class ArenaModule { }
