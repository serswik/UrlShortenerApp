import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppComponent],
      imports: [HttpClientTestingModule]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it('should retrieve shortened URLs from the server', () => {
    const mockUrls = [
      { id: 1, originalUrl: 'http://example.com', shortCode: 'abc123', createdBy: 'user1', createdAt: '2023-03-20T12:00:00Z' },
      { id: 2, originalUrl: 'http://another.com', shortCode: 'def456', createdBy: 'user2', createdAt: '2023-03-21T14:00:00Z' }
    ];

    component.ngOnInit();

    const req = httpMock.expectOne('/api/shortened-urls');
    expect(req.request.method).toEqual('GET');

    req.flush(mockUrls);

    expect(component.shortenedUrls).toEqual(mockUrls);
  });
});
