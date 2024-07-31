import { TestBed } from '@angular/core/testing';

import { ClassroomsService } from './classrooms.service';
import { HttpClient, provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';

describe('ClassroomsService', () => {
  let service: ClassroomsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        provideHttpClient()
      ]
    });
    service = TestBed.inject(ClassroomsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
