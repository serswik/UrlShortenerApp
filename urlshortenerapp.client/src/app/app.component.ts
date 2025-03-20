import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common'
import { HttpClientModule } from '@angular/common/http'

interface ShortenedUrl {
  id: number;
  originalUrl: string;
  shortCode: string;
  createdBy: string;
  createdAt: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  imports: [CommonModule, HttpClientModule]
})
export class AppComponent implements OnInit {
  public shortenedUrls: ShortenedUrl[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getShortenedUrls();
  }

  getShortenedUrls() {
    this.http.get<ShortenedUrl[]>("http://localhost:5111/api/urls").subscribe(
      (result) => {
        this.shortenedUrls = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
