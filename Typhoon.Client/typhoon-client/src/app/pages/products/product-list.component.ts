import { Component, OnDestroy, OnInit } from '@angular/core';
import { ProductsService } from '../../services/products.service';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ConfirmDialogService } from '../../core/confirm-dialog.service';
import { AsyncPipe } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { DataTableComponent } from '../../shared/data-table/data-table/data-table.component';
import { Subject, debounceTime, distinctUntilChanged, takeUntil } from 'rxjs';
import {
  ProductCreateDto,
  ProductDto,
  ProductUpdateDto,
} from '../../models/productDto';
import { ProductFilter } from '../../models/productFilter';
import { Sort } from '@angular/material/sort';
import { PageEvent } from '@angular/material/paginator';
import { ProductModalComponent } from '../../components/product-modal/product-modal.component';
import { LoadingService } from '../../core/loading.service';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [
    DataTableComponent,
    MatButton,
    MatCardModule,
    MatInputModule,
    ReactiveFormsModule,
    AsyncPipe,
  ],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss',
})
export class ProductListComponent implements OnInit, OnDestroy {
  private readonly _destroying = new Subject<void>();
  form = this.fb.group({
    brand: [''],
    category: [''],
  });

  displayedColumns: string[] = [
    'name',
    'description',
    'brand',
    'price',
    'category',
    'actions',
  ];

  data: ProductDto[] = [];
  dataLength = 0;
  pageSize = 0;

  constructor(
    private productsService: ProductsService,
    private fb: FormBuilder,
    public dialog: MatDialog,
    private confirmDialogService: ConfirmDialogService,
    private loadingService: LoadingService
  ) {}

  ngOnInit(): void {
    this.productsService.getFilteredList(new ProductFilter());

    this.productsService.products$
      .pipe(takeUntil(this._destroying))
      .subscribe((pagedResult) => {
        this.data = pagedResult?.items.slice()!;
        this.dataLength = pagedResult?.totalCount!;
        this.pageSize = pagedResult?.pageSize!;
      });

    this.productsService.productFilter$
      .pipe(takeUntil(this._destroying))
      .subscribe((filter) => {
        if (filter) this.productsService.getFilteredList(filter);
      });

    this.form.controls.brand.valueChanges
      .pipe(debounceTime(2000), distinctUntilChanged())
      .subscribe((value) => {
        this.setBrandOrCompany(value!, true);
      });
    this.form.controls.category.valueChanges
      .pipe(debounceTime(2000), distinctUntilChanged())
      .subscribe((value) => {
        this.setBrandOrCompany(value!, false);
      });
  }

  ngOnDestroy(): void {
    this._destroying.next(undefined);
    this._destroying.complete();
  }

  get isLoading() {
    return this.loadingService.isLoading;
  }

  private setBrandOrCompany(value: string, brand: boolean) {
    let filter = this.productsService.getProductFilter();
    let newFilter: ProductFilter;
    if (brand) {
      newFilter = {
        ...(filter ?? new ProductFilter()),
        brand: value!,
      };
    } else {
      newFilter = {
        ...(filter ?? new ProductFilter()),
        category: value!,
      };
    }

    this.productsService.setProductFilter(newFilter);
  }

  sortChange(sort: Sort) {
    let filter = this.productsService.getProductFilter();
    const newFilter: ProductFilter = {
      ...(filter ?? new ProductFilter()),
      page: 1,
      orderBy: sort.active + ' ' + sort.direction,
    };
    this.productsService.setProductFilter(newFilter);
  }

  paginationChange(pageEvent: PageEvent) {
    let filter = this.productsService.getProductFilter();
    const newFilter: ProductFilter = {
      ...(filter ?? new ProductFilter()),
      page: pageEvent.pageIndex + 1,
      recordsToTake: pageEvent.pageSize,
    };
    this.productsService.setProductFilter(newFilter);
  }

  onEditClicked(item: ProductDto) {
    const config = new MatDialogConfig();

    config.disableClose = true;
    config.autoFocus = true;
    const editDto: ProductUpdateDto = {
      id: item.id,
      name: item.name,
      description: item.description,
      price: item.price,
      brand: item.brand,
      categoryId: item.category.id,
    };
    config.data = {
      ...editDto,
    };
    const dialogRef = this.dialog.open(ProductModalComponent, config);
    dialogRef.afterClosed().subscribe((result) => {
      if (result) this.productsService.getFilteredList(new ProductFilter());
    });
  }

  addNew() {
    const config = new MatDialogConfig();

    config.disableClose = true;
    config.autoFocus = true;

    const product: ProductCreateDto = {
      name: '',
      description: '',
      price: 0,
      brand: '',
      categoryId: 0,
    };
    config.data = {
      ...product,
    };

    const dialogRef = this.dialog.open(ProductModalComponent, config);

    dialogRef.afterClosed().subscribe((result) => {
      if (result) this.productsService.getFilteredList(new ProductFilter());
    });
  }

  onDeleteClicked(item: ProductDto) {
    this.confirmDialogService.confirmDialog(
      () => this.deleteProduct(item.id),
      undefined,
      `Do you want to delete ${item.name} ?`
    );
  }

  deleteProduct(id: number) {
    this.productsService.delete(id);
  }
}
