using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using TBA.Caches;
using TBA.Models;

namespace TBA.Common
{
    class StatusHelper
    {
        public void UpdateStatus()
        {
            StatusHttpClient httpClient = new StatusHttpClient();
            StatusCache statusCache = new StatusCache();

            var statusResponse = httpClient.Get().Result;
            if (!statusResponse.IsSuccessful)
            {
                throw statusResponse.Exception;
            }
            else
            {
                statusCache.Put(statusResponse.Data);
            }
        }

        public StatusModel GetStatus()
        {
            StatusCache statusCache = new StatusCache();
            UpdateStatus();
            StatusModel _status = statusCache.GetCachedItem();
            return _status;
        }
    }
}
