import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookcaseRoutingModules } from './bookcase-routing.modules';
import { BookcaseComponent } from './components/bookcase/bookcase.component';
import { AuthorsComponent } from './components/authors/authors.component';
import { BooksComponent } from './components/books/books.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [BookcaseComponent, AuthorsComponent, BooksComponent],
  imports: [
    FormsModule,
    CommonModule,
    BookcaseRoutingModules
  ]
})
export class BookcaseModule { }
