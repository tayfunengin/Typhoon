import { Injectable } from '@angular/core';
import { NotificationService } from '../core/notification.service';
import { ProductService } from '../api/api/product.service';
import { BehaviorSubject, tap } from 'rxjs';
import { PagedResult } from '../api/model/pagedResult';
import {
  ProductCreateDto,
  ProductDto,
  ProductUpdateDto,
} from '../models/productDto';
import { ProductFilter } from '../models/productFilter';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  private _products = new BehaviorSubject<
    PagedResult<ProductDto[]> | undefined
  >(undefined);
  products$ = this._products.asObservable();

  private _productFilter = new BehaviorSubject<ProductFilter | undefined>(
    undefined
  );
  productFilter$ = this._productFilter.asObservable();

  isloading = false;

  constructor(
    private productService: ProductService,
    private notificationService: NotificationService
  ) {}

  getProductFilter() {
    return this._productFilter.value;
  }

  setProductFilter(filter: ProductFilter) {
    this._productFilter.next(filter);
  }

  getFilteredList(filter: ProductFilter) {
    this.productService
      .apiProductGetFilteredList(
        filter.page,
        filter.recordsToTake,
        filter.orderBy,
        filter.brand,
        filter.category
      )
      .subscribe({
        next: (response) => {
          this._products.next(response.data);
        },
        error: () => {
          this._products.next(undefined);
        },
      });
  }

  create(createDto: ProductCreateDto) {
    return this.productService
      .apiProductPost(
        createDto.name,
        createDto.description,
        createDto.brand,
        createDto.price,
        createDto.categoryId
      )
      .pipe(
        tap((response) => {
          this.notificationService.success(response.message!);
        })
      );
  }

  edit(id: number, updateDto: ProductUpdateDto) {
    return this.productService
      .apiProductPut(
        id,
        updateDto.name,
        updateDto.description,
        updateDto.brand,
        updateDto.price,
        updateDto.categoryId
      )
      .pipe(
        tap((response) => {
          this.notificationService.success(response.message!);
        })
      );
  }
  delete(id: number) {
    this.productService.apiProductDelete(id).subscribe({
      next: (response) => {
        this.notificationService.success(response.message!);
        this.removeProduct(id);
      },
    });
  }

  private removeProduct(id: number) {
    const newProducts = { ...this._products.value };
    const index = newProducts?.items!.findIndex((x) => x.id == id);
    if (index! > -1) {
      newProducts?.items!.splice(index!, 1);
      newProducts.totalCount = newProducts.totalCount! - 1;
      this._products.next(newProducts as PagedResult<ProductDto[]>);
    }
  }
}
