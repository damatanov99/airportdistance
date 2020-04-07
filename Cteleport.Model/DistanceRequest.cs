using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cteleport.Model
{
    public class DistanceRequest
    {
        [Required(ErrorMessage = "city_iata_departure is required")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Invalid code")]
        public string city_iata_departure { get; set; }
        [Required(ErrorMessage = "city_iata_destination is required")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Invalid code")]
        public string city_iata_destination { get; set; }
    }
}
