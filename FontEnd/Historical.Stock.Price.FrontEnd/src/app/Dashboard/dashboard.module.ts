import {NgModule} from '@angular/core';
import {DashboardRoutingModule} from './dashboard-routing.module';
import {MainComponent} from './Pages/main/main.component';
import { ChartModule } from 'primeng/chart';
import {FinancialChartViewComponent} from './Components/financial-chart-view/financial-chart-view.component';
import {TreeModule} from 'primeng/tree';
import {TreeTableModule} from 'primeng/treetable';
import {TableModule} from 'primeng/table';
import {SplitButtonModule} from 'primeng/splitbutton';
import {CardModule} from 'primeng/card';
import {ButtonModule} from 'primeng/button';
import {DialogModule} from 'primeng/dialog';
import {CommonModule} from '@angular/common';


@NgModule(({
  providers: [],
  declarations: [
    MainComponent,
    FinancialChartViewComponent
  ],
  imports: [
    DashboardRoutingModule,
    ChartModule,
    TreeTableModule,
    TableModule,CardModule,
    ButtonModule,
    DialogModule,
    CommonModule
  ]
}))
export class DashboardModule {}
