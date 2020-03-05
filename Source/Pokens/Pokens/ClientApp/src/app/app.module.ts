import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { JwtModule } from '@auth0/angular-jwt';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './router/app-routing.module';
import { FirstLoginGuard } from './shared/core/first-login.guard';
import { HasPokemonsGuard } from './shared/core/has-pokemons.guard';
import { AuthGuard } from './shared/core/auth.guard';
import { IsInArenaGuard } from './shared/core/is-in-arena.guard';
import { SharedModule } from './shared/shared.module';
import { ToastrModule } from 'ngx-toastr';

export function tokenGetter() {
  return localStorage.getItem("currentUserToken");
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 2000,
      positionClass: 'toast-bottom-full-width'
    }),
    SharedModule.forChild(),
    JwtModule.forRoot({
      config: {
        tokenGetter,
        whitelistedDomains: [],
        blacklistedRoutes: []
      }
    })
  ],
  providers: [FirstLoginGuard, HasPokemonsGuard, AuthGuard, IsInArenaGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
