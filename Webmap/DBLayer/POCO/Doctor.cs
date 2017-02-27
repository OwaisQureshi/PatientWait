using System;

namespace Webmap.DBLayer.POCO {

    //Represents a single clinic information
    public class Doctor {
        public DateTime DoctorAppointmentTimings { get; set; }
        public DateTime DoctorWorkingTimings { get; set; }
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public Object DoctorContactInfo { get; set; } //Email,Telephone
        public string UserBasedRatings { get; set; }
    }
}