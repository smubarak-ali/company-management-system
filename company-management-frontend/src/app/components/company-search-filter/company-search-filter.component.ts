import { Component, Input, OnDestroy, OnInit, Signal, input, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectChange, MatSelectModule } from '@angular/material/select';
import { BackendService } from '../../services/backend.service';
import { Subscription } from 'rxjs';
import { IndustryDto } from '../../utils/objects/Industry';
import { CompanyDdlDto } from '../../utils/objects/Company';

@Component({
  selector: 'app-company-search-filter',
  standalone: true,
  imports: [MatSelectModule, MatFormFieldModule, MatInputModule, MatButtonModule],
  templateUrl: './company-search-filter.component.html',
  styleUrl: './company-search-filter.component.scss'
})
export class CompanySearchFilterComponent implements OnInit, OnDestroy {
  companyDdlSubs!: Subscription;
  industryDdlSubs!: Subscription;
  companyFormSubs!: Subscription;

  industryList: IndustryDto[] = [];
  companyList: CompanyDdlDto[] = [];

  @Input() companyNo = signal<number|undefined>(undefined);
  @Input() companyName = signal<string|undefined>(undefined);
  @Input() industryId = signal<number|undefined>(undefined);
  @Input() city = signal<string|undefined>(undefined);
  @Input() totalEmployees = signal<number|undefined>(undefined);
  @Input() parentCompany = signal<string|undefined>(undefined);
  
  constructor(private backendService: BackendService) {}

  ngOnDestroy(): void {
        if (this.companyDdlSubs) this.companyDdlSubs.unsubscribe();
    if (this.industryDdlSubs) this.industryDdlSubs.unsubscribe();
  }
  ngOnInit(): void {
    this.getCompanyDdl();
    this.getIndustryDdl();
  }
  
  getCompanyDdl() {
    this.companyDdlSubs = this.backendService.getCompanyListForDdl()
      .subscribe(list => {
        this.companyList = list;
        return list;
      });
  }

  getIndustryDdl() {
    this.companyDdlSubs = this.backendService.getIndustryList()
      .subscribe(list => {
        this.industryList = list;
        return list;
      });
  }

  onCompanyNoChange(event: Event) {
    const val = (event.target as HTMLInputElement).value;
    this.companyNo.set(+val);
  }

  onCompanyNameChange(event: Event) {
    const val = (event.target as HTMLInputElement).value;
    this.companyName.set(val);
  }

  onIndustryChange(changed: MatSelectChange) {
    const value = changed.value;
    this.industryId.set(+value);
  }

  onCityChange(event: Event) {
    const val = (event.target as HTMLInputElement).value;
    this.city.set(val);
  }

  onTotalEmployeeChange(event: Event) {
    const val = (event.target as HTMLInputElement).value;
    this.totalEmployees.set(+val);
  }

  onParentCompanyChange(changed: MatSelectChange) {
    const value = changed.value;
    this.parentCompany.set(value);
  }

}
