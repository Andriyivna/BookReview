import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
<<<<<<< HEAD
=======
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from '@angular/common/http';
>>>>>>> ccae311812251f519d42b999b10c866d9fb3c0df

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { AuthModule } from './auth/auth.module';
import { SharedModule } from './shared/shared.module';
import { BookcaseModule } from './bookcase/bookcase.module';
<<<<<<< HEAD
import { BookComponent } from './book/book.component';
=======
import { UserService } from './auth/user.service';
>>>>>>> ccae311812251f519d42b999b10c866d9fb3c0df

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
<<<<<<< HEAD
    ProfileComponent,
    BookComponent
=======
    ProfileComponent
>>>>>>> ccae311812251f519d42b999b10c866d9fb3c0df
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    SharedModule,
    AuthModule,
<<<<<<< HEAD
    BookcaseModule
  ],
  providers: [],
=======
    BookcaseModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [UserService],
>>>>>>> ccae311812251f519d42b999b10c866d9fb3c0df
  bootstrap: [AppComponent]
})
export class AppModule { }
