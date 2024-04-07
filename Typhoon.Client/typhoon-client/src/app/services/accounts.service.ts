import { Injectable } from '@angular/core';
import { AccountService } from '../api/api/account.service';
import { BehaviorSubject } from 'rxjs';
import { AuthResponseDto } from '../api/model/authResponseDto';
import { Router } from '@angular/router';
import { UserDto } from '../models/userDto';
import { NotificationService } from '../core/notification.service';

@Injectable({
  providedIn: 'root',
})
export class AccountsService {
  constructor(
    private accountService: AccountService,
    private router: Router,
    private notificationService: NotificationService
  ) {}

  private _user = new BehaviorSubject<AuthResponseDto | undefined>(undefined);
  user$ = this._user.asObservable();

  getUser() {
    return this._user.value;
  }

  login(email: string, password: string) {
    this.accountService.apiAccountLogin(email, password).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.handleAuthentication(response.data);
        }
        this.router.navigate(['/']);
      },
    });
  }

  private handleAuthentication(authResponseDto: AuthResponseDto) {
    this._user.next(authResponseDto);
    localStorage.setItem('typUserData', JSON.stringify(authResponseDto));
  }

  register(userDto: UserDto) {
    this.accountService
      .apiAccountRegister(
        userDto.email,
        userDto.password,
        userDto.firstName,
        userDto.lastName
      )
      .subscribe({
        next: (response) => {
          this.notificationService.success(response.message!);
          this.router.navigate(['/login']);
        },
      });
  }

  autoLogin() {
    const userData: AuthResponseDto = JSON.parse(
      localStorage.getItem('typUserData') as string
    );
    if (!userData) {
      return;
    }

    this._user.next(userData);
  }

  logout() {
    this._user.next(undefined);
    localStorage.removeItem('typUserData');
    this.router.navigate(['/login']);
  }
}
