import { Component, OnInit, inject } from '@angular/core';
import { RegisterComponent } from "../register/register.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent  {

  registerMode = false;


  cancelRegisterMode(event: any) {
    this.registerMode  = event;
  }
  registorToggle(){
    this.registerMode = !this.registerMode;
  }


 

}
