import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';

import { AuthRoutingModule } from './auth-routing.module';
import { AuthService } from './core/auth.service';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    SharedModule
  ],
  exports: [
    LoginComponent,
    RegisterComponent
  ],
  providers: [
    AuthService
  ]
})
export class AuthPageModule { }
