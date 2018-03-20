using System;
using System.Collections.Generic;
using System.Globalization;
using App.Common;

namespace App.Data.Model
{
    public class EpisodeModel
    {
        // ส่ง EPISODE_Rowid เพื่อแสดงรายละเอียดดังนี้
        public String PAADMRowId { get; set; }
        public String PAPMIRowId { get; set; }
        public String LocAdmDesc { get; set; }
        public String admCpName { get; set; }
        public String EpisodeNo { get; set; }
        public DateTime? admDate { get; set; }
        public String admTime { get; set; }
        public String PAPMINo { get; set; }
        public String PAPMIName3 { get; set; }
        public String PAPMIName { get; set; }
        public String PAPMIName2 { get; set; }
        public String AgeY { get; set; }
        public String AgeM { get; set; }
        public String SexCode { get; set; }
        public String wardCode { get; set; }
        public String wardDesc { get; set; }
        public String roomCode { get; set; }
        public String roomDesc { get; set; }
        public String bedCode { get; set; }
        public String BedTypeCode { get; set; }
        public String bedTypeDesc { get; set; }
        public String ward { get; set; }
        public DateTime? DOB { get; set; }
        public String chkCC { get; set; }
        public String chkCN { get; set; }
        public String chkTM { get; set; }
        public String chkNN { get; set; }
        public String chkCons { get; set; }
        public String chkObs { get; set; }
        public String chkPhyExam { get; set; }
        public String chkDiag { get; set; }
        public String chkLabRsl { get; set; }
        public String chkXrayRsl { get; set; }
        public String chiefComplaint { get; set; }
        public String CTPCP_Code { get; set; }
        public String CTPCP_DescM { get; set; }
        public DateTime? PAADM_FinDischgDate { set; get; }
        public String PAADM_FinDischgTime { get; set; }
        //public List<AppointModel> AppointmentList { set; get; } 
        //public String CTPCP_RowId { get; set; }
        //Episode data list
        public List<AllergiesModel> AllergiesModelList { get; set; }
        public List<ClinicalNotesModel> ClinicalNotesModelList { get; set; }
        public List<ClinicalNotesModel> ClinicalNotesModelNewList { get; set; }
        public List<ConsultationOrderModel> ConsultationOrderModelList { get; set; }
        public List<DiagnosisModel> DiagnosisModelList { get; set; }
        public List<DiagnosisModel> DiagnosisModelOtherList { get; set; }
        public List<ObservationModel> ObservationAllergyModelList { get; set; }
        public List<ObservationModel> ObservationModelList { get; set; }
        public List<PhyExamModel> PhyExamModelList { get; set; }
        public List<ProceduresModel> ProceduresModelList { get; set; }
        public List<RadiologyResultsModel> RadiologyResultsModelList { get; set; }
        public List<LaboratoryResultsModel> LaboratoryResultsModelList { get; set; }
        public List<OrderCategoryModel> OrderCategoryModelList { get; set; }
        public List<MedicationModel> MedicalModelList { get; set; }
        public List<MedicationModel> MedicalModelOtherList { get; set; }
        public List<MedicationModel> MedicalModelDoctorList { get; set; }
        public List<DoctorChiefComPlainModel> ChiefComplaintList { get; set; }
        public List<CareProvModel> CareProveList { get; set; }
        public List<DoctorChiefComPlainModel> DoctorChiefComPainList { get; set; }
        public List<DoctorChiefComPlainModel> DoctorChiefComPainNurseList { get; set; }
        public List<DoctorNameModel> NotDoctorList { get; set; }
        public List<DoctorNameModel> NotDoctorOtherList { get; set; }
        public List<DoctorChiefComPlainModel> DoctorChiefComPainAllList { get; set; }
        public List<DoctorChiefComPlainModel> DoctorChiefComPainDocList { get; set; }
        public List<DoctorNameModel> DoctorNameModelList { get; set; }
        public List<NursingInterventionModel> NursingInterventionList { get; set; }
        public List<DoctorNameModel> DoctorNameModelListLast { get; set; }
        public List<DoctorNameModel> LocationList { get; set; }
        public List<DoctorNameModel> NotDoctorLocationList { get; set; }
      //  public List<SocHistModel> SocHistModelList { get; set; }
      //  public List<PAFamilyModel> PAFamilyModelList { get; set; }
        //Flag for javascript to update record on ui
        public bool loaded { get; set; }
        public bool marked { get; set; }
        public int ERowid { get; set; }
        public bool showPhyList { get; set; }

        public String EpiDateString
        {
            get
            {
                if (admDate != null)
                {
                    return admDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                return "";
            }
        }

        public String PAADM_DischgDateString
        {
            get
            {
                if (PAADM_FinDischgDate != null)
                {
                    return PAADM_FinDischgDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                return "";
            }
        }


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


        public String EpiAgeYear
        {
            get
            {
                if (admDate != null && DOB != null)
                {
                    DateTime compareTo = DOB.Value;
                    DateTime now = admDate.Value;
                    var dateSpan = DateTimeSpan.CompareDates(compareTo, now);
                    return dateSpan.Years.ToString();
                }
                return "";
            }
        }

        public String EpiAgeMonth
        {
            get
            {
                if (admDate != null && DOB != null)
                {
                    DateTime compareTo = DOB.Value;
                    DateTime now = admDate.Value;
                    var dateSpan = DateTimeSpan.CompareDates(compareTo, now);
                    return dateSpan.Months.ToString();
                }
                return "";
            }
        }



        public String Episplit
        {
            get 
            {
                if (EpisodeNo != null)
                {
                    String epi = EpisodeNo.Substring(0, 1);
                    return epi.ToString();
                }
                return "";
            }
        }
      

    }
}