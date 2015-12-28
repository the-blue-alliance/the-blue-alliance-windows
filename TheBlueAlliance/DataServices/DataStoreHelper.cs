using System.Linq;
using SQLite.Net;
using TBA.Models;
using System.IO;

namespace TBA.DataServices
{
    class DataStoreHelper
    {
        // Initialize the database
        public static string DBPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "tba.sqlite");
        private static SQLiteConnection db = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DBPath);

        public static SQLiteConnection GetDataStore()
        {
            var EventModelTable = db.GetTableInfo("EventModel");

            if (!EventModelTable.Any())
            {
                db.CreateTable<EventModel>();
            }

            return db;
        }

        public static bool IsPopulated()
        {
            var count = db.ExecuteScalar<int>("SELECT COUNT(*) FROM EventModel LIMIT 0, 1");

            if (count==0)
            {
                return false;
            } else
            {
                return true;
            }
        }

        public static void PopulateDB()
        {

        }
    }
}
