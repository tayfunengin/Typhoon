import { Injectable } from '@angular/core';
import { CategoryService } from '../api/api/category.service';
import { NotificationService } from '../core/notification.service';
import { BehaviorSubject, Observable, Subject, finalize, tap } from 'rxjs';
import { CategoryDto } from '../models/categoryDto';
import { CategoryFilter } from '../models/categoryFilter';
import { PagedResult } from '../api/model/pagedResult';

@Injectable({
  providedIn: 'root',
})
export class CategoriesService {
  constructor(
    private categoryService: CategoryService,
    private notificationService: NotificationService
  ) {}

  private _categories = new BehaviorSubject<
    PagedResult<CategoryDto[]> | undefined
  >(undefined);
  categories$ = this._categories.asObservable();

  private _categoryFilter = new BehaviorSubject<CategoryFilter | undefined>(
    undefined
  );
  categoryFilter$ = this._categoryFilter.asObservable();

  isloading = false;

  getCategoryFilter() {
    return this._categoryFilter.value;
  }

  setCategoryFilter(filter: CategoryFilter) {
    this._categoryFilter.next(filter);
  }

  getFilteredList(filter: CategoryFilter) {
    this.isloading = true;
    this.categoryService
      .apiCategoryGetFilteredList(
        filter.page,
        filter.recordsToTake,
        filter.orderBy,
        filter.categoryName
      )
      .pipe(finalize(() => (this.isloading = false)))
      .subscribe({
        next: (response) => {
          this._categories.next(response.data);
        },
        error: () => {
          this._categories.next(undefined);
        },
      });
  }
  getAll(): Observable<CategoryDto[] | undefined> {
    this.isloading = true;
    const _categories = new Subject<CategoryDto[] | undefined>();
    this.categoryService
      .apiCategoryGetAll()
      .pipe(finalize(() => (this.isloading = false)))
      .subscribe({
        next: (response) => {
          _categories.next(response.data);
        },
        error: () => {
          _categories.next(undefined);
        },
      });

    return _categories.asObservable();
  }
  create(name: string, desc: string) {
    this.isloading = true;
    return this.categoryService.apiCategoryPost(name, desc).pipe(
      tap((response) => {
        this.notificationService.success(response.message!);
      }),
      finalize(() => (this.isloading = false))
    );
  }
  edit(id: number, categoryDto: CategoryDto) {
    this.isloading = true;
    return this.categoryService
      .apiCategoryPut(id, categoryDto.name, categoryDto.description)
      .pipe(
        tap((response) => {
          this.notificationService.success(response.message!);
          this.updateCategory(response.data!);
        }),
        finalize(() => (this.isloading = false))
      );
  }
  delete(id: number) {
    this.isloading = true;
    this.categoryService
      .apiCategoryDelete(id)
      .pipe(finalize(() => (this.isloading = false)))
      .subscribe({
        next: (response) => {
          this.notificationService.success(response.message!);
          this.removeCategory(id);
        },
      });
  }

  private updateCategory(dto: CategoryDto) {
    const newCategories = { ...this._categories.value };
    const index = newCategories?.items!.findIndex((x) => x.id == dto.id);
    if (index! > -1) {
      newCategories?.items!.splice(index!, 1, dto);
    } else {
      return;
    }
    this._categories.next(newCategories as PagedResult<CategoryDto[]>);
  }

  private removeCategory(id: number) {
    const newCategories = { ...this._categories.value };
    const index = newCategories?.items!.findIndex((x) => x.id == id);
    if (index! > -1) {
      newCategories?.items!.splice(index!, 1);
      newCategories.totalCount = newCategories.totalCount! - 1;
      this._categories.next(newCategories as PagedResult<CategoryDto[]>);
    }
  }
}
