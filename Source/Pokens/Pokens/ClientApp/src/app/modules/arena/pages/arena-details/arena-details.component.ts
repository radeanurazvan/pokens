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

  constructor(private arenaService: ArenaService) { }

  ngOnInit() {
    this.arenaService.getArenaDetails().subscribe((data: ArenaModel) => {
      this.arenaDetails = data;
    });
  }

}
