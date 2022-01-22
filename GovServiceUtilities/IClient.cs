using System.Net;
using System.Text.Json;
using GovServiceUtilities.Models.Lookups;
using RestSharp;
namespace GovServiceUtilities
{
    public interface IClient
    {
        public Task Init();
        public Task<LookupResponse> RequestLookup(string lookupId);
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

        public async Task<LookupResponse> RequestLookup(string lookupId)
        {
            //var lookupRequest = new RestRequest(Constants.Endpoints.RunLookup, Method.Post);
            //lookupRequest.AddParameter("id", lookupId);
            //var body = @"{""stopOnFailure"":true,""user"":{},""formId"":"""",""formValues"":{""Section 1"":{}},""isPublished"":false,""formName"":"""",""tokens"":{""port"":"""",""host"":"""",""site_url"":"""",""site_path"":"""",""site_origin"":"""",""user_agent"":"""",""site_protocol"":"""",""session_id"":"""",""product"":"""",""formLanguage"":"""",""authenticationType"":"""",""isAuthenticated"":true,""api_url"":"""",""transactionReference"":"""",""transaction_status"":"""",""published"":false,""summary"":""Bin collection dates"",""description"":""Bin collection dates""},""site"":"""",""created"":"""",""reference"":"""",""formUri"":""""}";
            //lookupRequest.AddJsonBody(body);

            //var response = await _restClient!.ExecuteAsync(lookupRequest);

            //var client = new RestClient("https://anguscouncil-self.achieveservice.com/apibroker/runLookup?id=61a74a140f9e9");

            var client = _restClient;

            var request = new RestRequest("apibroker/runLookup?id=61a74a140f9e9") {Method = Method.Post};
            //request.AddHeader("content-type", "application/json");
            //request.AddHeader("Cookie", "AWSALB=2PAdnNFoZmA5cS5TnjhRkwNxuLo4Vb8bTaRAto6yrOYTYn6SQCqY71RlehOxEAj734aEUi5WuvTe1uPdnlyhBT2LYKdUrPuzIHBKVYv7ecPR2C1b7UiQLxD4zg6w; AWSALBCORS=2PAdnNFoZmA5cS5TnjhRkwNxuLo4Vb8bTaRAto6yrOYTYn6SQCqY71RlehOxEAj734aEUi5WuvTe1uPdnlyhBT2LYKdUrPuzIHBKVYv7ecPR2C1b7UiQLxD4zg6w; PHPSESSID=b1gtj7mv88l5rvbmq0a5jv6rq3");
            request.AddJsonBody(new LookupRequestPayload());
            //request.AddHeader("content-type", "application/json");
            RestResponse response = await client.ExecuteAsync(request);


            var parsedResponse = JsonSerializer.Deserialize<LookupResponse>(response.Content);
            //Console.WriteLine(response.Content);

            return parsedResponse;

        }
    }
}