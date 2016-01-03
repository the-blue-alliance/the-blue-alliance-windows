using System.Collections.Generic;
using System.IO;
using System.Linq;
using TBA.Common;
using TBA.DataServices;
using TBA.Models;

namespace TBA.Caches
{
    public interface IStatusCache
    {
        bool HasCached();
        StatusModel GetCachedItem();
        void Put(StatusModel updatedModel);
    }

    public class StatusCache : IStatusCache
    {
        private string statusFile = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "apistatus.json");
        public bool HasCached()
        {
            if (!File.Exists(statusFile))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public StatusModel GetCachedItem()
        {
            StatusModel _Status = JsonFileHelper.ReadFromJsonFile<StatusModel>(statusFile);

            return _Status;
        }

        public void Put(StatusModel updatedModel)
        {
            JsonFileHelper.WriteToJsonFile(statusFile, updatedModel, true);
        }
    }
}
