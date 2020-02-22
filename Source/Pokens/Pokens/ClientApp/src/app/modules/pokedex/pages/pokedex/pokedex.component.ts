import { Component, OnInit } from '@angular/core';
import { PokedexService } from '../../core/pokedex.service';
import { PokedexPokemonModel } from '../../models/pokedex-pokemon.model';
import { MatDialog } from '@angular/material';
import { PopupDetailsComponent } from '../popup-details/popup-details.component';
import { Colors } from 'src/app/shared/components/colors.const';
import { map, tap } from 'rxjs/operators';

@Component({
  selector: 'app-pokedex',
  templateUrl: './pokedex.component.html',
  styleUrls: ['./pokedex.component.scss']
})
export class PokedexComponent implements OnInit {
  public pokemons: PokedexPokemonModel[];
  constructor(
    private pokedexService: PokedexService,
    private dialog: MatDialog
  ) {}

  ngOnInit() {
    this.pokedexService
      .getAllPokemons()
      .pipe(
        map(data =>
          data.map(x => {
            x.color = this.getBackGroundColor(x.name.charCodeAt(0));
            return x;
          })
        ),
        tap(data => (this.pokemons = data))
      )
      .subscribe();
  }

  public openPopup(pokemon: PokedexPokemonModel): void {
    this.dialog.open(PopupDetailsComponent, {
      width: '70%',
      height: '60%',
      data: {
        name: pokemon.name,
        stats: {
          health: pokemon.stats.health,
          defense: pokemon.stats.defense,
          dodgeChance: pokemon.stats.dodgeChance,
          attackPower: pokemon.stats.attackPower,
          criticalStrikeChance: pokemon.stats.criticalStrikeChance,
          summary: pokemon.stats.summary
        },
        images: pokemon.images,
        isStarter: pokemon.isStarter,
        abilities: pokemon.abilities
      }
    });
  }

  private getBackGroundColor(code: number): string {
    return Colors[this.getRandomInt(code)];
  }

  private getRandomInt(code: number) {
    return Math.floor(code % 5);
  }
}
