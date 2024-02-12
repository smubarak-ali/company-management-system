import { HttpClientModule } from '@angular/common/http';
import { AfterContentChecked, ChangeDetectorRef, Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormControl, ReactiveFormsModule, Validators, FormBuilder } from '@angular/forms';
import { BackendService } from '../../services/backend.service';
import { Subscription } from 'rxjs';
import { IndustryDto } from '../../utils/objects/Industry';
import { CompanyDdlDto, CompanyDto, CompanySaveDto } from '../../utils/objects/Company';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-company-save',
  standalone: true,
  imports: [ReactiveFormsModule, HttpClientModule],
  templateUrl: './company-save.component.html',
  styleUrl: './company-save.component.scss'
})
export class CompanySaveComponent implements OnInit, OnDestroy, AfterContentChecked {

  @Input('companyId') companyId = 0;

  editCompanyDtoSubs!: Subscription;
  companyDdlSubs!: Subscription;
  industryDdlSubs!: Subscription;
  companyFormSubs!: Subscription;

  industryList: IndustryDto[] = [];
  companyList: CompanyDdlDto[] = [];

  isEditMode = false;

  companyForm: FormGroup;
  companyName = new FormControl('', [Validators.required, Validators.pattern('[a-z A-Z]+')]);
  industry = new FormControl(0, [Validators.min(1), Validators.required]);
  employeeCount = new FormControl(0, [Validators.required, Validators.min(1), Validators.max(1000000)]);
  city = new FormControl('', [Validators.pattern('[a-z A-Z-]+'), Validators.maxLength(50)]);
  parentCompany = new FormControl('None');

  constructor(private formBuilder: FormBuilder, private backendService: BackendService, private router: Router, private changeDetector: ChangeDetectorRef) {
    this.companyForm = this.formBuilder.group({
      companyName: this.companyName,
      industry: this.industry,
      employeeCount: this.employeeCount,
      city: this.city,
      parentCompany: this.parentCompany
    });
  }

  ngAfterContentChecked() {
    this.changeDetector.detectChanges();
  }

  ngOnInit() {
    this.getCompanyDdl();
    this.getIndustryDdl();

    if (this.companyId > 0) {
      this.isEditMode = true;
      this.getCompanyById(this.companyId);
    }
  }

  ngOnDestroy() {
    if (this.companyDdlSubs) this.companyDdlSubs.unsubscribe();
    if (this.industryDdlSubs) this.industryDdlSubs.unsubscribe();
    if (this.companyFormSubs) this.companyFormSubs.unsubscribe();
  }

  getCompanyById(id: number) {
    this.editCompanyDtoSubs = this.backendService.getCompanyById(id)
      .subscribe(res => {
        this.companyName.patchValue(res.companyName);
        this.industry.patchValue(res.industryId);
        this.employeeCount.patchValue(res.totalEmployees);
        this.city.patchValue(res.city);
        this.parentCompany.patchValue(res.parentCompany);
      });
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

  onFormSubmit(event: MouseEvent) {
    event.preventDefault();
    console.log(" companyForm: ", this.companyForm.valid);
    if (this.companyForm.invalid) return;

    const formData = this.companyForm.value;
    const dto: CompanySaveDto = {
      city: formData.city || "",
      companyName: `${formData.companyName}`,
      industryId: formData.industry!,
      totalEmployees: formData.employeeCount!,
      parentCompany: `${formData.parentCompany}`
    }

    if (this.isEditMode) {
      this.companyFormSubs = this.backendService.updateCompany(this.companyId, dto).subscribe(res => this.cancel());
    } else {
      this.companyFormSubs = this.backendService.saveCompany(dto).subscribe(res => this.cancel());
    }
  }

  cancel() {
    this.router.navigate(["/"]);
  }

  //In this function, we can add more logic to check individual formcontrol error and return appropriate message.
  getErrorMessage() {
    return 'Invalid input, please try again';
  }

  // parentCompanyValidtor(control: AbstractControl) {
  //   const selectedCompanyId: number = control.value;

  //   if (selectedCompanyId == null || selectedCompanyId == 0) 
  //     return;

  //   const valid = selectedCompanyId !== this.companyId;

  //   return valid ? null : { parentCompanyMatch: true };
  // }
}
