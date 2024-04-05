import { Component, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialogActions,
  MatDialogClose,
  MatDialogTitle,
  MatDialogContent,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-dialog',
  standalone: true,
  imports: [
    MatButtonModule,
    MatDialogActions,
    MatDialogClose,
    MatDialogTitle,
    MatDialogContent,
  ],
  templateUrl: './confirm-dialog.component.html',
  styleUrl: './confirm-dialog.component.scss',
})
export class ConfirmDialogComponent {
  title = 'Delete Action';
  content = 'Are you sure ?';

  constructor(@Inject(MAT_DIALOG_DATA) private data?: ConfirmDialogData) {
    if (data?.title) this.title = data?.title;
    if (data?.content) this.content = data?.content;
  }
}

export interface ConfirmDialogData {
  title?: string;
  content?: string;
}
