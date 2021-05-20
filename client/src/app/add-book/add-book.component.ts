import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Author } from '../bookcase/components/authors/authors.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

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
export interface Genre {
  id?: number;
  name: string;
}
interface Alert {
  type: string;
  message: string;
}

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.scss']
})
export class AddBookComponent implements OnInit {

  readonly ApiURL = 'https://localhost:5001/api';
  isLoggedIn: boolean = false;
  authors: Array<Author> = [];
  genres: Array<Genre> = [];

  isSent: boolean = false;
  addBookForm: FormGroup;
  nrSelectAuth = 1;
  nrSelectGen = 1;

  alert: Alert = {type: 'success', message: 'A new book has been added!'}

  constructor(private http:HttpClient,private router: Router, private fb: FormBuilder) { }

  ngOnInit(): void {
    if(localStorage.getItem('token') != null){
      this.isLoggedIn = true;
    }else{
      this.router.navigate(['/login']);
    }
    this.addBookForm = this.fb.group({
      id: 0,
      title: ['',[
        Validators.required
      ]],
      publisher: ['',[
        Validators.required
      ]],
      releaseYear: ['',[
        Validators.required
      ]],
      authorId: [,[
        Validators.required
      ]],
      genreId: [,[
        Validators.required
      ]],
      coverImg: ['',[
        Validators.required
      ]],
      description: ['',[
        Validators.required
      ]],
      averageRates: 0
    })

    this.update();
  }

  update(): void {
    this.getAll()
      .then((data) => {
        this.authors = data;
      });
    this.getGenres()
      .then((data)=> {
        this.genres = data;
      });
  }
  str: any;
  bookId: any;
  addBook(){
    var genre:any;
    var formValue:any;

    this.str = this.addBookForm.get('coverImg').value.split( '\\' );
    formValue = this.addBookForm.value;
    formValue['coverImg'] = this.str[this.str.length-1];
    
    for(var i = 0; i < this.genres.length; i++){
      if(this.genres[i].name == formValue['genreId']){
        formValue['genreId'] = this.genres[i].id;
        break;
      }
    }
    for(var i = 0; i < this.authors.length; i++){
      if(this.authors[i].firstName + ' ' +this.authors[i].secondName == formValue['authorId']){
        formValue['authorId'] = this.authors[i].id;
        break;
      }
    }
    return this.http.post(this.ApiURL+'/Book/add',formValue);
  }
  fetchedBook: Book;
  onSubmit() {
    this.addBook().subscribe(
      (res:any) =>{
        this.isSent = true;
        this.getBook(this.addBookForm.get('title').value).then(data => {
          this.router.navigateByUrl('/book/'+data.id)
        })
      },
      err => {
        console.log(err);
      }
    )
  }

  getBook(title:string): Promise<Book>{
    return this.http.get<Book>(this.ApiURL+'/Book/title/'+title).toPromise();
  }
  getAll(): Promise<Array<Author>>{
    return this.http.get<Array<Author>>(this.ApiURL+'/Authors/').toPromise();
  }
  getGenres(): Promise<Array<Genre>>{
    return this.http.get<Array<Genre>>(this.ApiURL+'/Genres').toPromise();
  }
}
