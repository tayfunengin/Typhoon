export interface BaseApiResponse<T> {
  readonly success: boolean;
  message: string | null;
  data?: T;
}
