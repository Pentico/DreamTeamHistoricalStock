using System.Threading.Tasks;
using DreamTeam.Historical.Stock.Price.Dashboard.Application.Interfaces;
using DreamTeam.Historical.Stock.Price.Dashboard.Application.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;

namespace DreamTeam.Historical.Stock.Price.Dashboard.Application.Services;

public class SignalService: Hub<ISignalService>
{
}