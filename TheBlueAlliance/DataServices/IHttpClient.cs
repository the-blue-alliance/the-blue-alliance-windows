using System;
using System.Threading.Tasks;
using Windows.Web.Http;
using Newtonsoft.Json;
using TBA.Models;

namespace TBA
{
    internal static class HttpClientFactory
    {
        static HttpClient client;
        
        internal static HttpClient GetClient()
        {
            if (client == null)
            {
                client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-TBA-App-Id", "synth3tk:test:v0");
            }

            return client;
        }
    }

    public interface IHttpClient
    {
        Task<EventsResponse> Get(string args);
    }

    public class EventHttpClient : IHttpClient
    {
        private EventsResponse eventsResponse = new EventsResponse();
        public async Task<EventsResponse> Get(string args)
        {
            Uri uri = new Uri("http://www.thebluealliance.com/api/v2/event/" + args);
            HttpClient httpClient = HttpClientFactory.GetClient();
            eventsResponse.IsSuccessful = false; // Assume the response is bad until proven successful

            // Always catch network exceptions for async methods
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                string results = await response.Content.ReadAsStringAsync();
                eventsResponse.Data = JsonConvert.DeserializeObject<EventModel>(results);
                eventsResponse.IsSuccessful = true;

                return eventsResponse;
            }
            catch
            {
                // Details in ex.Message and ex.HResult. 
                eventsResponse.Exception = new ArgumentException("API Error");
                return eventsResponse;
            }
        }
    }
}
