import { Component, Inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CategoryDto } from '../../models/categoryDto';
import { MatButtonModule } from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialogRef,
  MatDialogTitle,
  MatDialogContent,
  MatDialogActions,
  MatDialogClose,
} from '@angular/material/dialog';
import { MatFormField } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { CategoriesService } from '../../services/categories.service';
import { withLatestFrom } from 'rxjs';

@Component({
  selector: 'app-category-modal',
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
  ],
  templateUrl: './category-modal.component.html',
  styleUrl: './category-modal.component.scss',
})
export class CategoryModalComponent {
  edit = false;
  form = this.fb.group({
    id: [this.category.id],
    name: [this.category.name, [Validators.required, Validators.maxLength(50)]],
    description: [this.category.description, Validators.required],
  });

  constructor(
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) private category: CategoryDto,
    private dialogRef: MatDialogRef<CategoryModalComponent>,
    private categoriesService: CategoriesService
  ) {
    this.edit = category.id != null;
  }

  get isLoading() {
    return this.categoriesService.isloading;
  }

  close() {
    this.dialogRef.close(false);
  }

  save() {
    if (this.edit) {
      this.categoriesService
        .edit(this.form.value.id!, this.form.value as CategoryDto)
        .subscribe({
          next: (response) => {
            if (response.success) this.dialogRef.close(true);
          },
        });
    } else {
      this.categoriesService
        .create(this.form.value.name!, this.form.value.description!)
        .subscribe({
          next: (response) => {
            if (response.success) this.dialogRef.close(true);
          },
        });
    }
  }
}
