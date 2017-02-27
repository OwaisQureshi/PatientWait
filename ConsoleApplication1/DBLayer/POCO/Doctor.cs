using LiteDB;
using System;

namespace DBLayer.POCO {
    //Represents a single clinic information
    public class Doctor {
        [BsonId]
        public int Id { get; set; }
        public string AppointmentTimings { get; set; }
        public string WorkingTimings { get; set; }
        public string Name { get; set; }
        public Object ContactInfo { get; set; } //Email,Telephone
        public string UserRatings { get; set; }
    }
}