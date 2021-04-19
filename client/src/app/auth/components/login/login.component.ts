import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginAttempt: boolean = false;

  constructor(public service: UserService, private router: Router) { }
  

  ngOnInit(): void {
  }

  onSubmit(form:NgForm){
    console.log(form.value);
    this.service.login(form.value).subscribe(
      (res:any)=>{
        localStorage.setItem('token',res.token);
        this.router.navigate(['']);
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
