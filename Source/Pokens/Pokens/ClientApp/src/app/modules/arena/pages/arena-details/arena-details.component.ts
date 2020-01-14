import { Component, OnInit } from '@angular/core';
import { ArenaService } from '../../core/arena.service';
import { ArenaModel } from '../../models/arena.model';

@Component({
  selector: 'app-arena-details',
  templateUrl: './arena-details.component.html',
  styleUrls: ['./arena-details.component.scss']
})
export class ArenaDetailsComponent implements OnInit {

  public arenaDetails: ArenaModel;
  public trainersIds: string[];
  private pokemons: any;

  constructor(private arenaService: ArenaService) { }

  ngOnInit() {
    this.arenaService.getArenaDetails().subscribe((data: ArenaModel) => {
      this.arenaDetails = data;
      this.trainersIds = this.arenaDetails.trainers.map(a => a.id);

      this.arenaService.getAllPokemons(this.trainersIds).subscribe((pokemons: any) => this.pokemons = pokemons);
    });
  }

  getPokemons(id: string): any {
    return this.pokemons.filter(p => p.trainerId === id);
  }
}
