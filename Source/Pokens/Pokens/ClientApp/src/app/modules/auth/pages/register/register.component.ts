import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  public registerFormGroup: FormGroup

  constructor(
    private formBuilder: FormBuilder,
    private router: Router
  ) { }

  public ngOnInit() {
    this.formInit();
  }

  public formInit() {
    this.registerFormGroup = this.formBuilder.group({
      email: new FormControl(null, [Validators.required]),
      username: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required])
    });
  }

  public register() {
    this.router.navigateByUrl('/login');
  }

}
