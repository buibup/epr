using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Data.Model
{
    public class PatientModel
    {
        public int PAPMI_RowId { get; set; }
        public String PAPMI_No { get; set; }
        public String Title { get; set; }
        public String PAPMI_Name { get; set; }
        public String PAPMI_Name2 { get; set; }
        public String PAPER_TelO { get; set; }
        public String PAPER_TelH { get; set; }
        public String FullName { get; set; }
        public String MiddleName { get; set; }
        public String Age { get; set; }
        public String Gender { get; set; }
        public String BirthDay { get; set; }
        public String ageM { get; set; }
        public String ageD { get; set; }
        public DateTime? DOB { get; set; }
        public String Address { set; get; }
        public String CITAREADesc { set; get; }
        public String CTCITDesc { set; get; }
        public String PROVDesc { set; get; }
        public String CTZIPCode { set; get; }
        public String DoctorCode { get; set; }
        public String SSDesc { set; get; }
        public String PAPERID { set; get; }
        public List<SocHistModel> SocHistModelList { get; set; }
        public List<PAFamilyModel> PAFamilyModelList { get; set; }
        public List<CurrentConditionModel> CurrentConditionModelList { get; set; }
        public List<HistoryMedModel> HistoryMedModelList { get; set; }
        public List<DocScanModel> DocScanModelList { get; set; }

        public String DOBDateString
        {
            get
            {
                if (DOB != null)
                {
                    return DOB.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                return "";
            }
        }

        public List<EpisodeModel> episodeList { get; set; }
        //public List<DoctorChiefComPlainModel> DoctorList { get; set; }

        public List<QuippeModel> QuippeModelList { get; set; }

        public String PAPMI_PrefLanguage_DR { get; set; }

    }
}