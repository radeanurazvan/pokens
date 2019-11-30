import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class ToastrService {

  constructor(private snackBar: MatSnackBar) { }

  openToastr(message: string): void {
    this.snackBar.open(message, 'Ok', {
      duration: 5000,
      panelClass: 'toast'
    });
  }
}
