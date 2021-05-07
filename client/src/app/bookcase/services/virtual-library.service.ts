import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MyBook } from '../components/books/books.component';

interface VirtualLibraryResponse {
  books: Array<MyBook>;
}
@Injectable({
  providedIn: 'root'
})
export class VirtualLibraryService {

  constructor(private http: HttpClient) { }
  readonly ApiURL = 'https://localhost:5001/api/VirtualLibrary/';

  getAll(): Promise<Array<MyBook>>{
    return this.http.get<VirtualLibraryResponse>(this.ApiURL)
      .toPromise()
      .then(response => response.books);
  }
}
