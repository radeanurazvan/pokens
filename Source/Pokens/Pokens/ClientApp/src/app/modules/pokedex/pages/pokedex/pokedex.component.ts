import { Component, OnInit } from '@angular/core';
import { PokedexService } from '../../core/pokedex.service';
import { PokedexPokemonModel } from '../../models/pokedex-pokemon.model';
import { MatDialog } from '@angular/material';
import { PopupComponent } from 'src/app/shared/components/popup/popup.component';

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
    const popupRef = this.dialog.open(PopupComponent, {
      data: {
        title: pokemon.name,
        content: `Are you sure you want to pick ${pokemon.name}?`,
      }
    });
  }
}
