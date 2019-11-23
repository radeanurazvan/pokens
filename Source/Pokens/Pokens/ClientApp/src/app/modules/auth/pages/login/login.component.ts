import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { LoginService } from '../../core/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginFormGroup: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private loginPageService: LoginService,
    private router: Router,
    private activatedRoute: ActivatedRoute) { }

  public ngOnInit(): void {
    this.initForm();
  }

  public login(): void {
    if (this.loginFormGroup.valid) {
      if (this.loginPageService.login(this.loginFormGroup.getRawValue())) {
        this.loggedIn();
      }
    }
  }

  public loggedIn(): void {
    // this.activatedRoute.
    this.router.navigateByUrl('/home');
  }

  public goToRegister(): void {
    this.router.navigateByUrl('/register');
  }

  private initForm(): void {
    this.loginFormGroup = this.formBuilder.group({
      username: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required])
    });
  }
}
