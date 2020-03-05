import { Component, OnInit } from '@angular/core';
import { AppNotifications } from './app.notifications';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'ClientApp';

  constructor(private notifications: AppNotifications) {
  }

  public ngOnInit(): void {
    const token = localStorage.getItem('currentUserToken');
    if (!token) {
      return;
    }

    const trainerId = new JwtHelperService().decodeToken(token).TrainerId;
    this.notifications.start(trainerId);
  }
}
