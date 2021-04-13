import { Component, OnInit } from '@angular/core';

export interface Author {
  id: number;
  firstName: string;
  lastName: string;
}

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.scss']
})
export class AuthorsComponent implements OnInit {

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
  editingMode = false;

  startAddAuthor(): void {
    this.editingMode = true;
    this.editingAuthor = {id: -1, firstName: '', lastName: ''};
  }

  startEditAuthors(author: Author): void {
    this.editingMode = true;
    this.editingAuthor = author;
  }

  saveAuthor(): void {
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
  }

}
