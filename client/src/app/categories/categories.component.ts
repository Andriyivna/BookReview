import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Author } from '../bookcase/components/authors/authors.component';
import { FormBuilder, FormGroup } from '@angular/forms';
import { analyzeAndValidateNgModules } from '@angular/compiler';


export interface Genre {
  id?: number;
  name: string;
}
export interface Book {
  id?: number;
  title: string;
  coverImg: string;
  publisher?: string;
  releaseYear: number;
  averageRates: number;
  description?: string;
  author: string;
  genre: string;
  authorId: number;
  genreId: number;
}

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss']
})
export class CategoriesComponent implements OnInit {

  readonly ApiURL = 'https://localhost:5001/api';
  isLoggedIn: boolean = false;
  authors: Array<Author> = [];
  genres: Array<Genre> = [];
  books: Array<Book> = [];
  searchBookForm: FormGroup;

  constructor(private http: HttpClient, private fb: FormBuilder) { }


  ngOnInit(): void {
    this.update();
    this.searchBookForm = this.fb.group({
      title: '',
      genreId: null,
      year: null,
      authorId: null,
      minRating: 1,
      maxRating: 10
    })
  }

  update(): void {
    this.getAll()
      .then((data) => {
        this.authors = data;
      });
    this.getGenres()
      .then((data) => {
        this.genres = data;
      });
    this.getBooks()
      .then((data) => {
        this.books = data;
      });
  }



  updateBooksByGenre() {
    this.getBooksByGenre().then((data) => {
      this.books = data
    });
    this.searchBookForm.controls['authorId'].setValue('');
  }
  updateBooksByAuthor() {
    this.getBooksByAuthor().then((data) => {
      this.books = data
    });
    this.searchBookForm.controls['genreId'].setValue('');
  }
  filterByRating() {
    if (this.books.filter(this.isBetween).length) {
      this.books = this.books.filter(this.isBetween)
    }
  }

  isBetween = (element: Book, index: any, array: any) => {
    return (element.averageRates >= this.searchBookForm.get('minRating').value && element.averageRates <= this.searchBookForm.get('maxRating').value)
  }

  filterBooks = (book: Book) => {
    return book.title.includes(this.searchBookForm.get('title').value)
  }

  resetSearch() {
    this.update();
    this.searchBookForm.controls['genreId'].setValue('');
    this.searchBookForm.controls['authorId'].setValue('');
    this.searchBookForm.controls['title'].setValue('');
  }
  getBooksByGenre(): Promise<Array<Book>> {
    return this.http.get<Array<Book>>(this.ApiURL + '/Book/filtreGenre/' + this.searchBookForm.get('genreId').value).toPromise();
  }
  getBooksByAuthor(): Promise<Array<Book>> {
    return this.http.get<Array<Book>>(this.ApiURL + '/Book/filtreAuthor/' + this.searchBookForm.get('authorId').value).toPromise();
  }
  getBooks(): Promise<Array<Book>> {
    return this.http.get<Array<Book>>(this.ApiURL + '/Book').toPromise();
  }
  getAll(): Promise<Array<Author>> {
    return this.http.get<Array<Author>>(this.ApiURL + '/Authors/').toPromise();
  }
  getGenres(): Promise<Array<Genre>> {
    return this.http.get<Array<Genre>>(this.ApiURL + '/Genres').toPromise();
  }
}
