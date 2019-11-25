import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../../core/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  public registerFormGroup: FormGroup

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService
  ) { }

  public ngOnInit() {
    this.formInit();
  }

  public formInit() {
    this.registerFormGroup = this.formBuilder.group({
      email: new FormControl(null, [Validators.required]),
      name: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required])
    });
  }

  public register() {
    this.authService.register(this.registerFormGroup.getRawValue()).subscribe(
      () => {
        this.router.navigateByUrl('/auth/login');
      },
      () => {
        console.log("Fail!");
      }
    )
  }

}
