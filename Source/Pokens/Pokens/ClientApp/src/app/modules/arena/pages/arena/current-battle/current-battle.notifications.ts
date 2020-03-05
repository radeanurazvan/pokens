import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { environment } from 'src/environments/environment';

interface CooldownChanged {
  playerId: string;
  abilityId: string;
  cooldown: number;
}

interface TurnTaken {
  playerId: string;
}

interface TrainerHealthChanged {
  trainerId: string;
  newHealth: number;
}

interface PokemonDodged {
  trainerId: string;
}

interface BattleLost {
  trainerId: string;
  experience: number;
}

interface BattleWon {
  trainerId: string;
  experience: number;
}

@Injectable()
export class CurrentBattleNotifications {
  private stoppedOnPurpose = false;
  private connection: HubConnection;

  public constructor() {
    this.connection = new HubConnectionBuilder()
      .withUrl(environment.battlesHubsUrl)
      .build();
  }

  public start(battleId: string): void {
    this.startConnection(() => {
      this.connection.invoke('joinBattleNotifications', battleId)
        .then(() => console.log(`Subscribed to notifications for battle ${battleId}`));
    });

    this.connection.onclose((e: Error) => {
      if (this.stoppedOnPurpose) {
        return;
      }

      console.log('Battles connection closed. Starting again...');
      this.startConnection();
    });
  }

  private startConnection(cb: () => void = () => { }): void {
    this.connection.start()
      .then(() => console.log('Connection started'))
      .then(cb)
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public onCooldownChanged(cb: (CooldownChanged) => void): CurrentBattleNotifications {
    this.connection.on('PlayerCooldownChangedEvent', cb);
    return this;
  }

  public onTurnTaken(cb: (TurnTaken) => void): CurrentBattleNotifications {
    this.connection.on('PlayerTookTurnEvent', cb);
    return this;
  }

  public onHealthChanged(cb: (TrainerHealthChanged) => void): CurrentBattleNotifications {
    this.connection.on('BattleHealthChangedEvent', cb);
    return this;
  }

  public onAbilityDodged(cb: (PokemonDodged) => void): CurrentBattleNotifications {
    this.connection.on('PokemonDodgedAbilityEvent', cb);
    return this;
  }

  public onBattleLost(cb: (BattleLost) => void): CurrentBattleNotifications {
    this.connection.on('TrainerLostBattleEvent', cb);
    return this;
  }

  public onBattleWon(cb: (BattleWon) => void): CurrentBattleNotifications {
    this.connection.on('TrainerWonBattleEvent', cb);
    return this;
  }

  public stop(): void {
    this.stoppedOnPurpose = true;
    this.connection.stop()
      .then(() => console.log('Stopped battles notifications subscription'));
  }
}
