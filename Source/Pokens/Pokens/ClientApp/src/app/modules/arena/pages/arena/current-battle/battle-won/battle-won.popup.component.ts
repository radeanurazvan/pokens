import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-popup',
  templateUrl: './battle-won-popup.component.html',
  styleUrls: ['./battle-won-popup.component.scss']
})
export class BattleWonPopupComponent implements OnInit {

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<BattleWonPopupComponent>
  ) {
  }

  ngOnInit() {
  }

  public close() {
    this.dialogRef.close();
  }

}
