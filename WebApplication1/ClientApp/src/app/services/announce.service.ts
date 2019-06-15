import { Injectable } from '@angular/core';
import { Announcer } from '../models/announcement.model';
import { HttpClient } from '@angular/common/http';


@Injectable()
export class AnnService {
  private apiUrl = 'https://localhost:44336/api/announcements';
  private array: Announcer[];
  public constructor(private http: HttpClient) {}

  findAll() {
    return (this.http.get<Announcer[]>(this.apiUrl));
  }
}
