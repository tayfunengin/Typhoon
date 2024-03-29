import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AccountsService } from './services/accounts.service';
import { HeaderComponent } from './components/header/header.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  constructor(private authsService: AccountsService) {}

  ngOnInit(): void {
    this.authsService.autoLogin();
  }
}
