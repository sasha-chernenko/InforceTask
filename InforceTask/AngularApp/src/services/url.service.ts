import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {URL} from "../models/url.model";


@Injectable({
  providedIn: 'root'
})
export class UrlService {
  public urls: URL[] = [];
  private baseUrl = '';
  constructor(private http: HttpClient) {
    this.baseUrl = this.getBaseUrl();
  }

  public getBaseUrl():string {
    return document.getElementsByTagName('base')[0].href;
}

  public getUrls(): Observable<URL[]>{
    return this.http.get<URL[]>(this.baseUrl + 'url');
  }

  public addUrl(addUrlRequest: URL): Observable<URL>{
    return this.http.post<URL>(this.baseUrl + 'url', addUrlRequest);
  }
  public deleteUrl(id: number): Observable<URL> {
    return this.http.delete<URL>(this.baseUrl + 'url/'+ id);
  }
  public deleteAllUrls():Observable<URL[]> {
    return this.http.delete<URL[]>(this.baseUrl + 'url');
  }
}
