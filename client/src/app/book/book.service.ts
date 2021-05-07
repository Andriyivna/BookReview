import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Book } from './book.component';



@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor(private http: HttpClient) { }
  readonly ApiURL = 'https://localhost:5001/api/Book/';

  get(id: number): Promise<Book>{
    return this.http.get<Book>(this.ApiURL + 'id/' + id).toPromise();
  }
}
