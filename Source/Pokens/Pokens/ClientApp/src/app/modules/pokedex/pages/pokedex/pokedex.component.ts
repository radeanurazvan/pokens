import { Component, OnInit } from '@angular/core';
import { PokedexService } from '../../core/pokedex.service';
import { PokedexPokemonModel } from '../../models/pokedex-pokemon.model';
import { MatDialog } from '@angular/material';
import { PopupComponent } from 'src/app/shared/components/popup/popup.component';
import { PopupDetailsComponent } from '../popup-details/popup-details.component';

@Component({
  selector: 'app-pokedex',
  templateUrl: './pokedex.component.html',
  styleUrls: ['./pokedex.component.scss']
})
export class PokedexComponent implements OnInit {
  public pokemons: PokedexPokemonModel[];
  constructor(private pokedexService: PokedexService,
    private dialog: MatDialog) 
    { }

  ngOnInit() {
    this.pokedexService.getAllPokemons().subscribe((pokens: PokedexPokemonModel[]) => {
      this.pokemons = pokens
    });
  }

  public openPopup(pokemon: PokedexPokemonModel): void {
    const popupRef = this.dialog.open(PopupDetailsComponent, {
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
}
