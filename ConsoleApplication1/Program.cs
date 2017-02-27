using DBLayer;
using DBLayer.POCO;
using LiteDB;
using System;
using System.Collections.Generic;

namespace ConsoleApplication1 {
    class Program {
        static void Main(string[] args) {

            using (var db = new LiteDatabase(DB.dbString)) {
                var col = db.GetCollection<Clinic>("Clinic");

                //Create dummy data
                var clinic = new Clinic {
                    Id = "one",
                    ClinicName = "Regional Hospital",
                    ClinicContactInfo = "asd",
                    ClinicTimings = "From 8:00 AM to 9:00PM",
                    ClinicWebSiteURL = "www.abc.com"
                };

                clinic.DoctorVisitingClinics = new List<Doctor>();
                clinic.DoctorVisitingClinics.Add(new Doctor() { Id = 1, AppointmentTimings = "sdsd", Name = "Dr.Richard" });

                var newId = col.Insert(clinic);

                var results = col.Find(x => x.Id.Equals("one"));

                Console.WriteLine(results);
                Console.ReadLine();
            }
            //col.Update();
        }
    }
}
;
