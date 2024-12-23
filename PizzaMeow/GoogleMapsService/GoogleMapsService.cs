using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices.JavaScript;

namespace PizzaMeow.GoogleMaps
{
    public class GoogleMapsService
    {
        private string apiKey = "AIzaSyAvaAUw5jaYsmIf-nIDK9TvvyGX-CwTGPY";

        public async Task<(double, double)> GetCoordinatesAsync(string street) 
        {
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={street}&key={apiKey}";
            ;
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var jsonResponse = JObject.Parse(response);

                var results = jsonResponse["results"];

                if (results.HasValues)
                {
                    var location = results[0]["geometry"]["location"];
                    double latitude = location["lat"].Value<double>();
                    double longitude = location["lng"].Value<double>();

                    return (latitude, longitude);
                }

                else throw new Exception("Coordinates is null or empty");
            }
        }
    }
}
