using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBLayer.POCO
{
    public class UserSearch
    {
        public int UserCurrentLocation { get; set; }
        public int UserSearchLocation { get; set; }
        public string UserLocationDetails { get; set; } //Name
        public string UserSearchLocationDetails { get; set; } //Name
        public DateTime SearchReachTime { get; set; } //Time to Travel
        public Object ClinicsList { get; set; } //Names,ID
    }
}