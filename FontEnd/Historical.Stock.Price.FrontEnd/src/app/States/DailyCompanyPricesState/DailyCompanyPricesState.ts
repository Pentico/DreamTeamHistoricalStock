import {Action, Selector, State, StateContext, Store} from '@ngxs/store';
import {DailyCompanyPricesStateModel} from './DailyCompanyPricesStateModel';
import {Injectable} from '@angular/core';
import * as signalR from '@microsoft/signalr';
import {environment} from '../../../environments/environment';
import * as DailyCompanyPricesActions from './DailyCompanyPrices.actions';
import {InvokeHubMethods} from './InvokeHubMethods';
import {OnSaveDailyCompanyPrice} from './DailyCompanyPrices.actions';


@State<DailyCompanyPricesStateModel>({
  name:'DAILY_COMPANY_PRICES',
  defaults: {
    DailyCompanyPrices: []
  }
})
@Injectable()
export class DailyCompanyPricesState {
  private HubConnection: signalR.HubConnection;

  constructor(private store: Store) {
    this.buildConnection();
    this.startConnection();
  }

  @Selector()
  static getDailyCompanyPrices(state: DailyCompanyPricesStateModel): any[] {
    return state.DailyCompanyPrices;
  }




  private buildConnection = () => {
    this.HubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.HubUrl}`,
        {
          skipNegotiation: true,
          transport: signalR.HttpTransportType.WebSockets
        })
      .withAutomaticReconnect()
      .build();
  }

  private startConnection = () => {
    this.HubConnection.start()
      .then(() => {
        this.HubConnection.on(InvokeHubMethods.SentDailyPricesUpdates, (data: any) => {
          this.store.dispatch(new OnSaveDailyCompanyPrice(data))
            .subscribe();
        });

      })
      .catch(err => {
        console.log('Error while starting connection' + err);

        // if you get error try to start connection again after 3 seconds.
        setTimeout(() => {
          this.startConnection();
        }, 3000);
      });
  }


  @Action(DailyCompanyPricesActions.InvokeDailyCompanyPrice)
  InvokeDailyCompanyPrice(ctx: StateContext<DailyCompanyPricesStateModel>, action: DailyCompanyPricesActions.InvokeDailyCompanyPrice): void {
  }

  @Action(DailyCompanyPricesActions.OnSaveDailyCompanyPrice)
  OnSaveDailyCompanyPrice(ctx: StateContext<DailyCompanyPricesStateModel>, action: DailyCompanyPricesActions.OnSaveDailyCompanyPrice): void {
    const state = ctx.getState();
    ctx.setState({
      ...state,
      DailyCompanyPrices: action.payload
    })
  }
}
