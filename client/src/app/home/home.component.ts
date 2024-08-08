import { Component, OnInit, inject } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  registerMode = false;
  http = inject(HttpClient);
  users: any;

  ngOnInit(): void {
    this.getUsers();
  }

  cancelRegisterMode(event: any) {
    this.registerMode  = event;
  }
  registorToggle(){
    this.registerMode = !this.registerMode;
  }


  getUsers(){
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: response => this.users = response,
      error: error => console.log(error),
      complete: () => console.log('Response Has Completed')
  } );
  }

}
