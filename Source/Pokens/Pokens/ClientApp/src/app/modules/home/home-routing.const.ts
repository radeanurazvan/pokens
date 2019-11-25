import { Routes } from '@angular/router';
import { AuthGuard } from 'src/app/shared/core/auth.guard';

import { HomeComponent } from './pages/home/home.component';

export const routes: Routes = [{
  path: '',
  component: HomeComponent,
  canActivate: [AuthGuard]
}]