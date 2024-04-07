import { NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AccountsService } from '../../services/accounts.service';
import { UserDto } from '../../models/userDto';
import { LoadingService } from '../../core/loading.service';

@Component({
  selector: 'app-register',
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
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent implements OnInit {
  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private accountsService: AccountsService,
    private loadingService: LoadingService
  ) {}
  ngOnInit(): void {
    this.form = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
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
      secondPassword: [
        '',
        [Validators.required, this.checkPasswordValue.bind(this)],
      ],
    });
  }

  get isLoading() {
    return this.loadingService.isLoading;
  }

  checkPasswordValue(control: FormControl): { [prop: string]: boolean } | null {
    let passwordValue = this.form?.get('password')?.value;
    if (passwordValue != control?.value) {
      return { secondPassword: true };
    }
    return null;
  }

  submit() {
    this.accountsService.register(this.form.value as UserDto);
    this.form.reset();
  }
}
