import { Component, OnInit } from '@angular/core';
import { ProfileService } from 'src/app/shared/core/profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  public pokemons: any;

  private palette: string[] = ['#00d793', '#C12026', '#58ABF6'];
  constructor(private profileService: ProfileService) { }

  ngOnInit() {
    this.profileService.getAllPokemons().subscribe(data => this.pokemons = data);
  }

  getBackGroundColor(): string {
    return this.palette[this.getRandomInt(3)];
  }

  private getRandomInt(max) {
    return Math.floor(Math.random() * Math.floor(max));
  }
}
