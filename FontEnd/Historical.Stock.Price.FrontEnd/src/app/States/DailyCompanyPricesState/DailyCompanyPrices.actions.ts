

export class InvokeDailyCompanyPrice {
  static readonly type = '[DailyCompanyPrices] InvokeDailyCompanyPrice';
  constructor() {
  }
}

export class OnSaveDailyCompanyPrice {
  static readonly type = '[DailyCompanyPrices] OnSaveDailyCompanyPrice';
  constructor(public payload: any) {
  }
}
