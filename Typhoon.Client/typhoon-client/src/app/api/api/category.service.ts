import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { HttpClient, HttpParams } from '@angular/common/http';
import { CategoryDto } from '../../models/categoryDto';
import { BaseApiResponse } from '../model/baseApiResponse';
import { PagedResult } from '../model/pagedResult';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private baseUrl = environment.apiUrl;
  private getFilteredListUrl = this.baseUrl + 'Category/getFilteredList';
  private getAll = this.baseUrl + 'Category/getAll';
  private createUrl = this.baseUrl + 'Category';
  private editUrl = this.baseUrl + 'Category';
  private delete = this.baseUrl + 'Category';
  constructor(private httpClient: HttpClient) {}

  public apiCategoryGetFilteredList(
    page: number,
    recordsToTake: number,
    orderBy: string,
    categoryName?: string
  ) {
    let params = new HttpParams();
    params = params.append('page', page);
    params = params.append('recordsToTake', recordsToTake);
    params = params.append('orderBy', orderBy);
    if (categoryName) params = params.append('categoryName', categoryName);

    return this.httpClient.get<BaseApiResponse<PagedResult<CategoryDto[]>>>(
      this.getFilteredListUrl,
      { params }
    );
  }

  public apiCategoryGetAll() {
    return this.httpClient.get<BaseApiResponse<CategoryDto[]>>(this.getAll);
  }

  public apiCategoryPost(name: string, description: string) {
    return this.httpClient.post<BaseApiResponse<CategoryDto>>(this.createUrl, {
      name: name,
      description: description,
    });
  }

  public apiCategoryPut(id: number, name: string, description: string) {
    return this.httpClient.put<BaseApiResponse<CategoryDto>>(
      this.editUrl + `/${id}`,
      {
        id: id,
        name: name,
        description: description,
      }
    );
  }
  public apiCategoryDelete(id: number) {
    return this.httpClient.delete<BaseApiResponse<CategoryDto>>(
      this.delete + `/${id}`
    );
  }
}
