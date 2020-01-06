import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { routes } from './arena-rounting.const';

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ArenaRoutingModule { }
