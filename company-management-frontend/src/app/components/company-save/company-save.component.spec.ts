import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanySaveComponent } from './company-save.component';

describe('CompanySaveComponent', () => {
  let component: CompanySaveComponent;
  let fixture: ComponentFixture<CompanySaveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompanySaveComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CompanySaveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
