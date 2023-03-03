import { Component, Inject } from '@angular/core'
import { HttpClient } from '@angular/common/http';
import {URL} from '../../models/url.model'
import { UrlService } from "../../services/url.service";
import { UserService } from "../../services/user.service";
import { User } from '../../models/user.model';


@Component({
  selector: 'app-urltable',
  templateUrl: './urltable.component.html'
})
export class URLTableComponent {
  public urls: URL[] = [];
  public baseUrl: string = "";
  public user: User = {
    id: 0,
    login: '',
    password: '',
    roleId: 0
  };
  addUrlRequest: URL = {
    id: 0,
    long: '',
    short: '',
    createdBy: '',
    createdDate: '1600-01-01T00:00:00'
  }

  constructor(private urlService: UrlService, private userService: UserService) {


  }
  ngOnInit(): void {
    this.baseUrl = this.urlService.getBaseUrl();
    this.userService
      .getUser()
      .subscribe((result: User) => (this.user = result));

    this.urlService
      .getUrls()
      .subscribe((result: URL[]) => (this.urls = result));
  }
  addUrl() {
    this.urlService.addUrl(this.addUrlRequest)
      .subscribe((response: any) => {
        this.urlService.getUrls()
          .subscribe((result: URL[]) => (this.urls = result));
      })
  }
  deleteUrl(deleteUrlId: number) {
    this.urlService.deleteUrl(deleteUrlId)
      .subscribe((response: any) => {
        this.urlService.getUrls()
          .subscribe((result: URL[]) => (this.urls = result));
      })

  }
  deleteAll() {
    this.urlService.deleteAllUrls()
      .subscribe((response: any) => {
        this.urlService.getUrls()
          .subscribe((result: URL[]) => (this.urls = result));
      })
  }
}
