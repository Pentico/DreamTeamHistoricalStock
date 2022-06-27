using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DreamTeam.Historical.Stock.Price.Dashboard.Application.IAbstracts;
using DreamTeam.Historical.Stock.Price.Dashboard.Application.Interfaces;
using DreamTeam.Historical.Stock.Price.Dashboard.Application.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sgbj.Cron;

namespace DreamTeam.Historical.Stock.Price.Dashboard.Application.Services;

public class DailyPricesHostedServices: IDailyPricesHostedServices
{
    
    private readonly string _schedule;
    private readonly string _httpClientUrl;
    private readonly string _rapidApiHost;
    private readonly string _rapidApiKey;
    private readonly string[] _companySymbolList;
    private readonly ILogger<DailyPricesHostedServices> _logger;
    private readonly IHubContext<SignalService, ISignalService> _signalService;

    public DailyPricesHostedServices(IOptions<ConfigSettings> configSettings, ILogger<DailyPricesHostedServices> logger, IHubContext<SignalService, ISignalService> iSignalService)
    {
        _signalService = iSignalService;
        _schedule = configSettings.Value.GetDailyPricesSchedule;
        _logger = logger;
        _rapidApiKey = configSettings.Value.RapidApiKey;
        _rapidApiHost = configSettings.Value.RapidApiHost;
        _httpClientUrl = configSettings.Value.RapidClientUrl;
        _companySymbolList = configSettings.Value.CompanySymbolList;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Every day at 8am local time "0 8 * * *"'
        _logger.LogInformation(" Every time at {Schedule}", _schedule);
        using var timer = new CronTimer( _schedule, TimeZoneInfo.Local);
        VoidGetDailyPrices(); 
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            _logger.LogInformation(" Task  Started at {UtcNow}", DateTime.UtcNow);
            // Do work
             VoidGetDailyPrices();

            _logger.LogInformation(" Task  Completed at {UtcNow}", DateTime.UtcNow);
        }
    }

    protected override async Task<bool> VoidGetDailyPrices()
    {
        try
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", _rapidApiKey);
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", _rapidApiHost);
            var list = new List<string>();
            
            /**
             * I don't like this part below because i could have handle the call match better and the data cleaning part. 
             */
            foreach (var s in _companySymbolList)
            {
                string msg;
                try
                {
                    msg = client.GetStringAsync($"{_httpClientUrl}&symbol={s}").Result;
                }
                catch (Exception e)
                {
                    _logger.LogCritical(" Error Updating Daily Prices {UtcNow}", DateTime.UtcNow);
                    break;
                }

                if (msg.Contains("Error Message")) continue;
                
                msg = msg.Replace(@"Meta Data", "MetaData");
                msg = msg.Replace(@"Time Series (Daily)", "TimeSeriesDaily");
            
            
                msg = msg.Replace(@"Last Refreshed", "LastRefreshed");
                msg = msg.Replace(@"Output Size", "OutputSize");
                msg = msg.Replace(@"Time Zone", "TimeZone");
                msg = msg.Replace(@"1. ", "");
                msg = msg.Replace(@"2. ", "");
                msg = msg.Replace(@"3. ", "");
                msg = msg.Replace(@"4. ", "");
                msg = msg.Replace(@"5. ", "");
                
                list.Add(msg);
            }

            if (list.Count > 0)
            {
                await this._signalService.Clients.All.DailyPriceSymbolMessage(list);   
            }
            return true;
        }
        catch (Exception e)
        {
            _logger.LogCritical(" Error Updating Daily Prices {UtcNow}", DateTime.UtcNow);
            throw;
        }
    }
}