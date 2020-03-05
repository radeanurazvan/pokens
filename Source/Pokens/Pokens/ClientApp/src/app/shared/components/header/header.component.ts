import { Component, OnInit, Input } from "@angular/core";
import { Router } from "@angular/router";

@Component({
  selector: "app-header",
  templateUrl: "./header.component.html",
  styleUrls: ["./header.component.scss"]
})
export class HeaderComponent implements OnInit {
  private titles: { [id: string]: string } = {
    "/home/map": "Find some pokemons!",
    "/home/arena": "Here you can see all available arenas",
    "/home/arena/details": "Here you can see the players in this arena",
    "/home/profile": "These are all the pokemons that you have",
    "/home/pokedex": "Want to know more about pokemons?",
    "/home/arena/current-battle": "You gotta be the very best!"
  };

  constructor(private router: Router) { }

  ngOnInit() { }

  getTitle(): string {
    return this.titles[this.router.routerState.snapshot.url];
  }
}
