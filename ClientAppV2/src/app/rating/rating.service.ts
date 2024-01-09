import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Rating } from '../shared/models/rating';

@Injectable({
  providedIn: 'root'
})
export class RatingService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getRatingsByUserName(userName: string): Observable<Rating[]> {
    return this.http.get<Rating[]>(`${this.baseUrl}Rating/${userName}`);
  }

  getRatingsByPhotoId(photoId: number): Observable<Rating[]> {
    return this.http.get<Rating[]>(`${this.baseUrl}Rating/photo/${photoId}`);
  }

  getAverageRatingForPhoto(photoId: number): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}Rating/photo/${photoId}/average`);
  }

  addRatingToPhoto(rating: Rating): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}Rating`, rating);
  }

  updateRating(rating: Rating): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}Rating`, rating);
  }

  getRatingForPhotoByUser(photoId: number, userName: string): Observable<Rating> {
    return this.http.get<Rating>(`${this.baseUrl}Rating/photo/${photoId}/user/${userName}`);
  }
}
