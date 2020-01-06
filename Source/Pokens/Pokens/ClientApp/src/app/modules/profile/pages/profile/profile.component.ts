import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../../core/profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  constructor(private profileService: ProfileService) { }

  ngOnInit() {
    this.profileService.getAllPokemons().subscribe((data) => console.log(data));
  }

}
