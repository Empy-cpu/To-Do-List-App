import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TodoAppService } from '../services/todo-app.service';
import { CommonModule } from '@angular/common';




@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [
  ]
})
export class LoginComponent implements OnInit {
  invalidLogin: boolean | undefined;

  constructor(public service: TodoAppService,
    private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
  }

  login(form: NgForm) {
    this.service.loginUser().subscribe(
      res => {
        const token = (<any>res).token;
        localStorage.setItem("jwt", token);
        this.invalidLogin = false;
        this.router.navigate(["/"]);
        this.toastr.success("LoggedIn successfully");
      },
      err => {
        this.invalidLogin = true;
      }
    );
  }
}