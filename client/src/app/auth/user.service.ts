import {Injectable} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {MustMatch, StrongPassword} from './password.validator';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  constructor(private fb: FormBuilder, private http: HttpClient) {
  }

  readonly BaseURL = 'https://localhost:5001/api';


  formModel = this.fb.group({
    DisplayName: ['', Validators.required],
    Email: ['', Validators.email],
    Passwords: this.fb.group({
        Password: ['', [Validators.required, Validators.minLength(6)]],
        ConfirmPassword: ['', Validators.required]
      }, {
        validators: [MustMatch('Password', 'ConfirmPassword'), StrongPassword('Password')]
      }
    ),
    avatar: ['']
  });

  register() {

    var body = {
      displayName: this.formModel.value.DisplayName,
      email: this.formModel.value.Email,
      password: this.formModel.value.Passwords.Password,
      avatarId: this.formModel.value.avatar
    };
    console.log(body);
    return this.http.post(this.BaseURL + '/Accounts/register', body);

  }

  login(formData: any) {
    return this.http.post(this.BaseURL + '/Accounts/login', formData);
  }
}

