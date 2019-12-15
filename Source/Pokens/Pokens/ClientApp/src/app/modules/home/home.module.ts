import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';

import { SideBarComponent } from '../../shared/components/side-bar/side-bar.component';
import { AuthModule } from '../auth/auth.module';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './pages/home/home.component';
import { MapComponent } from './pages/map/map.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { PokedexComponent } from './pages/pokedex/pokedex.component';

@NgModule({
  declarations: [HomeComponent, SideBarComponent, MapComponent, ProfileComponent, PokedexComponent],
  imports: [
    CommonModule,
    [SharedModule.forChild()],
    HomeRoutingModule,
    AuthModule
  ],
  providers: []
})
export class HomeModule { }
