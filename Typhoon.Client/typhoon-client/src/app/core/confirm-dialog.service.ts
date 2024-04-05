import { Injectable } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import {
  ConfirmDialogComponent,
  ConfirmDialogData,
} from '../shared/confirm-dialog/confirm-dialog.component';

@Injectable({
  providedIn: 'root',
})
export class ConfirmDialogService {
  constructor(private dialog: MatDialog) {}

  confirmDialog(
    onConfirmCallBack: () => void,
    title?: string,
    content?: string
  ) {
    const config = new MatDialogConfig();
    config.autoFocus = true;
    let dialogData: ConfirmDialogData = {
      title: title,
      content: content,
    };
    config.data = {
      ...dialogData,
    };

    const dialogRef = this.dialog.open(ConfirmDialogComponent, config);

    dialogRef.afterClosed().subscribe((result) => {
      if (result) onConfirmCallBack();
    });
  }
}
