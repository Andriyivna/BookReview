import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MyBook } from '../components/books/books.component';
import { Author } from '../components/authors/authors.component';
import { Book } from '../../book/book.component';

interface VirtualLibraryResponse {
  books: Array<MyBook>;
}
@Injectable({
  providedIn: 'root'
})
export class VirtualLibraryService {

  constructor(private http: HttpClient) { }
  readonly ApiURL = 'https://localhost:5001/api/VirtualLibrary/';


  changeStatus(id: number, status: string = ''): Promise<Book>{
    return this.http.put<Book>(this.ApiURL, {
      virtualBookId: id,
      status
    }).toPromise();
  }

  delete(id: number): Promise<void>{
    return  this.http.delete<void>(this.ApiURL + id).toPromise();
  }
  add(id: number, status: string = 'ToRead'): Promise<Book>{
    return this.http.post<Book>(this.ApiURL, {
      bookId: id,
      status
    }).toPromise();
  }

  getAll(): Promise<Array<MyBook>>{
    return this.http.get<VirtualLibraryResponse>(this.ApiURL)
      .toPromise()
      .then(response => response.books);
  }
}
