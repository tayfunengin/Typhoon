import {
  HttpErrorResponse,
  HttpEvent,
  HttpEventType,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpStatusCode,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, tap, throwError } from 'rxjs';
import { NotificationService } from '../notification.service';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment.development';
import { AccountsService } from '../../services/accounts.service';

@Injectable({
  providedIn: 'root',
})
export class ErrorInterceptorService implements HttpInterceptor {
  constructor(
    private notificationService: NotificationService,
    private router: Router,
    private accountsService: AccountsService
  ) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      tap((response: HttpEvent<any>) => {
        if (
          response.type == HttpEventType.Response &&
          !response.body.success &&
          response.body.message
        )
          this.notificationService.error(response.body.message);
      }),
      catchError((error: HttpErrorResponse) => {
        this.handleError(error, req);
        return throwError(() => error);
      })
    );
  }

  private handleError(error: HttpErrorResponse, req: HttpRequest<any>) {
    const statusCode = error.status as HttpStatusCode;

    switch (statusCode) {
      case HttpStatusCode.BadRequest:
        let errorMessage = '';
        if (error.error && typeof error.error === 'object')
          errorMessage = Object.values(error.error).join(', ');
        else errorMessage = error.error;

        this.notificationService.error(errorMessage ?? error.message);
        break;
      case HttpStatusCode.NotFound:
        this.notificationService.error(error.error ?? error.message);
        this.router.navigateByUrl('404');
        break;
      case HttpStatusCode.Unauthorized:
        if (req.url.startsWith(environment.apiUrl + 'Account/refreshToken')) {
          this.accountsService.logout();
        }
        break;
      case HttpStatusCode.InternalServerError:
        this.notificationService.error(error.error ?? error.message);
        break;
      default:
        this.notificationService.error('An unknown error occurred.');
        break;
    }
  }
}
