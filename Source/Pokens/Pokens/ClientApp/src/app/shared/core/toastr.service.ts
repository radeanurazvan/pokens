import { Injectable } from '@angular/core';
import { ToastrService as ExternalToastr } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ToastrService {

  constructor(private toastr: ExternalToastr) { }

  openToastr(message: string): void {
    this.toastr.info(message);
  }
}
