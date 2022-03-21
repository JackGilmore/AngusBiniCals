using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AngusBiniCals.Models;
using AngusBiniCals.Utilities;
using RestSharp;

namespace AngusBiniCals.Services
{
    public class AddressLookupService
    {
        public async Task<IEnumerable<AddressEntry>> GetAddressesFromPostCode(string postcode)
        {
            return await SearchCAG(postcode);
        }

        public async Task<string> GetAddressFromUPRN(string uprn)
        {
            var searchResults = await SearchCAG(uprn);

            var results = searchResults.ToList();
            if (searchResults == null || !results.Any())
            {
                throw new KeyNotFoundException("No record found for UPRN");
            }

            return results.FirstOrDefault()?.AddressTrimmed;
        }

        private static async Task<IEnumerable<AddressEntry>> SearchCAG(string search)
        {
            var client = new RestClient($"{Constants.SearchUrl}{search}");
            var request = new RestRequest();

            var response = await client.ExecuteAsync(request);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var addresses = JsonSerializer.Deserialize<IEnumerable<AddressEntry>>(response.Content ?? throw new InvalidOperationException("Null response from CAG"),options);

            return addresses;
        }
    }
}
