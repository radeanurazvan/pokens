import { Component, OnInit } from '@angular/core';
import { ArenaService } from '../../core/arena.service';
import { ArenaModel } from '../../models/arena.model';
import { MatDialog, MatIconRegistry } from '@angular/material';
import { TrainerModel } from '../../models/trainer.model';
import { ChallengePopupComponent } from './challenge-popup/challenge-popup.component';

@Component({
  selector: 'app-arena-details',
  templateUrl: './arena-details.component.html',
  styleUrls: ['./arena-details.component.scss']
})
export class ArenaDetailsComponent implements OnInit {
  public arenaDetails: ArenaModel;
  public trainersIds: string[];
  public pokemons: any = [];

  constructor(
    private arenaService: ArenaService,
    private dialog: MatDialog) { }

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

  public initChallenge(trainer: TrainerModel, pokemon: any) {
    this.dialog.open(ChallengePopupComponent, {
      data: {arena: this.arenaDetails, trainer, pokemon},
      width: '500px'
    });
  }
}
