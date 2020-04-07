using Cteleport.Interfaces;
using Cteleport.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cteleport.Services
{
    public class GeoService : IGeoService
    {
        private AppSettings appSettings;
        public GeoService(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        public async Task<ResponseResult<DistanceResponse>> CalculateDistance(DistanceRequest request)
        {
            var response = new ResponseResult<DistanceResponse>();

            try
            {
                var task1 = GetAirportDetails(request.city_iata_destination);
                var task2 =  GetAirportDetails(request.city_iata_departure);

                await Task.WhenAll(task1, task2);

                var airportDestinationResult = task1.Result;
                var airportDepartureResult = task2.Result;

                //3rd party package requires System.Device.Location (not sure if its fully available in .net core 2.1)

                //GeoCoordinate location1 = new GeoCoordinate(airportDestinationResult.location.lat, airportDestinationResult.location.lon);
                //GeoCoordinate location2 = new GeoCoordinate(airportDepartureResult.location.lat, airportDepartureResult.location.lon);
                //double distanceBetween = location1.GetDistanceTo(location2);
                //var distanceMiles = (distanceBetween / 1000) * 0.621371192;
                //response.data = new DistanceResponse() { calculated_distance = distanceMiles };

                //normal calculation (from stackoverflow)
                double distanceBetween = CalculateDistanceBetweenPoints(airportDestinationResult.location.lat, airportDestinationResult.location.lon, airportDepartureResult.location.lat, airportDepartureResult.location.lon);
                var distanceMiles = (distanceBetween) * 0.621371192;
                response.data = new DistanceResponse() { calculated_distance = distanceMiles };

            }
            catch (HttpRequestException ex)
            {
                response.Error(ErrorCodes.ConnectionError, "External API error");
            }
            catch (Exception ex)
            {
                response.Error(ErrorCodes.Unknown, "Unknown error");
            }
           

            return response;


        }


        private async Task<Airports> GetAirportDetails(string iataCode)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(appSettings.CTeleportAPIEndpoint + "airports/" + iataCode);
                    if (response != null && response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Airports>(jsonString);
                    }
                    else
                    {
                        throw new HttpRequestException();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private double CalculateDistanceBetweenPoints(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6376.5000; //Km
            lat1 = ToRad(lat1);
            lat2 = ToRad(lat2);
            lon1 = ToRad(lon1);
            lon2 = ToRad(lon2);
            var dLat = lat2 - lat1;
            var dLon = lon2 - lon1;
            var a = Math.Pow(Math.Sin(dLat / 2), 2) + (Math.Pow(Math.Sin(dLon / 2), 2) * Math.Cos(lat1) * Math.Cos(lat2));
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = R * c;
            return distance;

        }

        private double ToRad(double degs)
        {
            return degs * (Math.PI / 180.0);
        }


    }
}
