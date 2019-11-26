import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';

import { AuthModule } from '../auth/auth.module';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './pages/home/home.component';

@NgModule({
  declarations: [HomeComponent],
  imports: [
    CommonModule,
    [SharedModule.forChild()],
    HomeRoutingModule,
    AuthModule
  ],
  providers: []
})
export class HomeModule { }
