import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { BaseApiResponse } from '../model/baseApiResponse';
import { PagedResult } from '../model/pagedResult';
import { ProductDto, ProductUpdateDto } from '../../models/productDto';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private baseUrl = environment.apiUrl;
  private getFilteredListUrl = this.baseUrl + 'Product/getFilteredList';
  private createUrl = this.baseUrl + 'Product';
  private editUrl = this.baseUrl + 'Product';
  private delete = this.baseUrl + 'Product';

  constructor(private httpClient: HttpClient) {}

  public apiProductGetFilteredList(
    page: number,
    recordsToTake: number,
    orderBy: string,
    brand?: string,
    category?: string
  ) {
    let params = new HttpParams();
    params = params.append('page', page);
    params = params.append('recordsToTake', recordsToTake);
    params = params.append('orderBy', orderBy);
    if (brand) params = params.append('brand', brand);
    if (category) params = params.append('category', category);

    return this.httpClient.get<BaseApiResponse<PagedResult<ProductDto[]>>>(
      this.getFilteredListUrl,
      { params }
    );
  }

  public apiProductPost(
    name: string,
    description: string,
    brand: string,
    price: number,
    categoryId: number
  ) {
    return this.httpClient.post<BaseApiResponse<ProductDto>>(this.createUrl, {
      name: name,
      description: description,
      brand: brand,
      price: price,
      categoryId: categoryId,
    });
  }

  public apiProductPut(
    id: number,
    name: string,
    description: string,
    brand: string,
    price: number,
    categoryId: number
  ) {
    return this.httpClient.put<BaseApiResponse<ProductUpdateDto>>(
      this.editUrl + `/${id}`,
      {
        id: id,
        name: name,
        description: description,
        brand: brand,
        price: price,
        categoryId: categoryId,
      }
    );
  }
  public apiProductDelete(id: number) {
    return this.httpClient.delete<BaseApiResponse<ProductDto>>(
      this.delete + `/${id}`
    );
  }
}
