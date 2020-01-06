import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { JwtModule } from '@auth0/angular-jwt';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './router/app-routing.module';
import { FirstLoginGuard } from './shared/core/first-login.guard';
import { HasPokemonsGuard } from './shared/core/has-pokemons.guard';
import { AuthGuard } from './shared/core/auth.guard';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: null,
        whitelistedDomains: [],
        blacklistedRoutes: []
      }
    })
  ],
  providers: [FirstLoginGuard, HasPokemonsGuard, AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
