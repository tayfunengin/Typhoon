import { CategoryDto } from './categoryDto';

export interface ProductDto {
  id: number;
  name: string;
  description: string;
  brand: string;
  price: number;
  category: CategoryDto;
}

export interface ProductCreateDto {
  name: string;
  description: string;
  brand: string;
  price: number;
  categoryId: number;
}

export interface ProductUpdateDto {
  id: number;
  name: string;
  description: string;
  brand: string;
  price: number;
  categoryId: number;
}
