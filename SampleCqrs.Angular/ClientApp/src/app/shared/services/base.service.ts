import { HttpClient, HttpHeaders, HttpParamsOptions } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

  private _baseUrl: string = environment.baseUrI;
  httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      "Accept-Language": "En-us",
    })
  };

  constructor(private httpClient: HttpClient) {
  }

  protected Get(action: string,paramter: string = ''): Observable<any> {
    return this.httpClient.get<any>(`${this._baseUrl}/${action}/${paramter}`)
      .pipe(tap(() => { },
        (ex) => {
          alert(`Failed: ${ex.error.title} => ${ex.error.detail}`);
          console.log("http failed =>", ex);
        }
      ));
  }

  protected Post(action: string,model: any): Observable<any> {
    return this.httpClient.post<any>(`${this._baseUrl}/${action}`, model, this.httpOptions).pipe(tap(() => {
      alert("Success")
    },
      (ex) => {
        alert(`Failed: ${ex.error.title} => ${ex.error.detail}`);
        console.log("http failed =>", ex);
      }
    ));
  }

  protected Put( action: string,model: any, paramter: string = ''): Observable<any> {
    return this.httpClient.put<any>(`${this._baseUrl}/${action}/${paramter}`, model, this.httpOptions)
      .pipe(tap(() => {
        alert("Success")
      },
        (ex) => {
          alert(`Failed: ${ex.error.title} => ${ex.error.detail}`);
          console.log("http failed =>", ex);
        }
      ));
  }

  protected Delete(action: string,paramter: string): Observable<any> {
    return this.httpClient.delete<any>(`${this._baseUrl}/${action}/${paramter}`).pipe(tap(() => {
      alert("Success")
    },
      (ex) => {
        alert(`Failed: ${ex.error.title} => ${ex.error.detail}`);
        console.log("http failed =>", ex);
      }
    ));
  }

}
