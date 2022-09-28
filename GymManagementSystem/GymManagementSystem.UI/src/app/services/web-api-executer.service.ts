import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { MessageService } from './message.service';

@Injectable({
  providedIn: 'root'
})
export class WebApiExecuterService {

  private httpClient: HttpClient;
  private messageService: MessageService;
  private httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) }; // Authorization: 'Bearer ' + token;

  constructor(httpClient: HttpClient, messageService: MessageService) {
    this.httpClient = httpClient;
    this.messageService = messageService;
  }

  public postData<Type>(url: string, instance: Type): Observable<Type> {
    return this.httpClient.post<Type>(url, instance, this.httpOptions)
      .pipe(catchError(this.handleError<Type>('getData faild from ' + url)));
  }

  public getData<Type>(url: string): Observable<Type> {
    return this.httpClient.get<Type>(url, this.httpOptions)
      .pipe(catchError(this.handleError<Type>('getData faild from ' + url)));
  }

  public putData<Type>(url: string, instance: Type): Observable<Type> {
    return this.httpClient.put<Type>(url, instance, this.httpOptions)
      .pipe(catchError(this.handleError<Type>('getData faild from ' + url)));
  }

  public deleteData<Type>(url: string): Observable<Type> {
    return this.httpClient.delete<Type>(url, this.httpOptions)
      .pipe(catchError(this.handleError<Type>('getData faild from ' + url)));
  }

  public addHeader(headerName: string, headerValue: string) {
    this.httpOptions = { headers: this.httpOptions.headers.set(headerName, headerValue) };
  }

  public removeHeader(headerName: string) {
    this.httpOptions = { headers: this.httpOptions.headers.delete(headerName) };
  }

  private log(message: string): void {
    this.messageService.add(`HeroService: ${message}`);
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      this.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    }
  }
}
