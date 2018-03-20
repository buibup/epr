using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Data.Model
{
    public class DoctorNameModel
    {

        public String DoctorCode { get; set; }
        public String DoctorDesc { get; set; }
        public String PAADMRowId { get; set; }
        public String chiefComplaint { get; set; }
        public String chiefComplaintShow { get; set; }
        public String LocationCode { get; set; }
        public String LocationDesc { get; set; }
        public int DoctorRowNumber { get; set; }
        public String RowNumber { get; set; }
        public String CTPCPSMCNo { get; set; }
        public String SubSpec { set; get; }
        public String DoctorDescENG { get; set; }

    }
}