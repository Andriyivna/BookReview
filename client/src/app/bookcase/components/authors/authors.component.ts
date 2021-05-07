import { Component, OnInit } from '@angular/core';
import { AuthorsService } from '../../services/authors.service';

export interface Author {
  id?: number;
  firstName: string;
  secondName: string;
}

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.scss']
})
export class AuthorsComponent implements OnInit {

  constructor(private authorsService: AuthorsService) { }

  authors: Array<Author> = [];

  editingAuthor: Author = {firstName: '', secondName: ''};
  editingMode = false;

  startAddAuthor(): void {
    this.editingMode = true;
    this.editingAuthor = {firstName: '', secondName: ''};
  }

  startEditAuthors(author: Author): void {
    this.editingMode = true;
    this.editingAuthor = author;
  }

  saveAuthor(): void {
    if (!this.editingAuthor.id) {
      this.authorsService.create(this.editingAuthor)
        .then(() => this.update());
    } else {
      this.authorsService.update(this.editingAuthor)
        .then(() => this.update());
    }
    this.editingAuthor = {id: -1, firstName: '', secondName: ''};
    this.editingMode = false;
  }

  removeAuthor(id?: number): void {
    if (id && confirm('Are you sure?')) {
      this.authorsService.delete(id)
        .then(() => this.update());
    }
  }

  update(): void {
    this.authorsService.getAll()
      .then((data) => {
        this.authors = data;
      });
  }

  ngOnInit(): void {
    this.update();
  }

}
