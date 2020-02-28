import { Component, OnInit } from '@angular/core';
import { ArenaModel } from '../../../models/arena.model';
import { ArenaService } from '../../../core/arena.service';
import { MatDialog } from '@angular/material';
import { PopupComponent } from 'src/app/shared/components/popup/popup.component';
import { Router } from '@angular/router';
import { ToastrService } from '../../../../../shared/core/toastr.service';

@Component({
  selector: 'app-arena',
  templateUrl: './arena.component.html',
  styleUrls: ['./arena.component.scss']
})
export class ArenaComponent implements OnInit {

  public arenas: ArenaModel[];

  constructor(
    private arenaService: ArenaService,
    private dialog: MatDialog,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.arenaService.getArenas().subscribe((data: ArenaModel[]) => {
      this.arenas = data.sort((a, b) => (a.requiredLevel > b.requiredLevel) ? 1 : -1);
    });
  }

  public openPopup(arena: ArenaModel): void {
    const popupRef = this.dialog.open(PopupComponent, {
      data: {
        title: `Join ${arena.name}`,
        content: 'Are you sure you want to join this arena?',
        value: arena.id
      }
    });

    popupRef.afterClosed().subscribe(result => {
      if (result) {
        this.arenaService.joinArena(arena.id).subscribe(
          () => {
            this.toastr.openToastr(`Warping you to ${arena.name}`)
            setTimeout(() => {
              this.router.navigate(['/home/arena/details']);
            }, 500);
          },
          () => {
            console.log('Something went wrong!');
          }
        )
      }
    });
  }

}
