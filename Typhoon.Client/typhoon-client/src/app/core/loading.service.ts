import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoadingService {
  private readonly _isLoading = new BehaviorSubject<boolean>(false);
  isLoading$ = this._isLoading.asObservable();

  constructor() {}

  get isLoading() {
    return this._isLoading.value;
  }
  setIsLoading(value: boolean) {
    this._isLoading.next(value);
  }
}
