import {Component, OnInit} from '@angular/core';
import {BookService} from './book.service';
import {ActivatedRoute} from '@angular/router';
import {VirtualLibraryService} from '../bookcase/services/virtual-library.service';
import {MyBook} from '../bookcase/components/books/books.component';
import {FormControl, FormGroup, NgForm, Validators} from '@angular/forms';


export interface Book {
  id?: number;
  title: string;
  coverImg: string;
  publisher: string;
  releaseYear: number;
  averageRates: number;
  authorId: number;
  description: string;
  genreId: number;
  author: string;
  genre: string;
}
export interface Review {
givenRate: number;
content: string;
bookID: number;
avatar: string;
displayName: string;
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
    private virtualLibraryServices: VirtualLibraryService ) { }

  book: Book = null;
  reviews: Review[] = [];
  booksAdded: boolean = false;
  reviewAdded: boolean = false;
  review: Review = null;
  addToBookcase(): void {
    if (this.book.id) {
      this.virtualLibraryServices.add(this.book.id)
        .then(() => this.update());
      this.booksAdded = true;
    }
  }

  update(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id') as string, 10);
    this.bookService.get(id)
      .then(async (data) => {
        this.book = data;
        this.reviews = await this.bookService.getReviews(this.book.id).toPromise();
        this.reviews.forEach((item) => {
          if (item.displayName ==  localStorage.getItem('displayName')){
            this.reviewAdded = true;
          }
        });
        console.log('review', this.reviews);
      });
  }

 async ngOnInit(): Promise<void> {
    this.update();
    this.virtualLibraryServices.getAll()
      .then((data) => {
        data.forEach((item) => {
          if (item.bookId == this.book.id) {
            this.booksAdded = true;
          }
        });
      });


  }


  publish(form: NgForm): void{
    this.review = form.value;
    this.review.bookID = this.book.id;
    this.review.givenRate = +this.review.givenRate;
    this.bookService.addReview(this.review);

  }

}

