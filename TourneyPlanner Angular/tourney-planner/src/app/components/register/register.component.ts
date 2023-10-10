import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AuthenticationService } from 'src/services/authentication.service';
import { Auth } from 'src/app/interfaces/auth'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  // Minimum six characters, at least one uppercase letter, one lowercase letter, one number and one special character
  regex = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$";
  email = new FormControl('', [Validators.required, Validators.email]);
  password = new FormControl("", [Validators.required, Validators.minLength(6), Validators.pattern(this.regex)]);
  confirmPassword = new FormControl("", [Validators.required, Validators.minLength(6), Validators.pattern(this.regex)]);

  constructor(private matDialogRef: MatDialogRef<RegisterComponent>, private authService: AuthenticationService) { }

  submitRegistration() {
    let login: Auth = {email: this.email.value!, password: this.password.value!};
    this.authService.register(login);
    this.closeDialog();
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
      return 'Password must contain at least one uppercase letter, one lowercase letter, one number and one special character'
    }

    return this.password.hasError('minlength') ? 'Must be atleast 6 characters' : '';
  }

  closeDialog() {
    this.matDialogRef.close(false);
  }
}
