using System;
using System.Threading.Tasks;
using Windows.Web.Http;
using Newtonsoft.Json;
using TBA.Models;
using System.Collections.Generic;

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
                client.DefaultRequestHeaders.Add("X-TBA-App-Id", Globals.AppId);
            }

            return client;
        }
    }

    public class BaseHttpClient
    {
        public HttpClient httpClient = HttpClientFactory.GetClient();
        public Uri uri(string args)
        {
            Uri _uri = new Uri(Globals.BaseUrl + args);
            return _uri;
        }

        protected async Task<Tuple<T, Exception>> GetAndDeserialize<T>(Uri uri) where T : class
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                string results = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(results);

                return new Tuple<T, Exception>(result, null);
            }
            catch (Exception ex)
            {
                return new Tuple<T, Exception>(null, ex);
            }

        }
    }

    public class EventHttpClient : BaseHttpClient
    {
        public async Task<EventResponse> Get(string eventKey)
        {
            var response = await GetAndDeserialize<EventModel>(uri("event/"+eventKey));
            EventResponse eventsResponse = new EventResponse();


            if (eventsResponse.IsSuccessful = response.Item1 != null)
            {
                eventsResponse.Data = response.Item1;
            }
            else { eventsResponse.Exception = response.Item2; }
            return eventsResponse;
        }

        public async Task<EventListResponse> GetAll(int? year = null)
        {
            string yearString;
            // If we don't provide a year argument, fallback to the API's default of the current calendar year
            if (year == null)
            {
                yearString = "";
            }
            else
            {
                yearString = year.ToString();
            }

            var response = await GetAndDeserialize<List<EventModel>>(uri("events/" + yearString));
            EventListResponse eventListResponse = new EventListResponse();


            if (eventListResponse.IsSuccessful = response.Item1 != null)
            {
                eventListResponse.Data = response.Item1;
            }
            else { eventListResponse.Exception = response.Item2; }
            return eventListResponse;
        }
    }

            {
                // Details in ex.Message and ex.HResult. 
                eventListResponse.Exception = new ArgumentException("API Error");
                return eventListResponse;
            }
        }
    }
}