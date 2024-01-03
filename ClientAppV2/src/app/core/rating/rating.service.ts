import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {Rating} from 'src/app/shared/models/rating';
import {environment} from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RatingService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getRatingsByUserName(userName: string): Observable<Rating[]> {
    return this.http.get<Rating[]>(`${this.baseUrl}/${userName}`);
  }

  getRatingsByPhotoId(photoId: number): Observable<Rating[]> {
    return this.http.get<Rating[]>(`${this.baseUrl}/photo/${photoId}`);
  }

  getAverageRatingForPhoto(photoId: number): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}/photo/${photoId}/average`);
  }

  addRatingToPhoto(rating: Rating): Observable<void> {
    return this.http.post<void>(this.baseUrl, rating);
  }

  updateRating(rating: Rating): Observable<void> {
    return this.http.put<void>(this.baseUrl, rating);
  }
}
