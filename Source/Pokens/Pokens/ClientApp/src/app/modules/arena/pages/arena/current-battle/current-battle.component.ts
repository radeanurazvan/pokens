import { Component, OnInit, OnDestroy } from '@angular/core';
import { BattlesService } from '../../../core/battles.service';
import { ArenaService } from '../../../core/arena.service';
import { tap, switchMap, map } from 'rxjs/operators';
import { UserService } from '../../../../../shared/core/user.service';
import { CurrentBattleNotifications } from './current-battle.notifications';

@Component({
  templateUrl: './current-battle.component.html',
  styleUrls: ['./current-battle.component.scss']
})
export class CurrentBattleComponent implements OnInit, OnDestroy {
  private trainerId: string;

  public myMaxHealth: number = 100;
  public enemyMaxHealth: number = 100;

  public myHealth: number = 100;
  public enemyHealth: number = 100;

  public trainerName: string;
  public battle: any;
  public pokemons: any[] = [];
  public selectedAbilityIndex;
  public abilities: any[] = [];

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
      .onCooldownChanged(x => this.abilities.find(a => a.id === x.abilityId).cooldown = x.cooldown)
      .onHealthChanged(hc => {
        if (hc.trainerId === this.trainerId) {
          this.myHealth = hc.newHealth;
        } else {
          this.enemyHealth = hc.newHealth;
        }
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

  public ngOnDestroy(): void {
    this.currentBattleNotifications.stop();
  }

  public attack(): void {
    this.service.attack(this.battle.id, this.abilities[this.selectedAbilityIndex].id).subscribe();
  }
}
