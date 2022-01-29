using System.Text.Json;
using GovServiceUtilities.Models.Lookups;
using RestSharp;
namespace GovServiceUtilities
{
    public interface IClient
    {
        public Task Init();
        public Task<LookupResponse?> RequestLookup(string lookupId, LookupRequestPayload lookupRequestPayload);
    }
    public class Client : IClient
    {
        public string BaseUrl { get; set; }
        private RestClient? _restClient;
        private string? _sessionId;

        public Client(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public async Task Init()
        {
            _restClient = new RestClient(BaseUrl);
            var cookieRequest = new RestRequest();
            var cookieResponse = await _restClient.ExecuteAsync(cookieRequest);

            if (!cookieResponse.IsSuccessful)
            {
                throw new Exception(
                    $"Could not contact base URL {BaseUrl}/n Received response: {cookieResponse.StatusCode}: {cookieResponse.Content}");
            }

            if (cookieResponse.Cookies == null)
            {
                throw new Exception("No cookies returned in response");
            }

            var sessionId =
                cookieResponse.Cookies.FirstOrDefault(cookie => cookie.Name == Constants.SessionIdCookieName);

            if (sessionId == null)
            {
                throw new Exception($"No cookie with name {Constants.SessionIdCookieName} found");
            }

            _sessionId = sessionId.Value;
            _restClient.AddCookie(Constants.SessionIdCookieName, _sessionId);
        }

        public async Task<LookupResponse?> RequestLookup(string lookupId, LookupRequestPayload payload)
        {
            var client = _restClient;
            var request = new RestRequest($"apibroker/runLookup?id={lookupId}") {Method = Method.Post};

            request.AddJsonBody(payload);
            RestResponse response = await client!.ExecuteAsync(request);

            if (string.IsNullOrEmpty(response.Content))
            {
                return null;
            }

            var parsedResponse = JsonSerializer.Deserialize<LookupResponse>(response.Content);
            

            return parsedResponse;
        }
    }
}