using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpaceXLaunchSearchApi.Data.Models.SpaceX;
using SpaceXLaunchSearchApi.Handlers;

namespace SpaceXLaunchSearchApi.Data.Daos.SpaceX
{
  public class LaunchDao : BaseDao<LaunchModel>
  {
    private static string ContentEndpoint = "launches";

    public LaunchDao() : base(ContentEndpoint)
    {
    }

    public LaunchModel GetLaunchById(int id)
    {
      LaunchModel result = null;

      // Try/catch to allow us to log any errors that occur during the 
      // request or data stream.
      try
      {
        // Unpleasant .NET WebRequest code
        string queryString = $"{BaseUrl}{ContentEndpoint}?flight_number={id}";
        WebRequest request = WebRequest.Create(queryString);

        // Using blocks to make sure all these IDisposables are handled
        using (WebResponse response = request.GetResponse())
        using (Stream responseStream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(responseStream))
        {
          string rawResults = reader.ReadToEnd();
          result = JsonConvert.DeserializeObject<IEnumerable<LaunchModel>>(rawResults, new JsonSerializerSettings
          {
            Error = JsonErrorHandlers.HandleDeserializationError
          }).FirstOrDefault();
          reader.Close();
          responseStream.Close();
          response.Close();
        }
      }
      catch (Exception ex)
      {
        // TODO: Implement logging
        Console.WriteLine(ex.Message);
      }

      return result;
    }
  }
}
