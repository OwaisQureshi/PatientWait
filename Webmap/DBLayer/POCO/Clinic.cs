using System;

namespace Webmap.DBLayer.POCO {

    //Represents a single clinic information
    public class Clinic {
        public DateTime Timings { get; set; }
        public Object ContactInfo { get; set; }
        public string URL { get; set; }
    }
}