using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Data.Model
{
    public class RadiologyResultsModel
    {
        //ItemDesc SttDate DateExec ReportedByCode ReportedBy RadioStatus OrderStatus UserVerifiedCode UserVerified doctorExecutedCode DoctorExecuted HtmlPlanText 
        public String ItemDesc { get; set; }
        public DateTime? SttDate { get; set; }
        public DateTime? DateExec { get; set; }
        public String ReportedByCode { get; set; }
        public String ReportedBy { get; set; }
        public String RadioStatus { get; set; }
        public String OrderStatus { get; set; }
        public String UserVerifiedCode { get; set; }
        public String UserVerified { get; set; }
        public String doctorExecutedCode { get; set; }
        public String DoctorExecuted { get; set; }
        public String HtmlPlanText { get; set; }
        public String ItemCode { get; set; }


        public String SttDateSTR
        {
            get
            {
                if (SttDate != null)
                {
                    return SttDate.Value.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                }

                return "";
            }
        }

        
        public String DateExecSTR
        {
            get
            {
                if (DateExec != null)
                {
                    return DateExec.Value.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                }

                return "";
            }
        }


    }
}