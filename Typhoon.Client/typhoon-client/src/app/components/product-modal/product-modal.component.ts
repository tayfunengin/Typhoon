import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
import { CategoryDto } from '../../models/categoryDto';
import { CategoriesService } from '../../services/categories.service';
import { ProductCreateDto, ProductUpdateDto } from '../../models/productDto';
import { ProductsService } from '../../services/products.service';
import { MatButtonModule } from '@angular/material/button';
import { MatError, MatFormField } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { Subject, takeUntil } from 'rxjs';
import {
  MatOption,
  MatSelect,
  MatSelectModule,
} from '@angular/material/select';
import { MatProgressBarModule } from '@angular/material/progress-bar';

@Component({
  selector: 'app-product-modal',
  standalone: true,
  imports: [
    MatButtonModule,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
    ReactiveFormsModule,
    MatFormField,
    MatInput,
    MatError,
    MatSelect,
    MatOption,
    MatFormField,
    MatSelectModule,
    MatProgressBarModule,
  ],
  templateUrl: './product-modal.component.html',
  styleUrl: './product-modal.component.scss',
})
export class ProductModalComponent implements OnInit, OnDestroy {
  private readonly _destroying = new Subject<void>();

  edit = false;
  form: FormGroup = this.fb.group({
    id: [],
    name: [this.product.name, [Validators.required]],
    description: [
      this.product.description,
      [Validators.required, Validators.maxLength(250)],
    ],
    brand: [this.product.brand, [Validators.required]],
    price: [this.product.price, [Validators.required]],
    categoryId: [this.product.categoryId, [Validators.required]],
  });

  categories: CategoryDto[] = [];
  constructor(
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA)
    private product: ProductUpdateDto | ProductCreateDto,
    private dialogRef: MatDialogRef<ProductModalComponent>,
    private categoriesService: CategoriesService,
    private productsService: ProductsService
  ) {
    if ('id' in product) {
      this.edit = true;
      this.form.patchValue({ id: product.id });
    }
  }

  ngOnInit(): void {
    this.categoriesService
      .getAll()
      .pipe(takeUntil(this._destroying))
      .subscribe((response) => {
        this.categories = response?.sort((a, b) => {
          if (a.name.toUpperCase() < b.name.toUpperCase()) {
            return -1;
          }
          if (a.name.toUpperCase() > b.name.toUpperCase()) {
            return 1;
          }
          return 0;
        })!;
      });
  }

  ngOnDestroy(): void {
    this._destroying.next(undefined);
    this._destroying.complete();
  }

  get isLoading() {
    return this.productsService.isloading;
  }

  get categoryLoading() {
    return this.categoriesService.isloading;
  }

  close() {
    this.dialogRef.close(false);
  }

  save() {
    if (this.edit) {
      this.productsService
        .edit(this.form.value.id!, this.form.value)
        .subscribe({
          next: (response) => {
            if (response.success) this.dialogRef.close(true);
          },
        });
    } else {
      this.productsService.create(this.form.value).subscribe({
        next: (response) => {
          if (response.success) this.dialogRef.close(true);
        },
      });
    }
  }
}
