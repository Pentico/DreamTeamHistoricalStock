import { Component, OnInit } from '@angular/core';
import {Select, Selector} from '@ngxs/store';
import {DailyCompanyPricesState} from '../../../States/DailyCompanyPricesState/DailyCompanyPricesState';
import {Observable} from 'rxjs';
import {compose} from '@ngxs/store/operators';

@Component({
  selector: 'app-financial-chart-view',
  templateUrl: './financial-chart-view.component.html',
  styleUrls: ['./financial-chart-view.component.scss']
})
export class FinancialChartViewComponent implements OnInit {
  @Select(DailyCompanyPricesState.getDailyCompanyPrices)
  getAllDailyPrices: Observable<any[]>;
  items: any[] = [];
  display: boolean = false;
  arr: any[] = [];
  constructor() {
    this.onSubscribeToDailyPrices()
  }

  update(): void {
  }

  ngOnInit(): void {
  }

  onSubscribeToDailyPrices() {
    this.getAllDailyPrices.subscribe(items => {
      this.items = [];
      items.forEach( price =>{
        this.items.push(JSON.parse(price));
      })
      console.log(this.items);
    })
  }

  onViewPRices(Symbol: any) {
    console.error(this.items);
    const object = this.items.filter(x => x.MetaData.Symbol === Symbol).pop().TimeSeriesDaily;
    let secArr: any = [];
    Object.keys(object).map(function(key){
      // @ts-ignore
      object[key].Date = key;
      secArr.push(object[key])
    });
    this.arr = secArr;
    this.display = true;
  }

  onhide() {
    this.display = false;
  }
}
