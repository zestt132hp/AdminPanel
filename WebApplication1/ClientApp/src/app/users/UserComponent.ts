import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import * as Usermodel from "../models/user.model";
import User = Usermodel.User;

@Component({
  selector: 'user-select',
  templateUrl: './user.component.html'
})
export class UserComponent implements OnInit {
  private uservice: UserService;
  private users: User[];

  constructor(uservice :UserService) {
    this.uservice = uservice;
  }

  ngOnInit(): void {
    this.uservice.findAll().subscribe(x => this.users = x);
  }
}
