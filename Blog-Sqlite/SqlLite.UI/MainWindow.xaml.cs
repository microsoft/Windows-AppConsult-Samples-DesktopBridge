using Newtonsoft.Json.Linq;
using SqlLite.UI.Model;
using System;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Media.Imaging;
using SqlLite.UI.Data;

namespace SqlLite.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string defaultCity = "Berlin";
        private const string openWeatherApiKey = "";
        private const string openWeatherApiParam = "APPID=";
        private const string baseUri = "https://api.openweathermap.org/data/2.5";
        private const string weatherEndpoint = "weather";
        private const string iconUri = "http://openweathermap.org/img/w/";
        private const string icontype = "png";

        private const string ValidCod = "200";
        private const string COD = "cod";
        private const string WeatherSelector = "weather";
        private const string MainSelector = "main";
        private const string NameSelector = "name";

        private DataAccess data;
        private Weather weather;

        public MainWindow()
        {
            InitializeComponent();

            TbCity.Text = defaultCity;
            if (string.IsNullOrWhiteSpace(openWeatherApiKey))
            {
                TbResult.Text = "No OpenWeatherApi key set. Go to https://openweathermap.org/appid to get one.";
            }
        }

        private async void BtnGetWeather_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(openWeatherApiKey)) return;

            var result = string.Empty;
            var city = (string.IsNullOrWhiteSpace(TbCity.Text)) ? defaultCity : TbCity.Text.Trim();
            var uri = $"{baseUri}/{weatherEndpoint}?q={city}&{openWeatherApiParam}{openWeatherApiKey}";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var jsonData = JObject.Parse(json);
                    if (jsonData.SelectToken(COD).ToString() == ValidCod)
                    {
                        var name = jsonData.SelectToken(NameSelector).ToString();
                        var weathers = jsonData.SelectToken(WeatherSelector).Select(w => new Weather(w)).ToList();
                        var main = new Main(jsonData.SelectToken(MainSelector));

                        if (weathers.Any())
                        {
                            this.weather = weathers.FirstOrDefault();
                            IMGWeatherIcon.Source = new BitmapImage(new Uri($"{iconUri}{weather.Icon}.{icontype}"));
                        }

                        result = $"Weather for {name}:{Environment.NewLine}{weathers.FirstOrDefault()?.Description} @ {main.Temperature.CelsiusCurrent}°C";
                    }
                }
                else
                {
                    result = response.ReasonPhrase;
                }
            }

            TbResult.Text = result;
        }

        private async void BtnSaveToDb_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.weather == null) return;

            if (data == null)
            {
                this.data = new DataAccess("weather");
                await this.data.Init();
            }
            var result = await this.data.InsertWeather(this.weather);
        }
    }
}
