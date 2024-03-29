import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  private DURATION = 5000;

  constructor(private _snackBar: MatSnackBar) {}

  error(message: string) {
    this._snackBar.open(message, 'X', {
      panelClass: ['snackbar-error'],
      duration: this.DURATION,
      verticalPosition: 'top',
      horizontalPosition: 'end',
    });
  }

  info(message: string, duration?: number) {
    return this._snackBar.open(message, 'X', {
      panelClass: ['snackbar-info'],
      duration: duration ? duration : this.DURATION,
      verticalPosition: 'top',
      horizontalPosition: 'end',
    });
  }

  success(message: string) {
    return this._snackBar.open(message, 'X', {
      panelClass: ['snackbar-success'],
      duration: this.DURATION,
      verticalPosition: 'top',
      horizontalPosition: 'end',
    });
  }

  warning(message: string) {
    return this._snackBar.open(message, 'X', {
      panelClass: ['snackbar-warning'],
      duration: this.DURATION,
      verticalPosition: 'top',
      horizontalPosition: 'end',
    });
  }
}
