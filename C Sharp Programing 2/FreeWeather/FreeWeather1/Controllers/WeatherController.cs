using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreeWeather1.Models;
using System.Net.Http;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeWeather1.Controllers
{
    public class WeatherController : Controller
    {

        static HttpClient client = new HttpClient();

        string apiKey = "af3beccb1675daada2e5840b10fffdf6";


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Zip data)
        {
            int zipcode = data.zipCode;

            string response = GetWeather(zipcode.ToString()).GetAwaiter().GetResult();

            // Deserialize the results.
            OpenWeatherMap openWeatherMap = JsonConvert.DeserializeObject<OpenWeatherMap>(response);

            /* Question for Class - Why Does the Deserialization not have a problem when we cut out various
             * properties of the classes created by the json?
             * 
             * need more details on how this foreach loop below here is working. What is it doing? 
             * 
             * openWeatherMap.weather is an array - its going through the array... of strings. ah I got it.
             * 
             * but still - DesearlizeObject is doing the heavy lifting here. It is what is taking the Json - because
             * jsons are always formated in the same manner - and taking the values and putting them in the proper class
             * properties...
             * 
             * it just has a if property !exist clause, don't bother with that value and move on?
             */
            

            string weatherDescription = "";
            foreach (var weatherItem in openWeatherMap.weather)
            {
                if (!string.IsNullOrEmpty(weatherDescription))
                    weatherDescription += " and ";

                weatherDescription += weatherItem.weatherDescription;
            }

            var viewModel = new WeatherData
            {
                City = openWeatherMap.cityName,
                zipCode = zipcode,
                Desc = weatherDescription,
                Temp = openWeatherMap.main.temp.ToString()

                
            };

            return View("LocalWeather", viewModel);
        }

        public async Task<string> GetWeather(string zipCode)
        {
            var requestParameters = new Dictionary<string, string>();
            requestParameters.Add("zip", zipCode);
            string uri = GetUri(requestParameters);

            var response = await client.GetAsync(uri);

            string returnValue = "";
            if (response.IsSuccessStatusCode)
            {
                returnValue = await response.Content.ReadAsStringAsync();
            }

            return returnValue;
        }

        public string GetUri(Dictionary<string, string> requestParameters)
        {
            requestParameters.Add("units", "imperial");
            requestParameters.Add("APPID", apiKey);

            var builder = new StringBuilder("http://api.openweathermap.org/data/2.5/weather");
            bool firstParameter = true;


            /* Ok. This is the IMPORTANT loop - this is the loop thats reading the JSON into the dictionary 
             * dataType. whats it doing?
             * 
             * well, we have the First Parameter = true for the first run through. 
             * Which we append to the string builder as True ... what does the ? "?" : "&" mean?
             * 
             * then it adds the first part of the json (the name) to the Key of the dictionary (first string) sets an
             * = sign, then the seceond part of the Json - in this case after the : (is that what the above is doing?)
             * as the value.
             * 
             * so we end up with a long string of Key = Value ...
             * 
             * oh? does ? "?" : "&" mean replace ? with ? and : with &? so the string would build (and because
             * jsons are all formated the same way, and this is a deserializer built to interpret jsons) the string would
             * end up Key=Value&Key=Value ect.... with any ? remaining question marks (so unknown data remains unknown data?)
             * 
             * 
             * 
             * no... none of that is right....  because requestParemeters at this point is just a dictionary with
             * zip, #####
             * units, imperial
             * APPID, ###ourKey###
             * 
             * so this is using the dictionary to build the URI... AH Thats what First Paremeter is for then!
             * 
             * it sets the ? as the first thing built onto the string after the api.blah/blah/weather
             * 
             * because it was true... so Builder((Boolean) ? "?" : "&") is a very complicated and interesting function
             * overload that takes a boolean, and sets the first value (?) if true and the second (after the ':'?) if false!
             * 
             * and for a dictionary, remember Key is the first object in the dictionary, and Value is the second.
             */
            foreach (var parameter in requestParameters)
            {
                builder.Append((firstParameter) ?   "?" : "&");
                builder.Append(parameter.Key);
                builder.Append("=");
                builder.Append(parameter.Value);
                firstParameter = false;
            }

            return builder.ToString();
        }
    }
}
