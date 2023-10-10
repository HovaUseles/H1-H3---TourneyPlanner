import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

// Doesn't work
export function requireMatchValidator(password: string, confirmPassword: string): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (control.value != null || control.value != "" || control.value != undefined) {
      if (password == confirmPassword) {
        return control;
      }
    }
    return null;
  };
};
