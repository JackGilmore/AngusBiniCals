using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AngusBiniCals.Models;
using AngusBiniCals.Utilities;
using Jint;
using RestSharp;

namespace AngusBiniCals.Services
{
    public class PostcodeLookupService
    {
        public async Task<IEnumerable<AddressEntry>> GetAddressesFromPostCode(string postcode)
        {
            var client = new RestClient(Constants.AddressSearchURL);
            var request = new RestRequest(Method.GET);
            request.AddParameter("txtSearch", postcode);

            var response = await client.ExecuteAsync(request);

            var engine = new Engine()
                .Execute(response.Content)
                .Execute("var addressJson = JSON.stringify(addresses)");

            var addressJson = engine.GetValue("addressJson").AsString();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var addresses = JsonSerializer.Deserialize<IEnumerable<AddressEntry>>(addressJson,options);

            return addresses;
        }
    }
}
