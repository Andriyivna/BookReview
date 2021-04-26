import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
<<<<<<< HEAD

  constructor() { }

  ngOnInit(): void {
  }

=======
  
  isLoggedIn: boolean = false;

  constructor() { 
  }

  ngOnInit(): void {
    if(localStorage.getItem('token') != null){
      this.isLoggedIn = true;
    }
  }
  
>>>>>>> ccae311812251f519d42b999b10c866d9fb3c0df
}
