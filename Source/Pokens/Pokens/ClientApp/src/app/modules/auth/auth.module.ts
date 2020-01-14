import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { SharedModule } from 'src/app/shared/shared.module';

import { AuthRoutingModule } from './auth-routing.module';
import { AuthService } from './core/auth.service';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { MatProgressSpinnerModule } from '@angular/material';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    MatProgressSpinnerModule,
    [SharedModule.forChild()]
  ],
  exports: [
    LoginComponent,
    RegisterComponent
  ],
  providers: [
    AuthService,
    JwtHelperService
  ]
})
export class AuthModule { }
