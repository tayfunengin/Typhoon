import { BaseFilter } from './baseFiilter';

export class ProductFilter implements BaseFilter {
  constructor(
    recordsToTake = 10,
    page = 1,
    orderBy = 'Id Desc',
    brand = '',
    category = ''
  ) {
    this.recordsToTake = recordsToTake;
    this.page = page;
    this.orderBy = orderBy;
    this.category = category;
    this.brand = brand;
  }
  page: number;
  recordsToTake: number;
  orderBy: string;
  category: string;
  brand: string;
}
