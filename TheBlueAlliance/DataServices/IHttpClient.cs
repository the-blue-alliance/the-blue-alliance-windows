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

    public interface IHttpClient<T>
    {
        Task<T> Get(string args);
    }

    public class BaseHttpClient
    {
        public Uri uri(string args)
        {
            Uri _uri = new Uri(Globals.BaseUrl + args);
            return _uri;
        }
    }

    public class EventHttpClient : BaseHttpClient, IHttpClient<EventResponse>
    {
        public async Task<EventResponse> Get(string eventKey)
        {
            EventResponse eventsResponse = new EventResponse();
            HttpClient httpClient = HttpClientFactory.GetClient();
            eventsResponse.IsSuccessful = false; // Assume the response is bad until proven successful

            // Always catch network exceptions for async methods
            try
            {
                Uri _uri = uri("event/");
                HttpResponseMessage response = await httpClient.GetAsync(_uri);
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

        public async Task<EventListResponse> GetAll(int? year = null)
        {
            EventListResponse eventListResponse = new EventListResponse();
            HttpClient httpClient = HttpClientFactory.GetClient();
            string yearString;
            eventListResponse.IsSuccessful = false; // Assume the response is bad until proven successful

            // Always catch network exceptions for async methods
            try
            {
                // If we don't provide a year argument, fallback to the API's default of the current calendar year
                if(year == null)
                {
                    yearString = "";
                }
                else
                {
                    yearString = year.ToString();
                }

                Uri _uri = uri("events/" + yearString);
                HttpResponseMessage response = await httpClient.GetAsync(_uri);
                response.EnsureSuccessStatusCode();
                string results = await response.Content.ReadAsStringAsync();
                eventListResponse.Data = JsonConvert.DeserializeObject<List<EventModel>>(results);
                eventListResponse.IsSuccessful = true;

                return eventListResponse;
            }
            catch
            {
                // Details in ex.Message and ex.HResult. 
                eventListResponse.Exception = new ArgumentException("API Error");
                return eventListResponse;
            }
        }
    }
}