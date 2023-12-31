import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AuthenticationService } from 'src/services/authentication.service';
import { Auth } from 'src/app/interfaces/auth'
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  // Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character
  regex = "^((?=.*\\d)|(?=.*[^a-zA-Z0-9]))+(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$";
  email = new FormControl('', [Validators.required, Validators.email]);
  password = new FormControl("", [Validators.required, Validators.minLength(8), Validators.pattern(this.regex)]);
  confirmPassword = new FormControl("", [Validators.required, Validators.minLength(8), Validators.pattern(this.regex)]);

  constructor(private matDialogRef: MatDialogRef<RegisterComponent>, private authService: AuthenticationService, private router: Router) { }

  submitRegistration() {
    let login: Auth = {email: this.email.value!, password: this.password.value!};
    this.authService.register(login);
    this.router.navigateByUrl("/User/Tournaments");
    this.matDialogRef.close();
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
      return 'Password must contain at least one uppercase letter, one lowercase letter, one number or one special character'
    }

    return this.password.hasError('minlength') ? 'Must be atleast 6 characters' : '';
  }

  closeDialog() {
    this.matDialogRef.close(false);
  }
}
