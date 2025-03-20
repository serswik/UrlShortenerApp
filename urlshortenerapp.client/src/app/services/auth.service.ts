import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:4200/api/auth'; // Замените на ваш API URL

  constructor(private http: HttpClient) { }

  register(model: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, model);
  }

  login(model: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, model);
  }
}
