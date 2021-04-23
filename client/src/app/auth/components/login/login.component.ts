import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../user.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {

  loginAttempt: boolean = false;
  user: string[] = [];
  readonly BaseURL = 'https://localhost:5001/api';
  
  constructor(public service: UserService, private router: Router,private http:HttpClient) {  }
  

  ngOnInit(): void {
  }

  onSubmit(form:NgForm){
    console.log(form.value);
    this.service.login(form.value).subscribe(
      (res:any)=>{
        var reqHeader = new HttpHeaders({ 
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + res.token
        });
        this.http.get<any>(this.BaseURL+`/Accounts`, { headers: reqHeader }).subscribe( res => {
          localStorage.setItem('displayName',res['displayName']);
        } );
        localStorage.setItem('token',res.token);
        console.log(localStorage.getItem('token'));
        
        this.router.navigate(['']).then(()=> {location.reload()});
      },
      err => {
        if(err.status == 401)
          console.log('Auth failed');
          form.reset();
          this.loginAttempt = true;
      }
    );
  }

  
}
