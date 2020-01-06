import { Component, OnInit } from '@angular/core';
import { ProfileService } from 'src/app/shared/core/profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  public pokemons: any;

  constructor(private profileService: ProfileService) { }

  ngOnInit() {
    this.profileService.getAllPokemons().subscribe(data => this.pokemons = data);
  }

}
