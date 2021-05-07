import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../user.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})



export class RegisterComponent implements OnInit {

  constructor(public service: UserService, private router: Router, private http:HttpClient) { }

  readonly BaseURL = 'https://localhost:5001/api';

  Avatars: any;

  ngOnInit(): void {
    this.service.formModel.reset();
    this.getAvatars();
  }

  getAvatars(){
    this.http.get<any>(this.BaseURL+`/Avatars`).subscribe( res => {
      this.Avatars = res;
      console.log(this.Avatars);
    } );
  }
  
  onSubmit(){
    this.service.register().subscribe(
      (res:any) =>{
        this.router.navigate(['/login']);
      },
      err => {
        console.log(err);
      }
    );
  }
}
