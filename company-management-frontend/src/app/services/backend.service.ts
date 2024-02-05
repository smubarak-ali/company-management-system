import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { CompanyDdlDto, CompanyDto } from '../utils/objects/Company';

@Injectable({
  providedIn: 'root'
})
export class BackendService {

  readonly BASE_URL = environment.backendBaseUrl;

  constructor(private http: HttpClient) {}

  getCompanyList() {
    const endpoint = `${this.BASE_URL}/company`;
    return this.http.get<CompanyDto[]>(endpoint);
  }

  getCompanyListForDdl() {
    const endpoint = `${this.BASE_URL}/company/ddl`;
    return this.http.get<CompanyDdlDto[]>(endpoint);
  }
}
