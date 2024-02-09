import { HttpClientModule } from '@angular/common/http';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild, effect, signal } from '@angular/core';
import { Subscription } from 'rxjs';
import { Router, RouterLink } from '@angular/router';

import { CompanyDto } from '../../utils/objects/Company';
import { BackendService } from '../../services/backend.service';
import { CompanySearchFilterComponent } from '../company-search-filter/company-search-filter.component';

@Component({
  selector: 'app-company-list',
  standalone: true,
  imports: [HttpClientModule, RouterLink, CompanySearchFilterComponent],
  templateUrl: './company-list.component.html',
  styleUrl: './company-list.component.scss'
})
export class CompanyListComponent implements OnInit, OnDestroy {

  sortByCompanyNameDesc = false;
  totalCount = 0;
  isLoadingResults = true;
  unsubDdlList!: Subscription;
  // displayedColumns: string[] = ['companyNo', 'companyName', 'industryName', 'totalEmployees', 'city', 'parentCompany', 'level'];
  items: CompanyDto[];
  
  companyNo = signal<number|undefined>(undefined);
  companyName = signal<string|undefined>(undefined);
  industryId = signal<number|undefined>(undefined);
  city = signal<string|undefined>(undefined);
  totalEmployees = signal<number|undefined>(undefined);
  parentCompany = signal<string|undefined>(undefined);

  constructor(private backendService: BackendService, private router: Router) {
    this.items = [];
    effect(() => {
      this.getCompanyList(this.companyNo(), this.companyName(), this.industryId(), this.city(), this.totalEmployees(), this.parentCompany());
    })
  }

  ngOnInit() {
    // this.getCompanyList();
  }

  ngOnDestroy() {
    console.log(" ngOnDestroyed called!")
    if (this.unsubDdlList) this.unsubDdlList.unsubscribe();
  }

  getCompanyList(companyNo?: number, companyName?: string, industryId?: number, city?: string, totalEmployees?: number, parentCompany?: string, pageIndex = 1, pageSize = 5) {
    this.unsubDdlList = this.backendService.getCompanyList({ sortByCompanyNameDesc: this.sortByCompanyNameDesc, pageIndex, pageSize, 
              companyNo, companyName, industryId, city, parentCompany, totalEmployees })
      .subscribe(list => {
        this.items = list.items;
        this.totalCount = list.totalPages;
        this.isLoadingResults = false;
        return list;
      })
  }

  selectedRow(row: CompanyDto) {
    this.router.navigate(["/save", { companyId: row.id }]);
  }

  // announceSortChange(sortState: Sort) {
  //   this.isLoadingResults = true;
  //   if (sortState.direction === "asc") {
  //     this.sortByCompanyNameDesc = false;
  //     this.getCompanyList(this.companyNo(), this.companyName(), this.industryId(), this.city(), this.totalEmployees(), this.parentCompany());
  //   } else {
  //     this.sortByCompanyNameDesc = true;
  //     this.getCompanyList(this.companyNo(), this.companyName(), this.industryId(), this.city(), this.totalEmployees(), this.parentCompany());
  //   }
  // }

  // pageChanged(event: PageEvent) {
  //   console.log({ event });
  //   this.isLoadingResults = true;
  //   this.getCompanyList(this.companyNo(), this.companyName(), this.industryId(), this.city(), this.totalEmployees(), this.parentCompany(), ++event.pageIndex, event.pageSize);
  // }

}
