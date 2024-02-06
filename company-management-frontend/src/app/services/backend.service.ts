import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { CompanyDdlDto, CompanyDto, CompanySaveDto } from '../utils/objects/Company';
import { IndustryDto } from '../utils/objects/Industry';
import { ApiPaginationResponse, CompanySearchCriteria } from '../utils/objects/ApiModel';

@Injectable({
  providedIn: 'root'
})
export class BackendService {

  readonly BASE_URL = environment.backendBaseUrl;

  constructor(private http: HttpClient) {}

  getCompanyList(criteria: CompanySearchCriteria) {
    let endpoint = `${this.BASE_URL}/company?`;
    if (criteria.city) endpoint += `city=${criteria.city}&`;
    if (criteria.companyName) endpoint += `companyName=${criteria.companyName}&`;
    if (criteria.companyNo) endpoint += `companyNo=${criteria.companyNo}&`;
    if (criteria.industryId) endpoint += `industryId=${criteria.industryId}&`;
    if (criteria.parentCompany) endpoint += `parentCompany=${criteria.parentCompany}&`;
    if (criteria.pageIndex) endpoint += `pageIndex=${criteria.pageIndex}&`;
    if (criteria.pageSize) endpoint += `pageSize=${criteria.pageSize}&`;
    endpoint += `sortByCompanyNameDesc=${criteria.sortByCompanyNameDesc}&`;
    console.log(" endpoint: ", endpoint);
    return this.http.get<ApiPaginationResponse<CompanyDto>>(endpoint);
  }

  getCompanyById(id: number) {
    const endpoint = `${this.BASE_URL}/company/${id}`;
    return this.http.get<CompanyDto>(endpoint);
  }

  getCompanyListForDdl() {
    const endpoint = `${this.BASE_URL}/company/ddl`;
    return this.http.get<CompanyDdlDto[]>(endpoint);
  }

  getIndustryList() {
    const endpoint = `${this.BASE_URL}/industry`;
    return this.http.get<IndustryDto[]>(endpoint);
  }

  saveCompany(dto: CompanySaveDto) {
    const endpoint = `${this.BASE_URL}/company`;
    return this.http.post<number>(endpoint, dto);
  }

  updateCompany(id: number, dto: CompanySaveDto) {
    const endpoint = `${this.BASE_URL}/company/${id}`;
    return this.http.put(endpoint, dto);
  }
}
