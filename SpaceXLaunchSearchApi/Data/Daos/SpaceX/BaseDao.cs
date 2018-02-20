using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using SpaceXLaunchSearchApi.Data.Models.SpaceX;
using SpaceXLaunchSearchApi.Handlers;

namespace SpaceXLaunchSearchApi.Data.Daos.SpaceX
{
    public class BaseDao<T1>
        where T1 : BaseModel
    {
        protected static readonly string BaseUrl = "https://api.spacexdata.com/v2/";
        private string ContentEndpoint { get; }

        public BaseDao(string contentEndpoint)
        {
            ContentEndpoint = contentEndpoint;
        }

        public IEnumerable<T1> GetAll()
        {
            IEnumerable<T1> results = null;

            // Try/catch to allow us to log any errors that occur during the 
            // request or data stream.
            try
            {
                // Unpleasant .NET WebRequest code
                string queryString = $"{BaseUrl}{ContentEndpoint}";
                WebRequest request = WebRequest.Create(queryString);

                // Using blocks to make sure all these IDisposables are handled
                using (WebResponse response = request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string rawResults = reader.ReadToEnd();
                    results = JsonConvert.DeserializeObject<IEnumerable<T1>>(rawResults, new JsonSerializerSettings
                    {
                        Error = JsonErrorHandlers.HandleDeserializationError
                    });
                    reader.Close();
                    responseStream.Close();
                    response.Close();
                }
            }
            catch (Exception ex)
            {
                // I was taught in the past that we should never do any routing
                // based on exceptions. I would output exceptions to a log here
                // or rethrow them as more "developer" friendly, categorized
                // exceptions. In this case, I'll just write it to console, and
                // allow the program's execution to continue as there is null
                // result set handling in the controller.
                // TODO: Implement logging
                Console.WriteLine(ex.Message);
            }

            return results;
        }
    }
}
