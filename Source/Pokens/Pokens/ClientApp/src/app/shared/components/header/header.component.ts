import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  private titles: { [id: string]: string } = {
    'Map': 'Find some pokemons!',
    'Arena': 'Here you can see all available arenas',
    'Profile': 'These are all the pokemons that you have',
    'Pokedex': 'Want to know more about pokemons?'
  };

  constructor() { }

  ngOnInit() {
  }

  // getTitle(): string {

  // }
}
