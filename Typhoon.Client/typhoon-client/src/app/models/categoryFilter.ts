import { BaseFilter } from './baseFiilter';

export class CategoryFilter implements BaseFilter {
  constructor(
    recordsToTake = 10,
    page = 1,
    orderBy = 'Id Desc',
    categoryName = ''
  ) {
    this.recordsToTake = recordsToTake;
    this.page = page;
    this.orderBy = orderBy;
    this.categoryName = categoryName;
  }
  page: number;
  recordsToTake: number;
  orderBy: string;
  categoryName: string;
}
