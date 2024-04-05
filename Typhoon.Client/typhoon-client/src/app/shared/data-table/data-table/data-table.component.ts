import { NgFor, NgIf, NgTemplateOutlet, TitleCasePipe } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  Output,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import {
  MatPaginator,
  MatPaginatorModule,
  PageEvent,
} from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSortModule, Sort } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-data-table',
  standalone: true,
  imports: [
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatProgressBarModule,
    NgIf,
    NgFor,
    NgTemplateOutlet,
    TitleCasePipe,
    MatButton,
    MatIcon,
  ],
  templateUrl: './data-table.component.html',
  styleUrl: './data-table.component.scss',
})
export class DataTableComponent {
  @Input() addNewButton = true;
  @Input() tableName = '';
  @Input() dataLength = 0;
  @Input() pageSize = 0;
  @Input() isLoading = false;
  @Input() displayedColumns: string[] = [];
  @Input() templateReferance!: TemplateRef<unknown>;
  @Input() data!: any[];

  @Output() onPaginationChange = new EventEmitter<PageEvent>();
  @Output() onSortChange = new EventEmitter<Sort>();
  @Output() onAddClicked = new EventEmitter<void>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  protected _data: any[] = [];
  getContext(element: any) {
    return { $implicit: element };
  }

  sortChanged(sort: Sort) {
    this.paginator.pageIndex = 0;
    this.onSortChange.emit(sort);
  }

  paginationChanged(page: PageEvent) {
    this.onPaginationChange.emit(page);
  }

  addClicked() {
    this.onAddClicked.next();
  }
}
