import { Injectable } from '@angular/core';
import { User } from '../models/user.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class UserService {
  private apiUrl = 'https://localhost:44336/api/users';
  private array: User[];
  public constructor(private http: HttpClient) { }

  findAll():Observable<User[]>{
    return this.http.get<User[]>(this.apiUrl);
  }

  getUser(id: string): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl + "/" + id);
  }
}
