using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Data.Model
{
    public class ObservationModel
    {


        public String paadmid { get; set; }
        public String weight { get; set; }
        public String height { get; set; }
        public String BPDiastolic { get; set; }
        public String BPSystolic { get; set; }
        public String Pulses { get; set; }
        public String Respiration { get; set; }
        public String Temperature { get; set; }
        public String OxygenSaturation { get; set; }
        public String BPMeans { get; set; }
        public String Bloo { get; set; }
        public String PS { get; set; }
        public String BMI { get; set; }
        public String Head { get; set; }
        public String chest { get; set; }
        public String FallRisk { get; set; }

        public bool HxDrug { get; set; }
        public bool HxDrugY { get; set; }
        public bool HxFood { get; set; }
        public bool HxFoodY { get; set; }
        public String HxOther { get; set; }
        public String HxOtherDesc { get; set; }
        public String HxFoodDesc { get; set; }

        public String PAINLOC { get; set; }
        public String CHARAC { get; set; }
        public String AssessTol { get; set; }
        public String Duration { get; set; }

        public String StatusArrival { set; get; }
        public String AreaofRisk {set; get;}
        public String Triage { set; get; }
        








    }
}