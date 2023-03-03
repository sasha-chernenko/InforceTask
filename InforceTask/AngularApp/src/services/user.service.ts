import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = '';
  constructor(private http: HttpClient) {
    this.baseUrl = this.getBaseUrl();
  }

  private getBaseUrl() :string {
    return  document.getElementsByTagName('base')[0].href;
  }

  public getUser(): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'account/get');
  }


}
