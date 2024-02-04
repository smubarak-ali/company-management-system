import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { BackendService } from '../../services/backend.service';

@Component({
  selector: 'app-company-list',
  standalone: true,
  imports: [HttpClientModule],
  templateUrl: './company-list.component.html',
  styleUrl: './company-list.component.scss'
})
export class CompanyListComponent implements OnInit {

  constructor(private backendService: BackendService) {}

  ngOnInit() {
    this.getCompanyList();
  }

  getCompanyList() {
    this.backendService.getCompanyList()
      .subscribe(list => {
        console.log(" list: ", list);
      });
  }
}
