import { Component, OnInit } from '@angular/core';
<<<<<<< HEAD

export interface Author {
  id: number;
  firstName: string;
  lastName: string;
=======
import { AuthorsService } from '../../services/authors.service';

export interface Author {
  id?: number;
  firstName: string;
  secondName: string;
>>>>>>> ccae311812251f519d42b999b10c866d9fb3c0df
}

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.scss']
})
export class AuthorsComponent implements OnInit {

<<<<<<< HEAD
  constructor() { }

  authors: Array<Author> = [
    {
      id: 1,
      firstName: 'Daniel',
      lastName: 'Keyes'
    },
    {
      id: 2,
      firstName: 'John',
      lastName: 'Tolkien'
    },
    {
      id: 3,
      firstName: 'Joanne',
      lastName: 'Rowling'
    }
  ];

  editingAuthor: Author = {id: -1, firstName: '', lastName: ''};
=======
  constructor(private authorsService: AuthorsService) { }

  authors: Array<Author> = [];

  editingAuthor: Author = {firstName: '', secondName: ''};
>>>>>>> ccae311812251f519d42b999b10c866d9fb3c0df
  editingMode = false;

  startAddAuthor(): void {
    this.editingMode = true;
<<<<<<< HEAD
    this.editingAuthor = {id: -1, firstName: '', lastName: ''};
=======
    this.editingAuthor = {firstName: '', secondName: ''};
>>>>>>> ccae311812251f519d42b999b10c866d9fb3c0df
  }

  startEditAuthors(author: Author): void {
    this.editingMode = true;
    this.editingAuthor = author;
  }

  saveAuthor(): void {
<<<<<<< HEAD
    if (this.editingAuthor.id === -1) {
      this.editingAuthor.id = Math.max(...this.authors.map(author => author.id)) + 1;
      this.authors.push(this.editingAuthor);
    } else {
      const index = this.authors.findIndex((author) => {
        return author.id === this.editingAuthor.id;
      });
      if (index !== -1) {
        this.authors[index] = this.editingAuthor;
      }

    }
    this.editingAuthor = {id: -1, firstName: '', lastName: ''};
    this.editingMode = false;
  }

  removeAuthor(id: number): void {
    const index = this.authors.findIndex((author) => {
      return author.id === id;
    });
    if (index !== -1) {
      this.authors[index] = this.editingAuthor;
      this.authors.splice(index, 1);
    }
  }


  ngOnInit(): void {
=======
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
>>>>>>> ccae311812251f519d42b999b10c866d9fb3c0df
  }

}
