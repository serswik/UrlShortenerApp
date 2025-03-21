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
  standalone: true,
  imports: [CommonModule, HttpClientModule]
})
export class AppComponent implements OnInit {
  public shortenedUrls: ShortenedUrl[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getShortenedUrls();
  }

  getShortenedUrls() {
    this.http.get<ShortenedUrl[]>("https://localhost:7225/api/Urls/public").subscribe(
      (result) => {
        this.shortenedUrls = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
