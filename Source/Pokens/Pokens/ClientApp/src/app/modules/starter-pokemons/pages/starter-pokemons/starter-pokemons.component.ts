import { Component, OnInit, Inject } from '@angular/core';
import { StarterPokemonsService } from '../../core/starter-pokemons.service';
import { StarterPokemonModel } from '../../models/starter-pokemon.model';
import { Guid } from 'guid-typescript';
import { MatDialog } from '@angular/material';
import { PopupComponent } from 'src/app/shared/components/popup/popup.component';

@Component({
  selector: 'app-starter-pokemons',
  templateUrl: './starter-pokemons.component.html',
  styleUrls: ['./starter-pokemons.component.scss']
})
export class StarterPokemonsComponent implements OnInit {

  public pokemons: StarterPokemonModel[] = [];

  constructor(
    private starterService: StarterPokemonsService,
    private dialog: MatDialog) {
  }

  public ngOnInit(): void {
    this.starterService.getPokemons().subscribe((data: StarterPokemonModel[]) => this.pokemons = data);
  }

  public openPopup(pokemon: StarterPokemonModel): void {
    const popupRef = this.dialog.open(PopupComponent, {
      data: {
        title: 'Big decision(no pressure)',
        content: `Are you sure you want to pick ${pokemon.name}?`,
        value: pokemon.id
      }
    });

    popupRef.afterClosed().subscribe(result => console.log(result));
  }
}
