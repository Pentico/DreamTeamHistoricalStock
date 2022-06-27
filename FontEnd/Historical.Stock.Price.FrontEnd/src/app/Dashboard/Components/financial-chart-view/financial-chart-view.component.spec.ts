import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FinancialChartViewComponent } from './financial-chart-view.component';

describe('FinancialChartViewComponent', () => {
  let component: FinancialChartViewComponent;
  let fixture: ComponentFixture<FinancialChartViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FinancialChartViewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FinancialChartViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
