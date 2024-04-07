import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { BaseApiResponse } from '../model/baseApiResponse';
import { AuthResponseDto } from '../model/authResponseDto';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(private httpClient: HttpClient) {}

  private baseUrl = environment.apiUrl;
  private loginUrl = this.baseUrl + 'Account/Login';
  private registerUrl = this.baseUrl + 'Account/Register';
  private refreshTokenUrl = this.baseUrl + 'Account/RefreshToken';

  public apiAccountLogin(
    email: string,
    password: string
  ): Observable<BaseApiResponse<AuthResponseDto>> {
    return this.httpClient.post<BaseApiResponse<AuthResponseDto>>(
      this.loginUrl,
      {
        email: email,
        password: password,
      }
    );
  }

  public apiAccountRegister(
    email: string,
    password: string,
    firstName: string,
    lastName: string
  ): Observable<BaseApiResponse<any>> {
    return this.httpClient.post<BaseApiResponse<any>>(this.registerUrl, {
      email: email,
      password: password,
      firstName: firstName,
      lastName: lastName,
    });
  }

  public apiAccountRefreshToken(
    userId: string,
    token: string,
    refreshToken: string
  ): Observable<BaseApiResponse<AuthResponseDto>> {
    return this.httpClient.post<BaseApiResponse<AuthResponseDto>>(
      this.refreshTokenUrl,
      {
        userId: userId,
        token: token,
        refreshToken: refreshToken,
      }
    );
  }
}
