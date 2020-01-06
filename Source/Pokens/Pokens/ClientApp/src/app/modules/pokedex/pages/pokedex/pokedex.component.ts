import { Component, OnInit } from '@angular/core';
import { PokedexService } from '../../core/pokedex.service';

@Component({
  selector: 'app-pokedex',
  templateUrl: './pokedex.component.html',
  styleUrls: ['./pokedex.component.scss']
})
export class PokedexComponent implements OnInit {

  constructor(private pokedexService: PokedexService) { }

  ngOnInit() {
    this.pokedexService.getAllPokemons().subscribe((data) => console.log(data));
  }

}
