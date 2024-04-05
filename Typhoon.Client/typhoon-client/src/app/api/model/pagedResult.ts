export interface PagedResult<T> {
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  items: T;
}
