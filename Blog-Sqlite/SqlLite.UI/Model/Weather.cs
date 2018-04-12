using Newtonsoft.Json.Linq;
using SqlLite.UI.Data;
using SQLite;

namespace SqlLite.UI.Model
{
    public class Weather
    {
        private const string IdSelector = "id";
        private const string MainSelector = "main";
        private const string DescriptionSelector = "description";
        private const string IconSelector = "icon";

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public Weather() { }

        public Weather(JToken weatherData)
        {
            Id = int.Parse(weatherData.SelectToken(IdSelector).ToString());
            Main = weatherData.SelectToken(MainSelector).ToString();
            Description = weatherData.SelectToken(DescriptionSelector).ToString();
            Icon = weatherData.SelectToken(IconSelector).ToString();
        }
    }
}
