import { Component, OnInit } from '@angular/core';
<<<<<<< HEAD
=======
import { Router } from '@angular/router';
import { UserService } from '../../user.service';
>>>>>>> ccae311812251f519d42b999b10c866d9fb3c0df

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

<<<<<<< HEAD
  constructor() { }

  ngOnInit(): void {
  }

=======
  constructor(public service: UserService, private router: Router) { }

  ngOnInit(): void {
    this.service.formModel.reset();
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
>>>>>>> ccae311812251f519d42b999b10c866d9fb3c0df
}
