import { Component, OnInit } from '@angular/core';
import { AuthorsService } from '../bookcase/services/authors.service';
import { BookService } from './book.service';
import { ActivatedRoute } from '@angular/router';

export interface Book {
  id?: number;
  title: string;
  coverImg: string;
  authorId: number;
  author: string;
  genreId: number;
  genre: string;
  usersWhoFavouritedBook: string;
}

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss']
})
export class BookComponent implements OnInit {

  constructor(
    private bookService: BookService,
    private route: ActivatedRoute,
  ) { }

  book: Book = null;

  addToBookcase(): void{
///
  }

  update(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id') as string, 10);
    this.bookService.get(id)
      .then((data) => {
        this.book = data;
      });
  }

  ngOnInit(): void {
    this.update();
  }

}

