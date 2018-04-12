using Newtonsoft.Json.Linq;
using SqlLite.UI.Data;
using SQLite;

namespace SqlLite.UI.Model
{
    public class Main
    {
        private const string Temp = "temp";
        private const string TempMin = "temp_min";
        private const string TempMax = "temp_max";
        private const string PressureSelector = "pressure";
        private const string HumiditySelector = "humidity";
        private const string SeaLevelSelector = "sea_level";
        private const string GroundLevel = "grnd_level";

        public int Id { get; set; }
        public int TemperatureId { get; set; }
        public Temperature Temperature { get; set; }
        public double Pressure { get; set; }
        public double Humdity { get; set; }
        public double SeaLevelAtm { get; set; }
        public double GroundLevelAtm { get; set; }

        public Main() { }

        public Main(JToken mainData)
        {
            Temperature = new Temperature(double.Parse(mainData.SelectToken(Temp).ToString()),
                double.Parse(mainData.SelectToken(TempMin).ToString()), double.Parse(mainData.SelectToken(TempMax).ToString()));

            Pressure = double.Parse(mainData.SelectToken(PressureSelector).ToString());
            Humdity = double.Parse(mainData.SelectToken(HumiditySelector).ToString());

            if (mainData.SelectToken(SeaLevelSelector) != null)
                SeaLevelAtm = double.Parse(mainData.SelectToken(SeaLevelSelector).ToString());

            if (mainData.SelectToken(GroundLevel) != null)
                GroundLevelAtm = double.Parse(mainData.SelectToken(GroundLevel).ToString());
        }
    }
}
