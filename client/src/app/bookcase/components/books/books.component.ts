import { Component, OnInit } from '@angular/core';
import { VirtualLibraryService } from '../../services/virtual-library.service';
import { AuthorsService } from '../../services/authors.service';
import { Author } from '../authors/authors.component';
import { Book } from '../../../book/book.component';

export interface MyBook {
  id?: number;
  bookId?: number;
  title: string;
  coverImg: string;
  author: string;
  genre: string;
  status: string;
  virtualBookId?: number;
}

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss']
})
export class BooksComponent implements OnInit {

  constructor(private virtualLibraryService: VirtualLibraryService) { }

  books: Array<MyBook> = [];
  booksToRead: Array<MyBook> = [];
  booksInProgress: Array<MyBook> = [];
  booksDone: Array<MyBook> = [];


  changeStatus(id: number, status: string): void{
      this.virtualLibraryService.put(id, status)
        .then(() => this.update());
  }

  update(): void {
    this.virtualLibraryService.getAll()
      .then((data) => {
        this.books = data;
        this.booksToRead = data.filter(book => book.status === 'ToRead');
        this.booksInProgress = data.filter(book => book.status === 'InProgress');
        this.booksDone = data.filter(book => book.status === 'Done');
      });
  }
  ngOnInit(): void {
    this.update();
  }

}
