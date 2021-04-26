import { Component, OnInit } from '@angular/core';
<<<<<<< HEAD
=======
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
>>>>>>> ccae311812251f519d42b999b10c866d9fb3c0df

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

<<<<<<< HEAD
  constructor() { }

  ngOnInit(): void {
  }

=======
  isLoggedIn: boolean = false;
  readonly BaseURL = 'https://localhost:5001/api';
  displayName: string = 'displayName';
  email: string = 'email';

  constructor(private router: Router,private http:HttpClient) { }

  ngOnInit(): void {
    if(localStorage.getItem('token') == null){
      this.router.navigate(['/login']);
    }else{
      this.isLoggedIn = true;
      var reqHeader = new HttpHeaders({ 
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + (localStorage.getItem('token') || '{}')
      });
      this.http.get<any>(this.BaseURL+`/Accounts`, { headers: reqHeader }).subscribe( res => {
        this.displayName = res['displayName'];
        this.email = res['email'];
      } );
      
    }
  }



>>>>>>> ccae311812251f519d42b999b10c866d9fb3c0df
}
