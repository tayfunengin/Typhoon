import { Component, OnDestroy, OnInit } from '@angular/core';
import { AccountsService } from '../../services/accounts.service';
import { Subject, takeUntil } from 'rxjs';
import { MatIconModule } from '@angular/material/icon';
import { NgIf } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [MatIconModule, NgIf, MatToolbarModule, RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit, OnDestroy {
  private readonly _destroying = new Subject<void>();
  isLoggedIn = false;

  constructor(private accountsService: AccountsService) {}

  ngOnInit(): void {
    this.accountsService.user$
      .pipe(takeUntil(this._destroying))
      .subscribe((user) => {
        this.isLoggedIn = user != null;
      });
  }

  ngOnDestroy(): void {
    this._destroying.next(undefined);
    this._destroying.complete();
  }

  logout() {
    this.accountsService.logout();
  }
}
