using System;
using System.Collections.Generic;

namespace Cteleport.Model
{
    public class RequestError
    {
        public string location { get; set; }
        public string param { get; set; }
        public string value { get; set; }
        public string msg { get; set; }
    }

    public class Location
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Airports
    {
        public string country { get; set; }
        public string city_iata { get; set; }
        public string iata { get; set; }
        public string city { get; set; }
        public string timezone_region_name { get; set; }
        public string country_iata { get; set; }
        public int rating { get; set; }
        public string name { get; set; }
        public Location location { get; set; }
        public string type { get; set; }
        public int hubs { get; set; }
        public List<RequestError> errors { get; set; }
    }
  
}
