import { Component, OnInit, OnDestroy, ElementRef, ViewChild, AfterViewChecked } from '@angular/core';
import { BattlesService } from '../../../core/battles.service';
import { ArenaService } from '../../../core/arena.service';
import { tap, switchMap, map, delay } from 'rxjs/operators';
import { UserService } from '../../../../../shared/core/user.service';
import { CurrentBattleNotifications } from './current-battle.notifications';
import { state, trigger, style, animate, transition, keyframes } from '@angular/animations';
import { MatDialog } from '@angular/material';
import { BattleWonPopupComponent } from './battle-won/battle-won.popup.component';
import { BattleLostPopupComponent } from './battle-lost/battle-lost.popup.component';

@Component({
  templateUrl: './current-battle.component.html',
  styleUrls: ['./current-battle.component.scss'],
  animations: [
    trigger('shakeit', [
      state('shakestart', style({
        transform: 'scaleX(1)',
      })),
      state('shakeend', style({
        transform: 'scaleX(1)',
      })),
      transition('shakestart => shakeend', animate('1000ms ease-in', keyframes([
        style({ transform: 'translate3d(-1px, 0, 0)', offset: 0.1 }),
        style({ transform: 'translate3d(2px, 0, 0)', offset: 0.2 }),
        style({ transform: 'translate3d(-4px, 0, 0)', offset: 0.3 }),
        style({ transform: 'translate3d(4px, 0, 0)', offset: 0.4 }),
        style({ transform: 'translate3d(-4px, 0, 0)', offset: 0.5 }),
        style({ transform: 'translate3d(4px, 0, 0)', offset: 0.6 }),
        style({ transform: 'translate3d(-4px, 0, 0)', offset: 0.7 }),
        style({ transform: 'translate3d(2px, 0, 0)', offset: 0.8 }),
        style({ transform: 'translate3d(-1px, 0, 0)', offset: 0.9 }),
      ]))),
      transition('shakeend => shakestart', animate('1000ms ease-in', keyframes([
        style({ transform: 'translate3d(-1px, 0, 0)', offset: 0.1 }),
        style({ transform: 'translate3d(2px, 0, 0)', offset: 0.2 }),
        style({ transform: 'translate3d(-4px, 0, 0)', offset: 0.3 }),
        style({ transform: 'translate3d(4px, 0, 0)', offset: 0.4 }),
        style({ transform: 'translate3d(-4px, 0, 0)', offset: 0.5 }),
        style({ transform: 'translate3d(4px, 0, 0)', offset: 0.6 }),
        style({ transform: 'translate3d(-4px, 0, 0)', offset: 0.7 }),
        style({ transform: 'translate3d(2px, 0, 0)', offset: 0.8 }),
        style({ transform: 'translate3d(-1px, 0, 0)', offset: 0.9 }),
      ])))
    ]),
    trigger('dead', [
      state('deadstart', style({
        transform: 'scale(1)',
      })),
      state('deadend', style({
        transform: 'scale(-1)',
      })),
      transition('deadstart => deadend', animate('1000ms ease-in'))
    ])
  ]
})
export class CurrentBattleComponent implements OnInit, OnDestroy, AfterViewChecked {
  @ViewChild('box', { static: false }) private myScrollContainer: ElementRef;

  private trainerId: string;

  public myCurrentState = 'shakestart';
  public myEnemyState = 'shakestart';
  public myCurrentDeadState = 'deadstart';
  public myEnemyDeadState = 'deadstart';

  public myMaxHealth: number = 100;
  public enemyMaxHealth: number = 100;

  public myHealth: number = 100;
  public enemyHealth: number = 100;

  public isMeActivePlayer = false;

  public trainerName: string;
  public battle: any;
  public pokemons: any[] = [];
  public selectedAbilityIndex;
  public abilities: any[] = [];
  public myPokemonHealth: number;
  public enemyPokemonHealth: number;

  public get comentaries() {
    if (this.battle) {
      return this.battle.commentaries;
    }
    return null;
  }

  public get myPokemon() {
    const pokemon = this.pokemons.find(p => p.trainerId === this.trainerId);
    if (pokemon) {
      pokemon.abilities = pokemon.abilities.sort((a, b) => (a.requiredLevel > b.requiredLevel) ? 1 : -1);
    }
    return pokemon;
  }

  public get enemyPokemon() {
    return this.pokemons.find(p => p.trainerId !== this.trainerId);
  }

  constructor(
    private service: BattlesService,
    private arenaService: ArenaService,
    private userService: UserService,
    private currentBattleNotifications: CurrentBattleNotifications,
    private dialog: MatDialog) {
  }

  private changeCurrentState() {
    this.myCurrentState = this.myCurrentState === 'shakestart' ? 'shakeend' : 'shakestart';
  }

  private changeEnemyState() {
    this.myEnemyState = this.myEnemyState === 'shakestart' ? 'shakeend' : 'shakestart';
  }

  private changeCurrentDeadState() {
    this.myCurrentDeadState = this.myCurrentDeadState === 'deadstart' ? 'deadend' : 'deadstart';
  }

  private changeEnemyDeadState() {
    this.myEnemyDeadState = this.myEnemyDeadState === 'deadstart' ? 'deadend' : 'deadstart';
  }

  private initEvent(): void {
    this.currentBattleNotifications
      .onCooldownChanged(x => {
        if (this.trainerId !== x.playerId) {
          return;
        }

        this.abilities.find(a => a.id === x.abilityId).cooldown = x.cooldown;
      })
      .onTurnTaken(x => {
        this.isMeActivePlayer = x.playerId !== this.trainerId;
        setTimeout(() => {
          this.service.getCurrentBattle().subscribe(b => this.battle = b);
        }, 300);
      })
      .onHealthChanged(x => {
        if (this.trainerId === x.trainerId) {
          this.changeCurrentState();
          this.myHealth = x.newHealth;
        } else {
          this.changeEnemyState();
          this.enemyHealth = x.newHealth;
        }
      })
      .onAbilityDodged(x => {
        if (this.trainerId === x.trainerId) {
          console.log('Dodged');
        }
      })
      .onBattleWon((x) => {
        if (this.trainerId !== x.trainerId) {
          return;
        }

        this.changeEnemyDeadState();
        setTimeout(() => {
          this.dialog.open(BattleWonPopupComponent, {
            data: x,
            width: "900px"
          });
        }, 1200);
        console.log('You won');
      })
      .onBattleLost((x) => {
        if (this.trainerId !== x.trainerId) {
          return;
        }

        this.changeCurrentDeadState();
        setTimeout(() => {
          this.dialog.open(BattleLostPopupComponent, {
            data: x,
            width: "900px"
          });
        }, 1200);
        console.log('You lose');
      });
  }

  public selectAbility(index: number): void {
    if (this.myPokemon.level >= this.myPokemon.abilities[index].requiredLevel) {
      this.selectedAbilityIndex = index;
    }
  }

  public ngOnInit(): void {
    this.initEvent();
    this.trainerId = this.userService.getUserId();
    this.trainerName = this.userService.getUserName();
    this.service.getCurrentBattle()
      .pipe(
        tap(b => this.battle = b),
        switchMap(b => this.arenaService.getAllPokemons([b.attackerId, b.defenderId])),
        map(pokemons => pokemons.filter(p => p.id === this.battle.attackerPokemonId || p.id === this.battle.defenderPokemonId)),
        tap(pokemons => this.pokemons = pokemons)
      )
      .subscribe(() => {
        this.abilities = this.myPokemon.abilities;
        this.currentBattleNotifications.start(this.battle.id);

        const isMeAttacker = this.battle.attackerId === this.trainerId;

        this.myMaxHealth = isMeAttacker ? this.battle.initialAttackerHealth : this.battle.initialDefenderHealth;
        this.enemyMaxHealth = isMeAttacker ? this.battle.initialDefenderHealth : this.battle.initialAttackerHealth;

        this.myHealth = isMeAttacker ? this.battle.attackerHealth : this.battle.defenderHealth;
        this.enemyHealth = isMeAttacker ? this.battle.defenderHealth : this.battle.attackerHealth;

        console.log(this.battle);
        console.log(this.pokemons);
      });
  }

  public ngAfterViewChecked(): void {
    this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
  }

  public ngOnDestroy(): void {
    this.currentBattleNotifications.stop();
  }

  public attack(): void {
    this.service.attack(this.battle.id, this.abilities[this.selectedAbilityIndex].id).subscribe();
  }
}
