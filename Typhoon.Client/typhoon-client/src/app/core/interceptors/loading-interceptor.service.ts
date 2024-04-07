import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, finalize } from 'rxjs';
import { LoadingService } from '../loading.service';
import { CategoriesService } from '../../services/categories.service';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class LoadingInterceptorService implements HttpInterceptor {
  constructor(
    private loadingService: LoadingService,
    private categoriesService: CategoriesService
  ) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (req.url.startsWith(environment.apiUrl + 'Category/getAll')) {
      this.categoriesService.setIsLoading(true);
    } else {
      this.loadingService.setIsLoading(true);
    }
    return next.handle(req).pipe(
      finalize(() => {
        if (req.url.startsWith(environment.apiUrl + 'Category/getAll')) {
          this.categoriesService.setIsLoading(false);
        } else {
          this.loadingService.setIsLoading(false);
        }
      })
    );
  }
}
