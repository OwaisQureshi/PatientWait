using System;
using System.Collections.Generic;

namespace Webmap.DBLayer.POCO {

    //Represents a single clinic information
    public class Clinic {
        public string ClinicID { get; set; }
        public string ClinicName { get; set; }
        public string ClinicTimings { get; set; }
        public Object ClinicContactInfo { get; set; } //Email,Telephone 
        public string ClinicWebSiteURL { get; set; }
        public List<Doctor> DoctorVisitingClinics { get; set; } 
    }
}