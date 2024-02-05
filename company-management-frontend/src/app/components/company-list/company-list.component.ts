import { HttpClientModule } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { BackendService } from '../../services/backend.service';
import { CompanyDto } from '../../utils/objects/Company';

@Component({
  selector: 'app-company-list',
  standalone: true,
  imports: [HttpClientModule, MatTableModule],
  templateUrl: './company-list.component.html',
  styleUrl: './company-list.component.scss'
})
export class CompanyListComponent implements OnInit, OnDestroy {

  unsubDdlList!: Subscription;
  displayedColumns: string[] = ['companyNo', 'companyName', 'industryName', 'totalEmployees', 'city', 'parentCompany', 'level'];
  dataSource = new MatTableDataSource<CompanyDto>(undefined);

  constructor(private backendService: BackendService, private router: Router) {}

  ngOnInit() {
    this.getCompanyList();
  }

  ngOnDestroy() {
    console.log(" ngOnDestroyed called!")
    if (this.unsubDdlList) this.unsubDdlList.unsubscribe();
    this.dataSource.disconnect();
  }

  getCompanyList() {
    this.unsubDdlList = this.backendService.getCompanyList()
      .subscribe(list => {
        this.dataSource = new MatTableDataSource<CompanyDto>(list);
        return list;
      })
  }

  goToSave() {
    this.router.navigate(['save']);
  }
}
