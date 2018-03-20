using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Data.Model
{
    public class SQCModel
    {
        public PatientModel PatientDetail { set; get; }
        public List<LaboratoryResultsModel> LabList { set; get; }
        public List<RadiologyResultsModel> RadList { set; get; }
        public List<DiagnosisModel> DiagList { set; get; }
        public List<vaccineModel> VaccineList { set; get; }
        public List<OrderInterest> OrderInterest { set; get; }
        
    }
}