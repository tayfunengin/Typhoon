import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatFormFieldModule } from '@angular/material/form-field';
import { NgIf } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { AccountsService } from '../../services/accounts.service';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    NgIf,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent implements OnInit {
  form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: [
      '',
      [
        Validators.required,
        Validators.pattern(
          /(?=[A-Za-z0-9@#$%^&+!=]+$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&+!=])(?=.{8,}).*$/
        ),
      ],
    ],
  });

  constructor(
    private fb: FormBuilder,
    private accountsService: AccountsService
  ) {}

  ngOnInit(): void {}

  get isLoading() {
    return this.accountsService.isloading;
  }

  submit() {
    const email = this.form.get('email')?.value!;
    const password = this.form.get('password')?.value!;
    this.accountsService.login(email, password);

    this.form.reset();
  }
}
