using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceXLaunchSearchApi.Data.Daos.SpaceX;
using SpaceXLaunchSearchApi.Data.Models.SpaceX;

namespace SpaceXLaunchSearchApi.Controllers
{
  [Route("api/[controller]")]
  public class LaunchesController : Controller
  {
    [HttpGet]
    public IEnumerable<LaunchModel> QueryLaunches([FromQuery] string query)
    {
      IEnumerable<LaunchModel> launchesUnfiltered = new LaunchDao().GetAll();

      if (query != null)
      {
        // Less verbose code by setting commonly used terms up here
        string queryLower = query.ToLower();
        bool? successQuery = ConvertSuccessFail(queryLower);

        // Just check each member for matches
        return launchesUnfiltered.Where(x =>
            successQuery != null && x.LaunchSuccess == successQuery
            || !String.IsNullOrWhiteSpace(x.Details) && x.Details.ToLower().Contains(queryLower)
            || Int16.TryParse(query, out var queryInt) && x.FlightNumber == queryInt
            || !String.IsNullOrWhiteSpace(x.Rocket.Name) && x.Rocket.Name.ToLower().Contains(queryLower)
            || !String.IsNullOrWhiteSpace(x.Rocket.Id) && x.Rocket.Id.ToLower().Contains(queryLower)
            || !String.IsNullOrWhiteSpace(x.Rocket.Type) && x.Rocket.Type.ToLower().Contains(queryLower)
            || !String.IsNullOrWhiteSpace(x.LaunchSite.Name) && x.LaunchSite.Name.ToLower().Contains(queryLower)
            || !String.IsNullOrWhiteSpace(x.LaunchSite.Id) && x.LaunchSite.Id.ToLower().Contains(queryLower)
            || !String.IsNullOrWhiteSpace(x.LaunchSite.LongName) && x.LaunchSite.LongName.ToLower().Contains(queryLower)
            || DateTime.TryParse(query, out var queryDate) && (x.LaunchLocalDateTime.Date.Equals(queryDate) || x.LaunchUtcDateTime.Date.Equals(queryDate))
        ).OrderBy(x => x.FlightNumber);
      }

      return launchesUnfiltered;
    }

    [HttpGet("{id}")]
    public LaunchModel GetLaunch(int id)
    {
      LaunchModel model = new LaunchDao().GetLaunchById(id);

      return model;
    }

    private bool? ConvertSuccessFail(string query)
    {
      string queryLower = query.ToLower();

      // Check for a few key "success/fail" terms and check launch status
      if (queryLower.Equals("failure") || queryLower.Equals("failed") || queryLower.Equals("fail"))
        return false;
      else if (queryLower.Equals("success") || queryLower.Equals("succeeded") || queryLower.Equals("succeed"))
        return true;

      return null;
    }
  }
}
