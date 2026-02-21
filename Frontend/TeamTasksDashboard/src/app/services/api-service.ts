import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export abstract class ApiService {
  private readonly baseUrl: string = environment.baseUrl;

  constructor(protected http: HttpClient) {}

  get<T>(endpoint: string, params?: any): Observable<T> {
    let httpParams = new HttpParams();
    if (params) {
      Object.keys(params).forEach((key) => {
        httpParams = httpParams.append(key, params[key]);
      });
    }

    return this.http.get<T>(this.createUrl(endpoint), { params: httpParams });
  }

  post<T, B>(endpoint: string, body: B): Observable<T> {
    return this.http.post<T>(this.createUrl(endpoint), body);
  }

  put<T, B>(id: string | number, body: B, endpoint: string = ''): Observable<T> {
    const path = endpoint ? `${endpoint}/${id}` : `${id}`;
    return this.http.put<T>(this.createUrl(path), body);
  }

  delete<T>(id: string | number, endpoint: string = ''): Observable<T> {
    const path = endpoint ? `${endpoint}/${id}` : `${id}`;
    return this.http.delete<T>(this.createUrl(path));
  }

  private createUrl(endpoint: string): string {
    const url = new URL(endpoint, this.baseUrl);
    return url.toString();
  }
}
