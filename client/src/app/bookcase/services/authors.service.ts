import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Author } from '../components/authors/authors.component';

@Injectable({
  providedIn: 'root'
})
export class AuthorsService {

  constructor(private http: HttpClient) { }
  readonly ApiURL = 'https://localhost:5001/api/Authors/';
  getAll(): Promise<Array<Author>>{
    return this.http.get<Array<Author>>(this.ApiURL).toPromise();
  }

  create(author: Author): Promise<Author> {
    return this.http.post<Author>(this.ApiURL, author).toPromise();
  }

  update(author: Author): Promise<Author> {
    return this.http.put<Author>(this.ApiURL, author).toPromise();
  }

  get(id: number): Promise<Author>{
    return this.http.get<Author>(this.ApiURL + id).toPromise();
  }

  delete(id: number): Promise<Author>{
    return this.http.delete<Author>(this.ApiURL + id).toPromise();
  }

}
