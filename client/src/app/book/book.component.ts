import { Component, OnInit } from '@angular/core';
import { BookService } from './book.service';
import { ActivatedRoute } from '@angular/router';
import { VirtualLibraryService } from '../bookcase/services/virtual-library.service';


export interface Book {
  id?: number;
  title: string;
  coverImg: string;
  publisher: string;
  releaseYear: number;
  averangeRates: number;
  authorId: number;
  description: string;
  genreId: number;
  author: string;
  genre: string;
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
    private virtualLibraryServices: VirtualLibraryService,
  ) { }

  book: Book = null;

  addToBookcase(): void{
    if (this.book.id){
      this.virtualLibraryServices.add(this.book.id)
        .then(() => this.update());
    }
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

