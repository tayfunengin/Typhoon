import { Component, OnDestroy, OnInit } from '@angular/core';
import { CategoryDto } from '../../models/categoryDto';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { CategoriesService } from '../../services/categories.service';
import { CategoryFilter } from '../../models/categoryFilter';
import { Subject, debounceTime, distinctUntilChanged, takeUntil } from 'rxjs';
import { DataTableComponent } from '../../shared/data-table/data-table/data-table.component';
import { MatButton } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CategoryModalComponent } from '../../components/category-modal/category-modal.component';
import { AsyncPipe } from '@angular/common';
import { ConfirmDialogService } from '../../core/confirm-dialog.service';
import { LoadingService } from '../../core/loading.service';
@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [
    DataTableComponent,
    MatButton,
    MatCardModule,
    MatInputModule,
    ReactiveFormsModule,
    AsyncPipe,
  ],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.scss',
})
export class CategoryListComponent implements OnInit, OnDestroy {
  private readonly _destroying = new Subject<void>();

  displayedColumns: string[] = ['name', 'description', 'actions'];

  data: CategoryDto[] = [];
  dataLength = 0;
  pageSize = 0;

  form = this.fb.group({
    searchText: [''],
  });

  constructor(
    private categoriesService: CategoriesService,
    private fb: FormBuilder,
    public dialog: MatDialog,
    private confirmDialogService: ConfirmDialogService,
    private loadingService: LoadingService
  ) {}

  ngOnInit(): void {
    this.categoriesService.getFilteredList(new CategoryFilter());

    this.categoriesService.categories$
      .pipe(takeUntil(this._destroying))
      .subscribe((pagedResult) => {
        this.data = pagedResult?.items.slice()!;
        this.dataLength = pagedResult?.totalCount!;
        this.pageSize = pagedResult?.pageSize!;
      });

    this.categoriesService.categoryFilter$
      .pipe(takeUntil(this._destroying))
      .subscribe((filter) => {
        if (filter) this.categoriesService.getFilteredList(filter);
      });

    this.form.controls.searchText.valueChanges
      .pipe(debounceTime(1000), distinctUntilChanged())
      .subscribe((value) => {
        this.setSearchText(value!);
      });
  }

  ngOnDestroy(): void {
    this._destroying.next(undefined);
    this._destroying.complete();
  }

  get isLoading() {
    return this.loadingService.isLoading;
  }

  private setSearchText(value: string) {
    let filter = this.categoriesService.getCategoryFilter();
    const newFilter: CategoryFilter = {
      ...(filter ?? new CategoryFilter()),
      categoryName: value!,
    };
    this.categoriesService.setCategoryFilter(newFilter);
  }

  sortChange(sort: Sort) {
    let filter = this.categoriesService.getCategoryFilter();
    const newFilter: CategoryFilter = {
      ...(filter ?? new CategoryFilter()),
      page: 1,
      orderBy: sort.active + ' ' + sort.direction,
    };
    this.categoriesService.setCategoryFilter(newFilter);
  }

  paginationChange(pageEvent: PageEvent) {
    let filter = this.categoriesService.getCategoryFilter();
    const newFilter: CategoryFilter = {
      ...(filter ?? new CategoryFilter()),
      page: pageEvent.pageIndex + 1,
      recordsToTake: pageEvent.pageSize,
    };
    this.categoriesService.setCategoryFilter(newFilter);
  }

  onEditClicked(item: CategoryDto) {
    const config = new MatDialogConfig();

    config.disableClose = true;
    config.autoFocus = true;

    config.data = {
      ...item,
    };

    const dialogRef = this.dialog.open(CategoryModalComponent, config);

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed', result);
    });
  }

  addNew() {
    const config = new MatDialogConfig();

    config.disableClose = true;
    config.autoFocus = true;

    config.data = {
      ...{ name: '', description: '' },
    };

    const dialogRef = this.dialog.open(CategoryModalComponent, config);

    dialogRef.afterClosed().subscribe((result) => {
      if (result) this.categoriesService.getFilteredList(new CategoryFilter());
    });
  }

  onDeleteClicked(item: CategoryDto) {
    this.confirmDialogService.confirmDialog(
      () => this.deleteCategory(item.id),
      'Category Delete Action'
    );
  }

  deleteCategory(id: number) {
    this.categoriesService.delete(id);
  }
}
