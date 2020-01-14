import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';

import { SideBarComponent } from '../../shared/components/side-bar/side-bar.component';
import { AuthModule } from '../auth/auth.module';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './pages/home/home.component';
import { HeaderComponent } from '../../shared/components/header/header.component';

@NgModule({
  declarations: [HomeComponent, SideBarComponent, HeaderComponent],
  imports: [
    CommonModule,
    [SharedModule.forChild()],
    HomeRoutingModule,
    AuthModule
  ],
  providers: []
})
export class HomeModule { }
