using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Data.Model
{
    public class DiagnosisModel
    {
        //ICD10Date ICD10 ICD10Desc icd10TypDesc ICD10DocCode ICD10DocName MRDIADesc mrdiaDoc inxCP 
        
        public DateTime? ICD10Date { get; set; }
        public String ICD10 { get; set; }
        public String ICD10Desc { get; set; }
        public String icd10TypDesc { get; set; }
        public String ICD10DocCode { get; set; }
        public String ICD10DocName { get; set; }
        public String MRDIADesc { get; set; }
        public String mrdiaDoc { get; set; }
        public String inxCP { get; set; }
        public String LocationCode { get; set; }
        public String LocationDesc { get; set; }
        public String CTPCPSMCNo { get; set; }
        public String SubSpec { get; set; }
        public String ICD10DateSTR
        {
            get
            {
                if (ICD10Date != null)
                {
                    return ICD10Date.Value.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                }

                return "";
            }
        }
      

    }
}