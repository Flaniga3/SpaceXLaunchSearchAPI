using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace SpaceXLaunchSearchApi.Handlers
{
    public class JsonErrorHandlers
    {
        public static void HandleDeserializationError(object sender, ErrorEventArgs args)
        {
            Console.WriteLine(args.ErrorContext.Error.Message);
            args.ErrorContext.Handled = true;
        }
    }
}
