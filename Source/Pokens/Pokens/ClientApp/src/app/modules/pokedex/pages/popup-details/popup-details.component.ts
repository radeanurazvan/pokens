import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';
import { PokedexPokemonModel } from '../../models/pokedex-pokemon.model';

@Component({
  selector: 'app-popup-details',
  templateUrl: './popup-details.component.html',
  styleUrls: ['./popup-details.component.scss']
})
export class PopupDetailsComponent implements OnInit {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: PokedexPokemonModel
  ) { }
  
  ngOnInit() {
  }

}
