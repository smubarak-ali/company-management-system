import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanySearchFilterComponent } from './company-search-filter.component';

describe('CompanySearchFilterComponent', () => {
  let component: CompanySearchFilterComponent;
  let fixture: ComponentFixture<CompanySearchFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompanySearchFilterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CompanySearchFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
