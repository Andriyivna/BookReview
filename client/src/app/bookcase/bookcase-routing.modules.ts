import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookcaseComponent } from './components/bookcase/bookcase.component';
import { BooksComponent } from './components/books/books.component';
import { AuthorsComponent } from './components/authors/authors.component';


const routes: Routes = [
  {
    path: 'bookcase',
    component: BookcaseComponent,
    children: [
      { path: 'books', component: BooksComponent },
      { path: 'authors', component: AuthorsComponent },
      { path: '**', redirectTo: 'books' }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BookcaseRoutingModules { }
