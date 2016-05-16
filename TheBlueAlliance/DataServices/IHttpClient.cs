using System;
using System.Threading.Tasks;
using Windows.Web.Http;
using Newtonsoft.Json;
using TBA.Models;
using System.Collections.Generic;
using TBA.DataServices;
using TBA.Common;

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
                client.DefaultRequestHeaders.Add("X-TBA-App-Id", Constants.AppId);
            }

            return client;
        }
    }

    public class BaseHttpClient
    {
        public HttpClient httpClient = HttpClientFactory.GetClient();
        public Uri uri(string args)
        {
            Uri _uri = new Uri(Constants.BaseUrl + args);
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

    public class TeamHttpClient : BaseHttpClient
    {
        public async Task<TeamResponse> Get(string teamKey)
        {
            var response = await GetAndDeserialize<TeamModel>(uri("team/" + teamKey));
            TeamResponse teamResponse = new TeamResponse();


            if (teamResponse.IsSuccessful = response.Item1 != null)
            {
                teamResponse.Data = response.Item1;
            }
            else { teamResponse.Exception = response.Item2; }
            return teamResponse;
        }

        public async Task<TeamListResponse> GetPage(int? page = 0)
        {
            var response = await GetAndDeserialize<List<TeamModel>>(uri("teams/" + page.ToString()));
            TeamListResponse teamListResponse = new TeamListResponse();


            if (teamListResponse.IsSuccessful = response.Item1 != null)
            {
                teamListResponse.Data = response.Item1;
            }
            else { teamListResponse.Exception = response.Item2; }
            return teamListResponse;
        }

        // This really isn't a part of the httpclient. Consider moving.
        public List<TeamModel> GetAll()
        {
            bool morePages = true;
            int page = 0;
            List<TeamModel> _teamList = new List<TeamModel>();

            do
            {
                List<TeamModel> pageResults = GetPage(page).Result.Data;
                if (pageResults.Count == 0)
                {
                    morePages = false;
                }
                _teamList.AddRange(pageResults);
                page++;
            } while (morePages == true);

            return _teamList;
        }
    }

    public class DistrictHttpClient : BaseHttpClient
    {
        public async Task<EventResponse> Get(string eventKey)
        {
            var response = await GetAndDeserialize<EventModel>(uri("event/" + eventKey));
            EventResponse eventsResponse = new EventResponse();


            if (eventsResponse.IsSuccessful = response.Item1 != null)
            {
                eventsResponse.Data = response.Item1;
            }
            else { eventsResponse.Exception = response.Item2; }
            return eventsResponse;
        }

        public async Task<DistrictListResponse> GetAll(int? year = null)
        {
            string yearString;
            // If we don't provide a year argument, provide the current max year
            if (year == null)
            {
                yearString = Constants.MaxSeason().ToString();
            }
            else
            {
                yearString = year.ToString();
            }

            var response = await GetAndDeserialize<List<DistrictListModel>>(uri("districts/" + yearString));
            DistrictListResponse districtListResponse = new DistrictListResponse();
            districtListResponse.Year = yearString;

            if (districtListResponse.IsSuccessful = response.Item1 != null)
            {
                districtListResponse.Data = response.Item1;
            }
            else { districtListResponse.Exception = response.Item2; }
            return districtListResponse;
        }
    }

    public class StatusHttpClient : BaseHttpClient
    {
        public async Task<StatusResponse> Get()
        {
            var response = await GetAndDeserialize<StatusModel>(uri("status"));
            StatusResponse statusResponse = new StatusResponse();


            if (statusResponse.IsSuccessful = response.Item1 != null)
            {
                statusResponse.Data = response.Item1;
            }
            else { statusResponse.Exception = response.Item2; }
            return statusResponse;
        }
    }
}