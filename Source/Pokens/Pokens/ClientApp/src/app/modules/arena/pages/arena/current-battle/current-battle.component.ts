import { Component, OnInit, OnDestroy, ElementRef, ViewChild, AfterViewChecked } from '@angular/core';
import { BattlesService } from '../../../core/battles.service';
import { ArenaService } from '../../../core/arena.service';
import { tap, switchMap, map, delay } from 'rxjs/operators';
import { UserService } from '../../../../../shared/core/user.service';
import { CurrentBattleNotifications } from './current-battle.notifications';

@Component({
  templateUrl: './current-battle.component.html',
  styleUrls: ['./current-battle.component.scss']
})
export class CurrentBattleComponent implements OnInit, OnDestroy, AfterViewChecked {
  @ViewChild('box', { static: false }) private myScrollContainer: ElementRef;

  private trainerId: string;

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
    private currentBattleNotifications: CurrentBattleNotifications) {
  }

  private initEvent(): void {
    this.currentBattleNotifications
      .onCooldownChanged(x => {
        this.abilities.find(a => a.id === x.abilityId).cooldown = x.cooldown;
      })
      .onTurnTaken(x => {
        setTimeout(() => {
          this.service.getCurrentBattle().subscribe(b => this.battle = b);
        }, 300);
      })
      .onHealthChanged(x => {
        if (this.trainerId === x.trainerId) {
          console.log('Took damage');
        }
      })
      .onAbilityDodged(x => {
        if (this.trainerId === x.trainerId) {
          console.log('Dodged');
        }
      })
      .onBattleWon(() => {
        console.log('You won');
      })
      .onBattleLost(() => {
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
