import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ProfileService } from 'src/app/shared/core/profile.service';
import { map, tap } from 'rxjs/operators';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  public pokemons: any[];

  private palette: string[] = ['#F5E97E', '#A2D7D5', '#DF807E', '#55A3AB', '#CC6310'];
  constructor(private profileService: ProfileService) { }

  ngOnInit() {
    this.profileService.getAllPokemons().pipe(
      map(data => data.map(x => { x.color = this.getBackGroundColor(x.name.charCodeAt(0)); return x; })),
      tap(data => this.pokemons = data)
    ).subscribe();
  }

  getBackGroundColor(code: number): string {
    return this.palette[this.getRandomInt(code)];
  }

  private getRandomInt(code: number) {
    return Math.floor(code % 5);
  }
}
