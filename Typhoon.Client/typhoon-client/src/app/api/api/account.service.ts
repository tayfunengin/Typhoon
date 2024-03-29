import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthResponseDtoApiResponse } from '../model/authResponseDtoApiResponse';
import { environment } from '../../../environments/environment.development';
import { BaseApiResponse } from '../model/baseApiResponse';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(private httpClient: HttpClient) {}

  private baseUrl = environment.apiUrl;
  private loginUrl = this.baseUrl + 'Account/Login';
  private registerUrl = this.baseUrl + 'Account/Register';

  public apiAccountLogin(
    email: string,
    password: string
  ): Observable<AuthResponseDtoApiResponse> {
    return this.httpClient.post<AuthResponseDtoApiResponse>(this.loginUrl, {
      email: email,
      password: password,
    });
  }

  public apiAccountRegister(
    email: string,
    password: string,
    firstName: string,
    lastName: string
  ): Observable<BaseApiResponse> {
    return this.httpClient.post<BaseApiResponse>(this.registerUrl, {
      email: email,
      password: password,
      firstName: firstName,
      lastName: lastName,
    });
  }
}
