import { AuthResponseDto } from './authResponseDto';

export interface AuthResponseDtoApiResponse {
  readonly success: boolean;
  message: string | null;
  data?: AuthResponseDto;
}
