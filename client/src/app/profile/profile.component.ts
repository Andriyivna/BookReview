import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Review} from '../book/book.component';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  isLoggedIn = false;
  readonly BaseURL = 'https://localhost:5001/api';
  displayName = 'displayName';
  email = 'email';
  avatarUrl = 'https://localhost:5001/Content/avatars/av-male.jpg';
  reviews: Review[];

  constructor(private router: Router, private http: HttpClient) { }

  async ngOnInit(): Promise<void> {
    if (localStorage.getItem('token') == null){
      this.router.navigate(['/login']);
    }else{
      this.isLoggedIn = true;
      let reqHeader = new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + (localStorage.getItem('token') || '{}')
      });
      this.http.get<any>(this.BaseURL + `/Accounts`, { headers: reqHeader }).subscribe(async res => {
        this.displayName = res.displayName;
        this.email = res.email;
        if (res.avatar != null) {
          this.avatarUrl = res.avatar;
        }
        this.reviews = await this.http.get<Review[]>(this.BaseURL + '/Reviews/book/review/' + this.displayName).toPromise();
        console.log(this.reviews);
      });
    }
  }

}
