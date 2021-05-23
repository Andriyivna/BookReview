import {Component, OnInit} from '@angular/core';
import {BookService} from '../book/book.service';
import {Book} from '../book/book.component';

export interface Quote {
  content: string;
  author: string;
}
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {

  isLoggedIn: boolean = false;
  books: Book[] = [];
  dailyQuote: Quote = null;
  constructor( private bookService: BookService) {
  }

  async ngOnInit(): Promise<void> {
    if (localStorage.getItem('token') != null) {
      this.isLoggedIn = true;
    }
    this.books = await this.bookService.getHighRateBook(4);
    this.dailyQuote = await this.bookService.getDailyQuote().toPromise();
  }

}
