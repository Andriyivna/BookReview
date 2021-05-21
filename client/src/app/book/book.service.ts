import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Book, Review} from './book.component';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor(private http: HttpClient) { }
  readonly ApiURL = 'https://localhost:5001/api/';

  get(id: number): Promise<Book>{
    return this.http.get<Book>(this.ApiURL + 'Book/id/' + id).toPromise();
  }
  getHighRateBook(count: number): Promise<Book[]>{
    return this.http.get<Book[]>(this.ApiURL + 'Book/high/rate/' + count).toPromise();
  }
  getReviews(bookID: number): Observable<Review[]>{
    return this.http.get<Review[]>(this.ApiURL + 'Reviews/book/' + bookID);
  }
  addReview(review: Review) {
    return this.http.post(this.ApiURL + 'Reviews', review);
  }
}
