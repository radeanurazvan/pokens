import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AppNotifications {
  private stoppedOnPurpose = false;
  private connection: HubConnection;

  public constructor(
    private router: Router
  ) {
    this.connection = new HubConnectionBuilder()
      .withUrl(environment.battlesHubsUrl)
      .build();
  }

  public start(trainerId: string): void {
    this.startConnection(() => {
      this.connection.invoke('joinTrainerNotifications', trainerId)
        .then(() => console.log(`Subscribed to notifications for trainer ${trainerId}`));
    });

    this.connection.onclose((e: Error) => {
      if (this.stoppedOnPurpose) {
        return;
      }

      console.log('Trainers connection closed. Starting again...');
      this.startConnection();
    });

    this.connection.on('battleStartedEvent', () => {
      console.log("hit battle started");
      setTimeout(() => {
        this.router.navigate(['/home/arena/current-battle']);
      }, 300);
    });
  }

  private startConnection(cb: () => void = () => { }): void {
    this.connection.start()
      .then(() => console.log('Connection started'))
      .then(cb)
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public stop(): void {
    this.stoppedOnPurpose = true;
    this.connection.stop()
      .then(() => console.log('Stopped Trainers notifications subscription'));
  }
}
