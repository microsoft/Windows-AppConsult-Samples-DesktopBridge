using System;
using SqlLite.UI.Data;
using SQLite;

namespace SqlLite.UI.Model
{
    public class Temperature
    {
        private double currentTemperatureInKelvin, minimumTemperatureInKelvin, maximumTemperatureInKelvin;
        
        public int Id { get; set; }
        
        public double CelsiusCurrent { get; private set; }
        public double FahrenheitCurrent { get; private set; }
        public double KelvinCurrent
        {
            get => currentTemperatureInKelvin;
            set
            {
                currentTemperatureInKelvin = value;
                CelsiusCurrent = ToCelsius(value);
                FahrenheitCurrent = ToFahrenheit(CelsiusCurrent);
            }
        }
        public double CelsiusMinimum { get; private set; }
        public double CelsiusMaximum { get; private set; }
        public double FahrenheitMinimum { get; private set; }
        public double FahrenheitMaximum { get; private set; }
        public double KelvinMinimum
        {
            get => minimumTemperatureInKelvin;
            set
            {
                minimumTemperatureInKelvin = value;
                CelsiusMinimum = ToCelsius(value);
                FahrenheitMinimum = ToFahrenheit(CelsiusMinimum);
            }
        }
        public double KelvinMaximum
        {
            get => maximumTemperatureInKelvin;
            set
            {
                maximumTemperatureInKelvin = value;
                CelsiusMaximum = ToCelsius(value);
                FahrenheitMaximum = ToFahrenheit(CelsiusMinimum);
            }
        }

        public Temperature() { }

        public Temperature(double temp, double min, double max)
        {
            KelvinCurrent = temp;
            KelvinMinimum = min;
            KelvinMaximum = max;
        }

        private double ToFahrenheit(double celsius)
        {
            return Math.Round(((9.0 / 5.0) * celsius) + 32, 3);
        }

        private double ToCelsius(double kelvin)
        {
            return Math.Round(kelvin - 273.15, 3);
        }
    }
}
