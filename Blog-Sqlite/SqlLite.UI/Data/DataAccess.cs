using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using SqlLite.UI.Model;
using SQLite;

namespace SqlLite.UI.Data
{
    internal class DataAccess
    {
        private SQLiteAsyncConnection db;

        internal DataAccess(string dbName)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"{dbName}.db");
            db = new SQLiteAsyncConnection(databasePath);
        }

        internal async Task Init()
        {
            if (db != null)
            {
                await db.CreateTableAsync<Weather>();
            }
        }

        internal async Task<int> InsertWeather(Weather w)
        {
            var result = 0;
            try
            {
                result = await db.InsertAsync(w);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"An error occurred: {e.Message}");
            }

            return result;
        }
    }
}
