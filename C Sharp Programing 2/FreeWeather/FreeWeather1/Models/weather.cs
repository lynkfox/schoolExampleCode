using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace FreeWeather1.Models
{
    public class WeatherData : Zip
    {
        [Display(Name = "City:")]
        public string City { get; set; }
        [Display(Name = "Temperature (F): ")]
        public string Temp { get; set; }
        [Display(Name = "Current Weather Description:")]
        public string Desc { get; set; }
    }
    public class OpenWeatherMap 
    {
        [HiddenInput(DisplayValue = false)]
        public Weather[] weather { get; set; }
        [HiddenInput(DisplayValue = false)]
        public Main main { get; set; }
        
        [JsonProperty("name")]
        public string cityName { get; set; }
    }


    public class Main
    {
        
        public float temp { get; set; }
    }

    public class Weather
    {

        [JsonProperty("description")]
        public string weatherDescription { get; set; }
    }

}
