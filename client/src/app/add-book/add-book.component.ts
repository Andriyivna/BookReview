import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Author } from '../bookcase/components/authors/authors.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

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

    this.addBookForm.valueChanges.subscribe(console.log)

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
  addBook(){
    this.str = this.addBookForm.get('coverImg').value.split( '\\' );
    const formValue = this.addBookForm.value;
    formValue['coverImg'] = this.str[this.str.length-1];
    //this.addBookForm.reset();
    return this.http.post(this.ApiURL+'/Book/add',formValue);
  }

  onSubmit() {
    this.addBook().subscribe(
      (res:any) =>{
        this.addBookForm.reset();
        this.isSent = true;
      },
      err => {
        console.log(err);
      }
    )
  }

  getAll(): Promise<Array<Author>>{
    return this.http.get<Array<Author>>(this.ApiURL+'/Authors/').toPromise();
  }
  getGenres(): Promise<Array<Genre>>{
    return this.http.get<Array<Genre>>(this.ApiURL+'/Genres').toPromise();
  }
}
