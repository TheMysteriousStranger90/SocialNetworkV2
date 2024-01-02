export interface PaginationResult<T> {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: T;
}
