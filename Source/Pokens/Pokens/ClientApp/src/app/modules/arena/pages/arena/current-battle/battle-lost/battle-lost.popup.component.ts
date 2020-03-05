import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-popup',
  templateUrl: './battle-lost-popup.component.html',
  styleUrls: ['./battle-lost-popup.component.scss']
})
export class BattleLostPopupComponent implements OnInit {

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<BattleLostPopupComponent>
  ) {
  }

  ngOnInit() {
  }

  public close() {
    this.dialogRef.close();
  }

}
