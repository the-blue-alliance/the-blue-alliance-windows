using System.Linq;
using SQLite.Net;
using TBA.Models;
using System.IO;
using System;
using System.Collections.Generic;

namespace TBA.DataServices
{
    class DataStoreHelper
    {
        // Initialize the database
        public static string DBPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "tba.sqlite");
        public static SQLiteConnection db = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DBPath);

        public static bool IsPopulated(string table)
        {
            var count = db.ExecuteScalar<int>("SELECT COUNT(*) FROM ? LIMIT 0, 1", table);

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

        public static void CreateTable<T>(string table)
        {
            var _table = db.GetTableInfo(table);

            if (!_table.Any())
            {
                db.CreateTable<T>();
            }
        }

        public static void InsertBulk<T>(List<T> models)
        {
            try
            {
                db.InsertAll(models.ToArray());
            } catch (System.Net.WebException ex)
            {
                throw ex;
            }
        }
    }
}
