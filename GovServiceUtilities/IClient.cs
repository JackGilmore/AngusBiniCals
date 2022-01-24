using System.Text.Json;
using GovServiceUtilities.Models.Lookups;
using RestSharp;
namespace GovServiceUtilities
{
    public interface IClient
    {
        public Task Init();
        public Task<LookupResponse?> RequestLookup(string lookupId);
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

        public async Task<LookupResponse?> RequestLookup(string lookupId)
        {


            //var client = new RestClient("https://anguscouncil-self.achieveservice.com/apibroker/runLookup?id=61a74a140f9e9");

            var client = _restClient;

            var request = new RestRequest("apibroker/runLookup?id=61a74a140f9e9") {Method = Method.Post};
            // TODO: Shift this into CalendarService and make the designated payload a static var or template
            request.AddJsonBody(new LookupRequestPayload());
            //request.AddHeader("content-type", "application/json");
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