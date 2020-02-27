import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { ProfileService } from '../../../../../shared/core/profile.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ArenaService } from '../../../core/arena.service';
import { ToastrService } from '../../../../../shared/core/toastr.service';

@Component({
  templateUrl: './challenge-popup.component.html',
  styleUrls: ['./challenge-popup.component.scss']
})
export class ChallengePopupComponent implements OnInit {
  public myPokemons: any[] = [];
  public selectedPokemon: any;

  constructor(
    private service: ProfileService,
    private snack: MatSnackBar,
    private arenaService: ArenaService,
    private dialogRef: MatDialogRef<ChallengePopupComponent>,
    private toast: ToastrService,
    @Inject(MAT_DIALOG_DATA) public data: any) {
  }

  public ngOnInit(): void {
    this.service.getAllPokemons().subscribe(p => this.myPokemons = p);
  }

  public choosePokemon(pokemon: any) {
    this.selectedPokemon = pokemon;
    this.snack.open(`${pokemon.name}, I choose you!`, null, {
      duration: 2000
    });
  }

  public reset() {
    this.selectedPokemon = null;
  }

  public sendChallenge() {
    this.arenaService.challenge(this.data.arena, this.data.trainer.id, this.data.pokemon.id, this.selectedPokemon.id).subscribe(() => {
      this.dialogRef.close();
      this.toast.openToastr(`You successfully challenged ${this.data.trainer.name}!`);
    });
  }
}
