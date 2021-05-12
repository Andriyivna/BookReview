import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  isLoggedIn: boolean = false;
  readonly BaseURL = 'https://localhost:5001/api';
  displayName: string = 'displayName';
  email: string = 'email';
  avatarUrl: string ='https://localhost:5001/Content/avatars/av-male.jpg';

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
        if(res['avatar'] != null)
          this.avatarUrl = res['avatar'];
      } );
    }
  }

}
