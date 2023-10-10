import { Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/services/authentication.service';
import { Auth } from 'src/app/interfaces/auth';
import { MatDialog } from '@angular/material/dialog';
import { RegisterComponent } from '../register/register.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email = new FormControl('', [Validators.required, Validators.email]);
  password = new FormControl("", [Validators.required, Validators.minLength(6), Validators.pattern("RegExp here")]);

  constructor(private authService: AuthenticationService, private matDialog: MatDialog, private router: Router) {
  }

  registerLogin() {
    this.matDialog.open(RegisterComponent);
  }

  submitLogin() {
    let login: Auth = {email: this.email.value!, password: this.password.value!};
    this.authService.getToken(login);
  }

  getEmailErrorMessage() {
    if (this.email.hasError('required')) {
      return 'You must enter a value';
    }

    return this.email.hasError('email') ? 'Not a valid email' : '';
  }

  getPasswordErrorMessage() {
    if (this.password.hasError('required')) {
      return 'You must enter a value';
    }
    else if (this.password.hasError("pattern")) {
      return 'Password must contain number, special character,\nuppercase and lowercase characters'
    }

    return this.password.hasError('minlength') ? 'Must be atleast 6 characters' : '';
  }
}
