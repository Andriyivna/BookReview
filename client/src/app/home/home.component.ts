import {Component, OnInit} from '@angular/core';
import {BookService} from '../book/book.service';
import {Book} from '../book/book.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  isLoggedIn: boolean = false;
  books: Book[] = [];
  constructor( private bookService: BookService) {
  }

  async ngOnInit(): Promise<void> {
    if (localStorage.getItem('token') != null) {
      this.isLoggedIn = true;
    }
    this.books = await this.bookService.getHighRateBook(4);
  }

}
