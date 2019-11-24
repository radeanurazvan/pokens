import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../../core/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginFormGroup: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private loginPageService: AuthService,
    private router: Router) { }

  public ngOnInit(): void {
    this.initForm();
  }

  public login(): void {
    if (this.loginFormGroup.valid) {
      this.loginPageService.login(this.loginFormGroup.getRawValue()).subscribe(
        () => {
          this.loggedIn();
        },
        () => {
          console.log('Fail!');
        });
    }
  }

  public loggedIn(): void {
    this.router.navigateByUrl('/home');
  }

  public goToRegister(): void {
    this.router.navigateByUrl('/register');
  }

  private initForm(): void {
    this.loginFormGroup = this.formBuilder.group({
      email: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required])
    });
  }
}
