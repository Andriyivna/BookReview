import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  
  isNavbarCollapsed = true;
  isLoggedIn: boolean = false;
  displayname: any;
  readonly BaseURL = 'https://localhost:5001/api';

  constructor(private router: Router,private http:HttpClient) { 
  }

  ngOnInit(): void {
    if(localStorage.getItem('token') != null){
      this.isLoggedIn = true;
      this.displayname = localStorage.getItem('displayName');
    }
  }

  onLogout(){
    localStorage.removeItem('token');
    this.router.navigate(['/login']).then(()=> {location.reload()});
  }

  getUser(){
    console.log((localStorage.getItem('token') || 'noToken'));
      var reqHeader = new HttpHeaders({ 
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + (localStorage.getItem('token') || '{}')
      });
    
    return this.http.get(this.BaseURL+`/Accounts`, { headers: reqHeader }).subscribe(
      data => console.log(data),
      err => console.log(err)
    );
  }
}
