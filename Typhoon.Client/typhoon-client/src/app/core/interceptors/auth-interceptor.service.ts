import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpHeaders,
  HttpInterceptor,
  HttpRequest,
  HttpStatusCode,
} from '@angular/common/http';
import { Observable, catchError, exhaustMap, take, throwError } from 'rxjs';
import { AccountsService } from '../../services/accounts.service';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { AccountService } from '../../api/api/account.service';
import { BaseApiResponse } from '../../api/model/baseApiResponse';
import { AuthResponseDto } from '../../api/model/authResponseDto';

@Injectable({
  providedIn: 'root',
})
export class AuthInterceptorService implements HttpInterceptor {
  constructor(
    private accountsService: AccountsService,
    private accountService: AccountService
  ) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return this.accountsService.user$.pipe(
      take(1),
      exhaustMap((user) => {
        if (!user) {
          return next.handle(req);
        }
        req = req.clone({
          headers: new HttpHeaders().set(
            'Authorization',
            'Bearer ' + user.token
          ),
        });

        return next.handle(req).pipe(
          catchError((err: any) => {
            if (
              err instanceof HttpErrorResponse &&
              err.status === HttpStatusCode.Unauthorized &&
              !req.url.startsWith(environment.apiUrl + 'Account/refreshToken')
            ) {
              return this.handle401Error(req, next);
            }
            return throwError(() => err);
          })
        );
      })
    );
  }

  private handle401Error(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const user = this.accountsService.getUser();
    return new Observable((observer) => {
      this.accountService
        .apiAccountRefreshToken(
          user?.userId as string,
          user?.token as string,
          user?.refreshToken as string
        )
        .subscribe({
          next: (response: BaseApiResponse<AuthResponseDto>) => {
            this.accountsService.handleAuthentication(response.data!);
            req = req.clone({
              headers: new HttpHeaders().set(
                'Authorization',
                'Bearer ' + response.data?.token
              ),
            });
            next.handle(req).subscribe((event) => {
              observer.next(event);
            });
          },
          error: (err) => {
            observer.error(err);
          },
        });
    });
  }
}
