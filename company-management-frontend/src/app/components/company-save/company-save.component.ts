import { HttpClientModule } from '@angular/common/http';
import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormControl, ReactiveFormsModule, Validators, MaxLengthValidator, FormBuilder, AbstractControl } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { BackendService } from '../../services/backend.service';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { Subscription } from 'rxjs';
import { IndustryDto } from '../../utils/objects/Industry';
import { CompanyDdlDto, CompanyDto, CompanySaveDto } from '../../utils/objects/Company';
import { Router } from '@angular/router';

@Component({
  selector: 'app-company-save',
  standalone: true,
  imports: [ReactiveFormsModule, HttpClientModule, MatSelectModule, MatFormFieldModule, MatInputModule, MatButtonModule],
  templateUrl: './company-save.component.html',
  styleUrl: './company-save.component.scss'
})
export class CompanySaveComponent implements OnInit, OnDestroy {

  @Input('companyId') companyId = 0;

  editCompanyDtoSubs!: Subscription;
  companyDdlSubs!: Subscription;
  industryDdlSubs!: Subscription;
  companyFormSubs!: Subscription;

  industryList: IndustryDto[] = [];
  companyList: CompanyDdlDto[] = [];
  
  isEditMode = false;

  companyForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private backendService: BackendService, private router: Router) {
    this.companyForm = this.formBuilder.group({
      companyName: ['', [Validators.required, Validators.pattern('[a-z A-Z]+')]],
      industry: [0, [Validators.min(1), Validators.required]],
      employeeCount: [0, [Validators.required, Validators.min(1), Validators.max(1000000)]],
      city: ['', [Validators.pattern('[a-z A-Z-]+'), Validators.maxLength(50)]],
      parentCompany: ['None']
    });
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
        this.companyForm.controls['companyName'].patchValue(res.companyName);
        this.companyForm.controls['industry'].patchValue(res.industryId);
        this.companyForm.controls['employeeCount'].patchValue(res.totalEmployees);
        this.companyForm.controls['city'].patchValue(res.city);
        this.companyForm.controls['parentCompany'].patchValue(res.parentCompany);
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

  onFormSubmit(event: Event) {
    event.preventDefault();
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
