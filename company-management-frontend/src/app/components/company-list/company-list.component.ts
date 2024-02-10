import { HttpClientModule } from '@angular/common/http';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild, computed, effect, signal } from '@angular/core';
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

  private PAGE_SIZE = 5;
  private pageIndex$ = signal(1);
  unsubDdlList!: Subscription;
  
  items: CompanyDto[];
  totalCount = 0;
  isLoadingResults = true;
  sortByCompanyNameDesc = false;
  pageIndex = computed(() => this.pageIndex$());
  
  companyNo = signal<number|undefined>(undefined);
  companyName = signal<string|undefined>(undefined);
  industryId = signal<number|undefined>(undefined);
  city = signal<string|undefined>(undefined);
  totalEmployees = signal<number|undefined>(undefined);
  parentCompany = signal<string|undefined>(undefined);

  constructor(private backendService: BackendService, private router: Router) {
    this.items = [];
    effect(() => {
      this.getCompanyList(this.companyNo(), this.companyName(), this.industryId(), this.city(), this.totalEmployees(), this.parentCompany(), this.pageIndex());
    })
  }

  ngOnInit() {
    
  }

  ngOnDestroy() {
    console.log(" ngOnDestroyed called!")
    if (this.unsubDdlList) this.unsubDdlList.unsubscribe();
  }

  incrementPageIndex(event: MouseEvent) {
    event.stopPropagation();
    const furtherPagesAvailable = Math.ceil(this.totalCount / this.PAGE_SIZE);
    if (this.pageIndex() === furtherPagesAvailable) return;
    this.pageIndex$.set(this.pageIndex() + 1);
  }

  decrementPageIndex(event: MouseEvent) {
    event.stopPropagation();
    if (this.pageIndex() === 1) return;
    this.pageIndex$.set(this.pageIndex() - 1);
  }

  getCompanyList(companyNo?: number, companyName?: string, industryId?: number, city?: string, totalEmployees?: number, parentCompany?: string, pageIndex = 1, pageSize = this.PAGE_SIZE) {
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
