import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { JwtModule } from '@auth0/angular-jwt';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './router/app-routing.module';
import { FirstLoginGuard } from './shared/core/first-login.guard';

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
  providers: [FirstLoginGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
