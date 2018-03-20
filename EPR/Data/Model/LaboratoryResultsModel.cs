using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Data.Model
{
    public class LaboratoryResultsModel
    {
        //HN title Fname Lname EpisodeNo DateOfAuth DateOfRec LabNo Department HosCode HosDesc tsCode tsName tcCode tcname unit data flag low high Reference 
        
       // public DateTime? ALGOnsetDate { get; set; }
        public String HN { get; set; }
        public String title { get; set; }
        public String Fname { get; set; }
        public String Lname { get; set; }
        public String EpisodeNo { get; set; }
        public DateTime? DateOfAuth { get; set; }
        public DateTime? DateOfRec { get; set; }
        public String LabNo { get; set; }
        public String Department { get; set; }
        public String HosCode { get; set; }
        public String HosDesc { get; set; }
        public String tsCode { get; set; }
        public String tsName { get; set; }
        public String tcCode { get; set; }
        public String tcname { get; set; }
        public String unit { get; set; }
        public String data { get; set; }
        public String flag { get; set; }
        public String low { get; set; }
        public String high { get; set; }
        public String Reference { get; set; }

        public String DateOfAuthSTR
        {
            get
            {
                if (DateOfAuth != null)
                {
                    return DateOfAuth.Value.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                }

                return "";
            }
        }

    }
}