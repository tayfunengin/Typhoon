import {
  HttpEvent,
  HttpHandler,
  HttpHeaders,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Observable, exhaustMap, take } from 'rxjs';
import { AccountsService } from '../../services/accounts.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthInterceptorService implements HttpInterceptor {
  constructor(private authsService: AccountsService) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return this.authsService.user$.pipe(
      take(1),
      exhaustMap((user) => {
        if (!user) {
          return next.handle(req);
        }
        const modifiedReq = req.clone({
          headers: new HttpHeaders().set('Bearer', user.token),
        });
        return next.handle(modifiedReq);
      })
    );
  }
}
