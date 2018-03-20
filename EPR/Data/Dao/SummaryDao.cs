using System;
using System.Collections.Generic;
using App.Data.Model;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using App.Common;
using InterSystems.Data.CacheClient;
using InterSystems.Data.CacheTypes;
using InterSystems.Data.CacheClient.ObjBind;
using System.Dynamic;

namespace App.Data.Dao
{
    public class SummaryDao : BaseDao
    {
       
        public List<ClinicalNotesModel> ClinicalNotes(string EpiRowid)
        {
            
            List<ClinicalNotesModel> ClinicalNotesList = new List<ClinicalNotesModel>();
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            
            
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHRptEprClinicalNotes_getDiag\".('" +EpiRowid + "')}";
                //DataTable dt = new DataTable();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    // dt.Load(command.ExecuteReader());

                    using (CacheDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            ClinicalNotesModel model = new ClinicalNotesModel();
                            model.NOTDate               = GetDateTime(reader, 0); 
                            model.NOTTime               = GetString(reader, 1); 
                            model.NOTNurseIdDesc        = GetString(reader, 2); 
                            model.NOTEnterLocCode       = GetString(reader, 3); 
                            model.NOTEnterLocDesc       = GetString(reader, 4); 
                            model.NOTClinNoteTypeCode   = GetString(reader, 5); 
                            model.NOTClinNoteTypeDesc   = GetString(reader, 6); 
                            model.NotesHtmlPlainText    = GetString(reader, 7);
                            model.NOTEditCPDesc         = GetString(reader, 8);
                            model.ReasonError           = GetString(reader, 9);
                            model.notNurseId            = GetString(reader, 10);
                            model.inxCP                 = GetString(reader, 11);
                            model.notNurseIdCode = GetString(reader, 12);
                            model.notNurseSMCNo = GetString(reader, 13);
                            model.notNurseSubSpec = GetString(reader, 14);
                            ClinicalNotesList.Add(model);
                        }

                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

            return ClinicalNotesList;
        }
        public List<ClinicalNotesModel> ClinicalNotesNew(string EpiRowid)
        {

            List<ClinicalNotesModel> ClinicalNotesList = new List<ClinicalNotesModel>();
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHRptEprClinicalNotes_getDiag\".('" + EpiRowid + "')}";
                //DataTable dt = new DataTable();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    // dt.Load(command.ExecuteReader());

                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        string ChiefComplaint = "";
                        string IllnessNote = "";
                        while (reader.Read())
                        {
                            string chief = "";
                            string Illness = "";

                          if( GetString(reader, 6) =="Chief Complaint"){
                              chief = GetString(reader, 7);
                          }


                          if (GetString(reader, 5).Trim()== "PN")
                          {
                              Illness = GetString(reader, 7);
                          }
                          

                          if (ChiefComplaint.Trim() != "") {
                              if (chief.Trim()!="")
                              {
                              ChiefComplaint = ChiefComplaint + " , " + chief; 
                              }
                          }
                          else
                          {
                              if (chief.Trim() != "")
                              { ChiefComplaint = chief; }
                          }

                          if (IllnessNote.Trim() != "") { if (Illness.Trim() != "") { IllnessNote = IllnessNote + " , " + Illness; } } else { if (Illness.Trim() != "") { IllnessNote = Illness; } }
                        }

                        ClinicalNotesModel model = new ClinicalNotesModel();
                        model.NotesHtmlPlainText = ChiefComplaint;
                        model.NotesHtmlPlainTextIllness = IllnessNote;
                        ClinicalNotesList.Add(model);

                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

            return ClinicalNotesList;
        }
        public List<ConsultationOrderModel> ConsultationOrder(String EpiRowid)
        {
            List<ConsultationOrderModel> ConsultationOrderList = new List<ConsultationOrderModel>();
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHRptEprQEPRQEPRConsult1_getDiag\".('" + EpiRowid + "')}";
               
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;

                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ConsultationOrderModel model = new ConsultationOrderModel();
                            model.QUESDate = GetDateTime(reader, 1);
                            model.QUESPAAdmDR = GetString(reader, 0);
                            model.QUESTime = GetString(reader, 2); 
                            model.QNotSpeci = GetString(reader, 3); 
                            model.QSpeci = GetString(reader, 4); 
                            model.QUrgent = GetString(reader, 5); 
                            model.QNotUrgent = GetString(reader, 6); 
                            model.QOPDAppoin = GetString(reader, 7);
                            model.QDate = GetString(reader, 8); 
                            model.QProblem = GetString(reader, 9); 
                            model.QSpecifiedDoctor = GetString(reader, 10); 
                            model.QDepartDesc = GetString(reader, 11); 
                            model.cpCode = GetString(reader, 12);
                            model.cpDesc = GetString(reader, 13);
                            model.userUpdate = GetString(reader, 14);
                            model.QSpecified = GetString(reader, 15); 
                            model.QUrgentCb = GetString(reader,16); 
                            model.QDepartTXT = GetString(reader,17); 

                            ConsultationOrderList.Add(model);
                        }

                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

            return ConsultationOrderList;
        }
        public List<AllergiesModel> Allergies(String Hn)
        {
            List<AllergiesModel> AllergiesList = new List<AllergiesModel>();
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            conn.Open();
            {

                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHRptEprAllergies_getDiag\".('" + Convert.ToInt32(Hn) + "')}";
              
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;

                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        AllergiesModel model;
                        string strDesc = "";
                       string strDescALL = "";
                       while (reader.Read())
                       {
                           model = new AllergiesModel();
                           strDesc = "";

                           model.ALGOnsetDate = GetDateTime(reader, 0);
                           model.ALGSeverityDesc = GetString(reader, 2);
                           model.ALGIngredDesc = GetString(reader, 3);
                           model.ALGPHCDMDesc = GetString(reader, 4);
                           model.ALGPHCGEDesc = GetString(reader, 5);
                           model.ALGAlGrpDesc = GetString(reader, 6);
                           model.ALGDesc = GetString(reader, 7);
                           model.ALGComments = GetString(reader, 8);
                           model.ALGStatus = GetString(reader, 1);
                           if (model.ALGStatus.ToString() != "I")
                           {

                               if (model.ALGAlGrpDesc.Trim().Length > 0)
                               {
                                   if (model.ALGDesc.Trim().Length > 0)
                                   {
                                       if (strDesc.Trim().Length > 0) { strDesc = strDesc + " " + model.ALGAlGrpDesc + " : " + model.ALGDesc; } else { strDesc = model.ALGAlGrpDesc + " : " + model.ALGDesc; }

                                       if (model.ALGSeverityDesc.Trim().Length > 0)
                                       {
                                           strDesc = strDesc + " " + model.ALGSeverityDesc;
                                           if (model.ALGComments.Trim().Length > 0)
                                           {
                                               strDesc = strDesc + ";" + model.ALGComments;
                                           }

                                       }
                                   }
                                   else
                                   {
                                       if (strDesc.Trim().Length > 0) { strDesc = strDesc + " " + model.ALGAlGrpDesc; } else { strDesc = model.ALGAlGrpDesc; }
                                       if (model.ALGSeverityDesc.Trim().Length > 0)
                                       {
                                           strDesc = strDesc + " " + model.ALGSeverityDesc;
                                           if (model.ALGComments.Trim().Length > 0)
                                           {
                                               strDesc = strDesc + ";" + model.ALGComments;
                                           }
                                       }

                                   }

                               } // end  if--------------------------
                               else if (model.ALGIngredDesc.Trim().Length > 0)
                               {
                                   if (model.ALGDesc.Trim().Length > 0)
                                   {
                                       if (strDesc.Trim().Length > 0) { strDesc = strDesc + " " + model.ALGIngredDesc + " : " + model.ALGDesc; } else { strDesc = model.ALGIngredDesc + " : " + model.ALGDesc; }
                                       if (model.ALGSeverityDesc.Trim().Length > 0)
                                       {
                                           strDesc = strDesc + " " + model.ALGSeverityDesc;
                                           if (model.ALGComments.Trim().Length > 0)
                                           {
                                               strDesc = strDesc + ";" + model.ALGComments;
                                           }

                                       }

                                   }
                                   else
                                   {
                                       if (strDesc.Trim().Length > 0) { strDesc = strDesc + " " + model.ALGIngredDesc; } else { strDesc = model.ALGIngredDesc; }
                                       if (model.ALGSeverityDesc.Trim().Length > 0)
                                       {
                                           strDesc = strDesc + " " + model.ALGSeverityDesc;
                                           if (model.ALGComments.Trim().Length > 0)
                                           {
                                               strDesc = strDesc + ";" + model.ALGComments;
                                           }
                                       }

                                   }
                               }
                               else if (model.ALGPHCDMDesc.Trim().Length > 0)
                               {
                                   if (model.ALGDesc.Trim().Length > 0)
                                   {
                                       if (strDesc.Trim().Length > 0) { strDesc = strDesc + " " + model.ALGPHCDMDesc + " : " + model.ALGDesc; } else { strDesc = model.ALGPHCDMDesc + " : " + model.ALGDesc; }
                                       if (model.ALGSeverityDesc.Trim().Length > 0)
                                       {
                                           strDesc = strDesc + " " + model.ALGSeverityDesc;
                                           if (model.ALGComments.Trim().Length > 0)
                                           {
                                               strDesc = strDesc + ";" + model.ALGComments;
                                           }

                                       }

                                   }
                                   else
                                   {
                                       if (strDesc.Trim().Length > 0) { strDesc = strDesc + " " + model.ALGPHCDMDesc; } else { strDesc = model.ALGPHCDMDesc; }
                                       if (model.ALGSeverityDesc.Trim().Length > 0)
                                       {
                                           strDesc = strDesc + " " + model.ALGSeverityDesc;
                                           if (model.ALGComments.Trim().Length > 0)
                                           {
                                               strDesc = strDesc + ";" + model.ALGComments;
                                           }
                                       }

                                   }
                               }
                               else
                               {
                                   if (model.ALGDesc.Trim().Length > 0)
                                   {
                                       if (strDesc.Trim().Length > 0) { strDesc = strDesc + " " + model.ALGPHCGEDesc + " : " + model.ALGDesc; } else { strDesc = model.ALGPHCGEDesc + " : " + model.ALGDesc; }
                                       if (model.ALGSeverityDesc.Trim().Length > 0)
                                       {
                                           strDesc = strDesc + " " + model.ALGSeverityDesc;
                                           if (model.ALGComments.Trim().Length > 0)
                                           {
                                               strDesc = strDesc + ";" + model.ALGComments;
                                           }

                                       }

                                   }
                                   else
                                   {
                                       if (strDesc.Trim().Length > 0) { strDesc = strDesc + " " + model.ALGPHCGEDesc; } else { strDesc = model.ALGPHCGEDesc; }
                                       if (model.ALGSeverityDesc.Trim().Length > 0)
                                       {
                                           strDesc = strDesc + " " + model.ALGSeverityDesc;
                                           if (model.ALGComments.Trim().Length > 0)
                                           {
                                               strDesc = strDesc + ";" + model.ALGComments;
                                           }
                                       }

                                   }
                               }
                               if (strDesc.Trim() != "")
                               {


                                   if (strDescALL.Trim() != "") { strDescALL = strDescALL + "  ,  " + strDesc; } else { strDescALL = strDesc; }




                               }

                           }
                       }

                        if (strDescALL.Trim() != "") {
                            AllergiesModel model2 = new AllergiesModel();
                            model2.ALGDescAll = strDescALL; 
                            AllergiesList.Add(model2); 
                        } 

                       
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

            return AllergiesList;
        }
        public List<ObservationModel> Observation(String EpiRowid)
        {
            List<ObservationModel> ObservationList = new List<ObservationModel>();
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHrptEprObservation_GetData\".('" + EpiRowid + "')}";

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                         

                    using (CacheDataReader reader = command.ExecuteReader())
                    {
            
                        while (reader.Read())
                        {
                            ObservationModel model = new ObservationModel();

                            model.paadmid = GetString(reader, 0);
                            model.weight = GetString(reader, 1);
                            model.height = GetString(reader, 2);
                            model.BPDiastolic = GetString(reader, 3);
                            model.BPSystolic = GetString(reader, 4);
                            model.Pulses = GetString(reader, 5);
                            model.Respiration = GetString(reader, 6);
                            model.Temperature = GetString(reader, 7);
                            model.OxygenSaturation = GetString(reader, 8);
                            model.BPMeans = GetString(reader, 9);
                            model.Bloo = GetString(reader, 10);
                            model.PS = GetString(reader, 11);
                            model.Head = GetString(reader, 12);
                            model.BMI = GetString(reader, 17);
                            model.FallRisk = GetString(reader, 20);
                            model.AssessTol = GetString(reader, 18);
                            model.PAINLOC = GetString(reader, 14);
                            model.CHARAC = GetString(reader, 15);
                            model.Duration = GetString(reader, 22);
                            model.StatusArrival = GetString(reader, 19);
                            model.AreaofRisk = GetString(reader, 32);
                            model.chest = GetString(reader, 33);
                            model.Triage = GetString(reader, 21);

                            

                            if (model.weight.Trim().Length <= 0) { model.weight = "N/A"; }
                            if (model.height.Trim().Length <= 0) { model.height = "N/A"; }
                            if (model.BPDiastolic.Trim().Length <= 0) { model.BPDiastolic = "N/A"; }
                            if (model.BPSystolic.Trim().Length <= 0) { model.BPSystolic = "N/A"; }
                            if (model.Pulses.Trim().Length <= 0) { model.Pulses = "N/A"; }
                            if (model.Respiration.Trim().Length <= 0) { model.Respiration = "N/A"; }
                            if (model.Temperature.Trim().Length <= 0) { model.Temperature = "N/A"; }
                            if (model.OxygenSaturation.Trim().Length <= 0) { model.OxygenSaturation = "N/A"; }
                            if (model.BPMeans.Trim().Length <= 0) { model.BPMeans = "N/A"; }
                            if (model.Bloo.Trim().Length <= 0) { model.Bloo = "N/A"; }
                            if (model.PS.Trim().Length <= 0) { model.PS = "N/A"; }
                            if (model.Head.Trim().Length <= 0) { model.Head = "N/A"; }
                            if (model.BMI.Trim().Length <= 0) { model.BMI = "N/A"; }
                            if (model.FallRisk.Trim().Length <= 0) { model.FallRisk = "N/A"; }
                            if (model.PAINLOC.Trim().Length <= 0) { model.PAINLOC = "N/A"; }
                            if (model.Duration.Trim().Length <= 0) { model.Duration = "N/A"; }
                            if (model.CHARAC.Trim().Length <= 0) { model.CHARAC = "N/A"; }
                            if (model.AssessTol.Trim().Length <= 0) { model.AssessTol = "N/A"; }
                            if (model.Triage.Trim().Length <= 0) { model.Triage = "N/A"; }
                            ObservationList.Add(model);
                           
                        }

                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

            return ObservationList;
        }
        public String  BMI(String EpiRowid)
        {
            //List<ObservationModel> ObservationList = new List<ObservationModel>();
            String bmi ="";
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHrptEprObservation_GetData\".('" + EpiRowid + "')}";

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;


                    using (CacheDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            //ObservationModel model = new ObservationModel();

                            //model.paadmid = GetString(reader, 0);
                            //model.weight = GetString(reader, 1);
                            //model.height = GetString(reader, 2);
                            //model.BPDiastolic = GetString(reader, 3);
                            //model.BPSystolic = GetString(reader, 4);
                            //model.Pulses = GetString(reader, 5);
                            //model.Respiration = GetString(reader, 6);
                            //model.Temperature = GetString(reader, 7);
                            //model.OxygenSaturation = GetString(reader, 8);
                            //model.BPMeans = GetString(reader, 9);
                            //model.Bloo = GetString(reader, 10);
                            //model.PS = GetString(reader, 11);
                            //model.Head = GetString(reader, 12);
                            bmi = GetString(reader, 17);
                            //model.FallRisk = GetString(reader, 20);
                            //model.AssessTol = GetString(reader, 18);
                            //model.PAINLOC = GetString(reader, 14);
                            //model.CHARAC = GetString(reader, 15);
                            //model.Duration = GetString(reader, 22);
                            //model.StatusArrival = GetString(reader, 19);
                            //model.AreaofRisk = GetString(reader, 32);
                            //model.chest = GetString(reader, 33);
                            //model.Triage = GetString(reader, 21);



                            //if (model.weight.Trim().Length <= 0) { model.weight = "N/A"; }
                            //if (model.height.Trim().Length <= 0) { model.height = "N/A"; }
                            //if (model.BPDiastolic.Trim().Length <= 0) { model.BPDiastolic = "N/A"; }
                            //if (model.BPSystolic.Trim().Length <= 0) { model.BPSystolic = "N/A"; }
                            //if (model.Pulses.Trim().Length <= 0) { model.Pulses = "N/A"; }
                            //if (model.Respiration.Trim().Length <= 0) { model.Respiration = "N/A"; }
                            //if (model.Temperature.Trim().Length <= 0) { model.Temperature = "N/A"; }
                            //if (model.OxygenSaturation.Trim().Length <= 0) { model.OxygenSaturation = "N/A"; }
                            //if (model.BPMeans.Trim().Length <= 0) { model.BPMeans = "N/A"; }
                            //if (model.Bloo.Trim().Length <= 0) { model.Bloo = "N/A"; }
                            //if (model.PS.Trim().Length <= 0) { model.PS = "N/A"; }
                            //if (model.Head.Trim().Length <= 0) { model.Head = "N/A"; }
                            //if (bmi.Trim().Length <= 0) { bmi = "N/A"; }
                            //if (model.FallRisk.Trim().Length <= 0) { model.FallRisk = "N/A"; }
                            //if (model.PAINLOC.Trim().Length <= 0) { model.PAINLOC = "N/A"; }
                            //if (model.Duration.Trim().Length <= 0) { model.Duration = "N/A"; }
                            //if (model.CHARAC.Trim().Length <= 0) { model.CHARAC = "N/A"; }
                            //if (model.AssessTol.Trim().Length <= 0) { model.AssessTol = "N/A"; }
                            //if (model.Triage.Trim().Length <= 0) { model.Triage = "N/A"; }
                            

                        }

                        conn.Close();
                        conn.Dispose();
                        
                    }
                }
            }
            return bmi;

        }
        public List<ObservationModel> ObservationAllergy(String EpiRowid)
        {
            List<ObservationModel> ObservationListAllergy = new List<ObservationModel>();
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            int i = 0;
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHrptEprObservationAllEpi_GetData\".('" + EpiRowid + "')}";

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;

                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            i++;
                            ObservationModel model = new ObservationModel();

                            string HxDrug = GetString(reader, 25);
                            string HxFood = GetString(reader, 26);
                            string HxOther = GetString(reader, 27);
                            string HxOtherDesc = GetString(reader, 28);
                            if (HxDrug.Trim().Length > 0)
                            {
                                if (HxDrug.Trim().ToUpper() == "NKA") { model.HxDrug = true; } else { model.HxDrug = false; }
                                if (HxDrug.Trim().ToUpper() == "YES") { model.HxDrugY = true; } else { model.HxDrugY = false; }
                            }
                            else { model.HxDrug = false; model.HxDrugY = false; }

                            if (HxFood.Trim().Length > 0)
                            {
                                if (HxFood.Trim().ToUpper() == "NKA") { model.HxFood = true; } else { model.HxFood = false; }
                                if (HxFood.Trim().ToUpper() == "YES") { model.HxFoodY = true; } else { model.HxFoodY = false; }
                            }
                            else { model.HxFood = false; model.HxFoodY = false; }

                            model.HxOther = HxOther;
                            model.HxOtherDesc = HxOtherDesc;
                            model.HxFoodDesc = GetString(reader, 29);

                            if (i == 1)
                            {
                                ObservationListAllergy.Add(model);
                            }
                            else {
                                return ObservationListAllergy;
                            }
                        }

                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

            return ObservationListAllergy;
        }
        public DataTable GetClinicalExam(String EpiRowid)
        {
            DataTable dt =new DataTable();
             //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHEprGetClinicalNotesPhyExam_getDiag\".('" + EpiRowid + "')}";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {

                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    try
                    {
                        CacheDataAdapter OD = new CacheDataAdapter();
                        OD.SelectCommand = command;
                        OD.Fill(dt);
                        
                    }
                    catch(System.Exception e){

                        conn.Close();
                        conn.Dispose();
                        command.Dispose();
                    }
                   

                }



            }



             return dt;
        }
        public List<PhyExamModel> PhyExam(String EpiRowid)
        {
            List<PhyExamModel> PhyExamList = new List<PhyExamModel>();
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHRptEprPhyExam2_GetData\".('" + EpiRowid + "')}";
               
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;

                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PhyExamModel model = new PhyExamModel();
                           
                            if (reader[0] != null) { model.Q1ID = Convert.ToInt32(reader[0]); } else { model.Q1ID = 0; }
                            model.Q1Text = GetString(reader, 1);
                           
                                        model.Q1EPI = GetString(reader, 2);
                                        model.Q1HN = GetString(reader, 3);
                                        model.userCode = GetString(reader, 4);
                                        model.userName = GetString(reader, 5);
                                        model.DoctorCode = GetString(reader, 9);
                                        model.LocationCode = GetString(reader, 10);


                                        PhyExamList.Add(model);
                            

                           
                        }

                        conn.Close();
                    }
                            DataTable dt1 = new DataTable();
                            int i = 0;
                           dt1 = GetClinicalExam(EpiRowid);
                            if (dt1.Rows.Count > 0)
                            {
                                for (i = 0; i < dt1.Rows.Count; i++)
                                {
                                    PhyExamModel model = new PhyExamModel();
                                    model.Q1ID = 0;
                                    model.Q1Text = dt1.Rows[i]["NotesHtmlPlainText"].ToString();
                                    model.Q1EPI = dt1.Rows[i][EpiRowid].ToString();
                                    model.Q1HN = dt1.Rows[i]["papmiNo"].ToString();
                                    model.userCode = dt1.Rows[i]["notNurseIdCode"].ToString();
                                    model.userName = dt1.Rows[i]["NOTNurseIdDesc"].ToString();
                                    model.DoctorCode = dt1.Rows[i]["notNurseIdCode"].ToString();






                                    PhyExamList.Add(model);
                                }

                            }

                    conn.Close();
                    conn.Dispose();

                }
            }

            return PhyExamList;
        }
        public List<DiagnosisModel> Diagnosis(String EpiRowid)
        {
            List<DiagnosisModel> DiagnosisList = new List<DiagnosisModel>();
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHRptEprDiag_getDiag\".('" + EpiRowid + "')}";

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;

                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DiagnosisModel model = new DiagnosisModel();
                            model.ICD10Date = GetDateTime(reader, 0); 
                            model.ICD10 = GetString(reader, 1);
                            model.ICD10Desc = GetString(reader, 2); 
                            model.icd10TypDesc = GetString(reader, 3);
                            model.ICD10DocCode = GetString(reader, 4);
                            model.ICD10DocName = DoctorName.clean(GetString(reader, 5));
                            model.MRDIADesc = GetString(reader, 6); 
                            model.mrdiaDoc = GetString(reader, 7); 
                            model.inxCP = GetString(reader, 8);
                            model.LocationCode = GetString(reader, 9);
                            model.LocationDesc = GetString(reader, 10);
                            model.CTPCPSMCNo = GetString(reader, 11);
                            model.SubSpec = GetString(reader, 12);
                            DiagnosisList.Add(model);
                        }

                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

            return DiagnosisList;
        }
        public List<ProceduresModel> Procedures(String EpiRowid)
        {
            List<ProceduresModel> ProceduresList = new List<ProceduresModel>();
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHrptEprProcedure_GetData\".('" + EpiRowid + "')}";

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;

                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProceduresModel model = new ProceduresModel();
                            model.ProcDate = GetDateTime(reader, 1); 
                            model.ProcTime = GetString(reader, 5);
                            model.OperCode = GetString(reader, 8);
                            model.OperDesc = GetString(reader, 9); 
                            model.OperCatDesc = GetString(reader, 11); 
                            model.OperCpDesc = GetString(reader, 13); 
                            model.AnaesDesc = GetString(reader, 15); 

                            ProceduresList.Add(model);
                        }

                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

            return ProceduresList;
        }
        public List<LaboratoryResultsModel>LaboratoryResults(String HNno,String Epino)
        {
            List<LaboratoryResultsModel> LaboratoryResultsList = new List<LaboratoryResultsModel>();
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHRptEprLabResult_GetData\".('" + HNno + "','" + Epino + "')}";

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;

                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LaboratoryResultsModel model = new LaboratoryResultsModel();

                            model.HN = GetString(reader, 0); 
                            model.title = GetString(reader, 1); 
                            model.Fname = GetString(reader, 2); 
                            model.Lname = GetString(reader, 3); 
                            model.EpisodeNo = GetString(reader, 4); 
                            model.DateOfAuth = GetDateTime(reader, 5); 

                            model.LabNo = GetString(reader, 7);
                            model.Department = GetString(reader, 8); 
                            model.HosCode = GetString(reader, 9); 
                            model.HosDesc = GetString(reader, 10); 
                            model.tsCode = GetString(reader, 11); 
                            model.tsName = GetString(reader, 12); 
                            model.tcCode = GetString(reader, 13);
                            model.tcname = GetString(reader, 14); 
                            model.unit = GetString(reader, 15); 
                            model.data = GetString(reader, 16);
                            model.flag = GetString(reader, 17);
                            model.low = GetString(reader, 18);
                            model.high = GetString(reader, 19);
                            model.Reference = GetString(reader, 20);
                            if (model.tcname.IndexOf("HIV") != -1) { }
                            else {
                                LaboratoryResultsList.Add(model);
                            }
                        }

                        conn.Close();
                        conn.Dispose();
                        

                    }
                }
            }

            return LaboratoryResultsList;
        }
        public List<vaccineModel> VaccineListResesultes(String HNno) { 
            List<vaccineModel> vaccineList = new List<vaccineModel>();
            vaccineDAO dao = new vaccineDAO();
            vaccineList = dao.getvaccinceModelList(HNno);
            return vaccineList;
        }
        public List<LaboratoryResultsModel> LaboratoryResultsbyHN(String HNno)
        {
            List<LaboratoryResultsModel> LabList = new List<LaboratoryResultsModel>();
            CacheConnection conn0 = new CacheConnection(Cache_DB_Connecttion);
            conn0.Open();
            {
                String sql0 = "select PAADM_RowID,PAADM_PAPMI_DR->PAPMI_No,PAADM_ADMNO,* from pa_adm  where PAADM_PAPMI_DR->PAPMI_No ='" + HNno + "'  and  PAADM_ADMNo <> '' and Year(PAADM_AdmDate) >= (Year(GetDate()) -4)order by  PAADM_AdmDate asc";
                if (conn0.State == ConnectionState.Closed)
                    conn0.Open();
                using (CacheCommand command0 = new CacheCommand())
                {
                    command0.Connection = conn0;
                    command0.CommandType = CommandType.Text;
                    command0.CommandText = sql0;
                    command0.CommandTimeout = 3000;
                    using (CacheDataReader reader = command0.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            string PAMI_No = "";
                            string PAADM_ADMNO = "";
                            PAMI_No = GetString(reader, 1);
                            PAADM_ADMNO = GetString(reader, 2);
                            List<LaboratoryResultsModel> LabResultsList = new List<LaboratoryResultsModel>();
                            LabResultsList = LaboratoryResults(PAMI_No, PAADM_ADMNO);
                            LabList.AddRange(LabResultsList);
                        }

                    }
                    conn0.Close();
                    conn0.Dispose();
                }
            }

            return LabList;
        }
        public List<RadiologyResultsModel>RadiologyResults(String EpiRowid)
        {
            List<RadiologyResultsModel> RadiologyResultsList = new List<RadiologyResultsModel>();
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHRptEprXRayResult_GetData\".('" + EpiRowid + "')}";

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;

                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RadiologyResultsModel model = new RadiologyResultsModel();
                            model.ItemDesc = GetString(reader, 0);
                            model.SttDate = GetDateTime(reader, 1);
                            model.DateExec = GetDateTime(reader, 2); 
                            model.ReportedByCode = GetString(reader,3); 
                            model.ReportedBy = GetString(reader, 4); 
                            model.RadioStatus = GetString(reader, 5); 
                            model.OrderStatus = GetString(reader, 6);
                            model.UserVerifiedCode = GetString(reader, 7); 
                            model.UserVerified = GetString(reader, 8); 
                            model.doctorExecutedCode = GetString(reader, 9); 
                            model.DoctorExecuted = DoctorName.clean(GetString(reader, 10));
                            model.HtmlPlanText = GetString(reader, 11);
                            model.ItemCode = GetString(reader, 12);

                            RadiologyResultsList.Add(model);
                        }

                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

            return RadiologyResultsList;
        }

        public PatientInfo PatientInfobyRow(string HNRowid) {
            PatientInfo PIF = new PatientInfo();
            CacheConnection con = new CacheConnection(Cache_DB_Connecttion);
            con.Open();
            {
                string sql = "select PAPMI_No,Papmi_name,Papmi_name2,datediff('YY',PAPMI_DOB,getdate()) as age,PAPMI_Sex_DR->CTSEX_Code from pa_patmas  where PAPMI_RowId1='"+HNRowid+"'";
                if (con.State == ConnectionState.Closed)
                    con.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    command.Connection = con;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PIF.HN = GetString(reader, 0);
                            PIF.Name = GetString(reader, 1);
                            PIF.Surname = GetString(reader, 2);
                            PIF.Sex = GetString(reader, 3);
                            PIF.Age = GetString(reader, 4);
                        }
                    }
                }
                con.Close();
                con.Dispose();
            }
            return PIF;
        }

        public PatientModel PatientDetail(String HN)
        {
            PatientModel model = new PatientModel();
            //dynamic model = new ExpandoObject();
            
            PatientDao PtDAO = new PatientDao();

            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                string Rowid = PatientDetailByHN(HN);
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHGetHNByRowId_getData\".('" + Convert.ToInt32(Rowid) + "')}";
                //String sql = "@select PAPMI_RowId1,PAPMI_No,PAPMI_Name,PAPMI_Name2,PAPMI_SurName as MiddleName,PAPMI_RowId1->PAPER_TelO,PAPMI_RowId1->PAPER_TelH,{fn CONCAT({fn CONCAT(PAPMI_Name,' ')},PAPMI_Name2)} as FullName,PAPMI_RowId1->PAPER_AgeYr, PAPMI_RowId1->PAPER_Sex_DR->CTSEX_Desc,PAPMI_RowId1->PAPER_AgeMth,PAPMI_RowId1->PAPER_AgeDay,PAPMI_DOB, PAPMI_RowId1->PAPER_StName,PAPMI_RowId1->PAPER_CityCode_DR->CTCIT_Desc,PAPMI_RowId1->PAPER_Zip_DR->CTZIP_Desc,PAPMI_RowId1->PAPER_Country_DR->CTCOU_Desc,PAPMI_RowId1->PAPER_CT_Province_DR->PROV_Desc from pa_patmas where PAPMI_RowId1='" + Convert.ToInt32(HnRowId)+"'";
                DataTable dt = new DataTable();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    // dt.Load(command.ExecuteReader());

                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            model.PAPMI_RowId = GetInteger(reader, 0);
                            model.PAPMI_No = GetString(reader, 1);
                            model.Title = GetString(reader, 2);
                            model.PAPMI_Name = GetString(reader, 3);
                            model.PAPMI_Name2 = GetString(reader, 4);
                            model.MiddleName = GetString(reader, 5);
                            model.PAPER_TelO = GetString(reader, 6);
                            model.PAPER_TelH = GetString(reader, 7);
                            model.FullName = GetString(reader, 8);
                            model.Age = GetString(reader, 9);
                            model.Gender = GetString(reader, 10);
                            model.ageM = GetString(reader, 11);
                            model.ageD = GetString(reader, 12);
                            model.DOB = GetDateTime(reader, 13);
                            model.Address = GetString(reader, 14);
                            model.CITAREADesc = GetString(reader, 19);
                            model.PROVDesc = GetString(reader, 17);
                            model.CTZIPCode = GetString(reader, 20);
                            model.SSDesc = GetString(reader, 21);
                            model.PAPERID = GetString(reader, 22);
                            model.PAFamilyModelList = PtDAO.PAFamilyCachecon(Convert.ToString(Rowid));
                            model.SocHistModelList = PtDAO.SocHistCachecon(Convert.ToString(Rowid));
                            model.CurrentConditionModelList = PtDAO.CurrentConditionCachecon(Convert.ToString(Rowid));
                            model.HistoryMedModelList = PtDAO.HistoryMedModelCachecon(Convert.ToString(Rowid));
                           // model.BMI = "";

                        }
                        conn.Close();
                        conn.Dispose();
                    }

                }
            }

            return model;
        }
        public string PatientDetailByHN(String HN)
        {
            CacheConnection conn0 = new CacheConnection(Cache_DB_Connecttion);
            conn0.Open();
            {
                string PAADM_PAPMI_DR = "";
                string sql = "select distinct PAADM_PAPMI_DR->PAPMI_No,PAADM_PAPMI_DR from pa_adm where PAADM_PAPMI_DR->PAPMI_No='" + HN + "'";
                if (conn0.State == ConnectionState.Closed)
                    conn0.Open();
                using (CacheCommand command0 = new CacheCommand())
                {
                    command0.Connection = conn0;
                    command0.CommandType = CommandType.Text;
                    command0.CommandText = sql;
                    command0.CommandTimeout = 3000;
                    using (CacheDataReader reader = command0.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            
                            PAADM_PAPMI_DR = GetString(reader, 1);
                            
                        }

                    }
                    conn0.Close();
                    conn0.Dispose();
                    return PAADM_PAPMI_DR;
                }
                
            }
        }
        public List <OrderInterest> OrderInterest(String HN)
        {
            List<OrderInterest> OrdInList = new List<OrderInterest>();
            CacheConnection conn0 = new CacheConnection(Cache_DB_Connecttion);
            conn0.Open();
            {
                String sql0 = "select distinct OEORI_ItmMast_DR->ARCIM_Code,OEORI_ItmMast_DR->ARCIM_Desc,OEORI_SttDat  from oe_orditem where OEORI_OEORD_ParRef->OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_No='"+HN+"' and OEORI_ItmMast_DR->ARCIM_Code in ("+ OrderInterestString + ")";
                //String sql0 = "select PAADM_RowID,PAADM_PAPMI_DR->PAPMI_No,PAADM_ADMNO,* from pa_adm  where PAADM_PAPMI_DR->PAPMI_No='" + HNno + "' ";
                if (conn0.State == ConnectionState.Closed)
                    conn0.Open();
                using (CacheCommand command0 = new CacheCommand())
                {
                    command0.Connection = conn0;
                    command0.CommandType = CommandType.Text;
                    command0.CommandText = sql0;
                    command0.CommandTimeout = 3000;
                    
                    using (CacheDataReader reader = command0.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            OrderInterest model = new OrderInterest();
                            model.ARCIM_Code = GetString(reader, 0);
                            model.ARCIM_Desc = GetString(reader, 1);
                            model.OEORI_SttDat = GetDateTime(reader, 2);
                            OrdInList.Add(model);
                        }
                        

                    }
                    conn0.Close();
                    conn0.Dispose();
                    
                }
            }
            return OrdInList;
        }
        public SQCModel SQCResult(String HNno) {
            SQCModel SQC = new SQCModel();
            SQC.PatientDetail = PatientDetail(HNno);
            SQC.DiagList = DiagonosisByHN(HNno);
            SQC.LabList = LaboratoryResultsbyHN(HNno);
            SQC.RadList = RadiologyResultsByHN(HNno);
            SQC.VaccineList = VaccineListResesultes(HNno);
            SQC.OrderInterest = OrderInterest(HNno);
            return SQC;
        }

        public List<RadiologyResultsModel> RadiologyResultsByHN(String HNno)
        {
            List<RadiologyResultsModel> RadioList = new List<RadiologyResultsModel>();
            CacheConnection conn0 = new CacheConnection(Cache_DB_Connecttion);
            conn0.Open();
            {
                String sql0 = "select PAADM_RowID,PAADM_PAPMI_DR->PAPMI_No,PAADM_ADMNO,* from pa_adm  where PAADM_PAPMI_DR->PAPMI_No ='" + HNno + "'  and  PAADM_ADMNo <> '' and Year(PAADM_AdmDate) >= (Year(GetDate()) -4)order by  PAADM_AdmDate asc";
                if (conn0.State == ConnectionState.Closed)
                    conn0.Open();
                using (CacheCommand command0 = new CacheCommand())
                {
                    command0.Connection = conn0;
                    command0.CommandType = CommandType.Text;
                    command0.CommandText = sql0;
                    command0.CommandTimeout = 3000;
                    using (CacheDataReader reader = command0.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            string PAADM_RowID = "";
                            PAADM_RowID = GetString(reader, 0);
                            List<RadiologyResultsModel> RadiologyResultsList = new List<RadiologyResultsModel>();
                            RadiologyResultsList = RadiologyResults(PAADM_RowID);
                            RadioList.AddRange(RadiologyResultsList);
                        }

                    }
                }
                conn0.Close();
                conn0.Dispose();
            }
            
            return RadioList;
        }

        public List<DiagnosisModel> DiagonosisByHN(String HNno) {
            List<DiagnosisModel> DiagList = new List<DiagnosisModel>();
            CacheConnection conn0 = new CacheConnection(Cache_DB_Connecttion);
            conn0.Open();
            {
                String sql0 = "select distinct  PAADM_RowID,PAADM_PAPMI_DR->PAPMI_No,PAADM_ADMNO,PAADM_AdmDate,Year(PAADM_AdmDate) from pa_adm  where PAADM_PAPMI_DR->PAPMI_No ='"+HNno+"'  and  PAADM_ADMNo <> '' and Year(PAADM_AdmDate) >= (Year(GetDate()) -4)order by  PAADM_AdmDate asc";
                //String sql0 = "select PAADM_RowID,PAADM_PAPMI_DR->PAPMI_No,PAADM_ADMNO,* from pa_adm  where PAADM_PAPMI_DR->PAPMI_No='" + HNno + "' ";
                if (conn0.State == ConnectionState.Closed)
                    conn0.Open();
                using (CacheCommand command0 = new CacheCommand())
                {
                    command0.Connection = conn0;
                    command0.CommandType = CommandType.Text;
                    command0.CommandText = sql0;
                    command0.CommandTimeout = 3000;
                    using (CacheDataReader reader = command0.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            string PAADM_RowID = "";
                            PAADM_RowID = GetString(reader, 0);
                            List<DiagnosisModel> DiagResultsList = new List<DiagnosisModel>();
                            DiagResultsList = Diagnosis(PAADM_RowID);
                            DiagList.AddRange(DiagResultsList);
                        }

                    }

                }
                conn0.Close();
                conn0.Dispose();
            }

            return DiagList;
        }


        public DataTable GetMedication(String EpiRowid,String Doctorcode)
        {
            DataTable dt = new DataTable();
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                String sql = @"SELECT distinct ARCIM_DESC,CTUOM_Desc,PHCFR_Desc2,OEORI_PhQtyOrd,CTPCP_Code  FROM VSVH_PHAORD WHERE     paadm_ADMNO = '" + EpiRowid + "' and  CTPCP_Code ='" + Doctorcode  + "'  AND ORCAT_DESC like 'Medicine%'  and  OSTAT_Desc <>'D/C (Discontinued)'  ";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {

                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    try
                    {
                        CacheDataAdapter OD = new CacheDataAdapter();
                        OD.SelectCommand = command;
                        OD.Fill(dt);

                    }
                    catch (System.Exception e)
                    {

                        conn.Close();
                        conn.Dispose();
                        command.Dispose();
                    }


                }



            }



            return dt;
        }
        public List<NursingInterventionModel> GetNursingIntervention(String EpiRowid)
        {
            List<NursingInterventionModel> NursingInterventionList = new List<NursingInterventionModel>();
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                //String sql = @"select QUESPAAdmDR,QUESPAPatMasDR,QUESDate,QUESTime,QUESUserDR,Q01,Q02,Q03,Q04,Q05,Q06,Q07,Q08,Q09,Q10,Q11,Q12,Q13,Q14,Q15,Q16,Q17,Q18,Q19,Q20,Q21,Q22,Q23,Q24,Q25,Q26,Q27,Q28,Q29,Q30,Q31,Q32,Q33,Q34,Q35,Q36,Q37,Q38,Q39,Q40,Q41,Q42,Q43,Q44,Q45,Q46,Q47,Q48,Q49,Q50,Q51,Q52,Q53,Q54,Q55,Q56,Q57,Q58,Q59,Q60,Q61,Q62,Q63,Q64,Q65,Q66,Q67,Q68,Q69,Q70,Q71,Q72,Q73,Q74,Q75,Q76,Q77,Q78,Q79,Q80,Q81,Q82,Q83,Q84,Q85,Q86,Q87,Q88,Q89,Q90,Q91,Q92,Q93,Q94,Q95,Q96,Q97,Q98,Q99,Q100,Q101,Q102,Q103,Q104,Q105,Q106,Q107,Q108,Q109,Q110 from questionnaire.QEPRNURIN2 where QUESPAAdmDR= '" + EpiRowid + "'";
               String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHEprGetNursingInterventionViewer_GetData\".('" + EpiRowid + "')}";
            if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;

                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NursingInterventionModel NursingIntervention = new NursingInterventionModel();
                            DateTime value = new DateTime(1980, 12, 31);
                            //value = value.AddDays(Convert.ToInt32(GetString(reader, 2)));
                            //NursingIntervention.QUESDate = GetString(reader, 0); 
                            NursingIntervention.QUESPAAdmDR = GetString(reader, 0);
                            //NursingIntervention.QUESPAPatMasDR = GetString(reader, 2); //value.ToString("dd/MM/yyyy");
                            //NursingIntervention.QUESTime = GetString(reader, 3);
                            //NursingIntervention.QUESUserDR = GetString(reader, 4);
                            NursingIntervention.Q01Time = GetString(reader, 1);
                            NursingIntervention.Q02Time = GetString(reader, 2);
                            NursingIntervention.Q03Time = GetString(reader, 3);
                            NursingIntervention.Q04Time = GetString(reader, 4);
                            NursingIntervention.Q05Time = GetString(reader, 5);
                            NursingIntervention.Q06Time = GetString(reader, 6);
                            NursingIntervention.Q07Time = GetString(reader, 7);
                            NursingIntervention.Q08Time = GetString(reader, 8);
                            NursingIntervention.Q09Time = GetString(reader, 9);
                            NursingIntervention.Q10Time = GetString(reader, 10);
                            NursingIntervention.Q11Dep = GetString(reader, 11);
                            NursingIntervention.Q12Dep = GetString(reader, 12);
                            NursingIntervention.Q13Dep = GetString(reader, 13);
                            NursingIntervention.Q14Dep = GetString(reader, 14);
                            NursingIntervention.Q15Dep = GetString(reader, 15);
                            NursingIntervention.Q16Dep = GetString(reader, 16);
                            NursingIntervention.Q17Dep = GetString(reader, 17);
                            NursingIntervention.Q18Dep = GetString(reader, 18);
                            NursingIntervention.Q19Dep = GetString(reader, 19);
                            NursingIntervention.Q20Dep = GetString(reader, 20);
                            NursingIntervention.Q21BT = GetString(reader, 21);
                            NursingIntervention.Q22BT = GetString(reader, 22);
                            NursingIntervention.Q23BT = GetString(reader, 23);
                            NursingIntervention.Q24BT = GetString(reader, 24);
                            NursingIntervention.Q25BT = GetString(reader, 25);
                            NursingIntervention.Q26BT = GetString(reader, 26);
                            NursingIntervention.Q27BT = GetString(reader, 27);
                            NursingIntervention.Q28BT = GetString(reader, 28);
                            NursingIntervention.Q29BT = GetString(reader, 29);
                            NursingIntervention.Q30BT = GetString(reader, 30);
                            NursingIntervention.Q31PR = GetString(reader, 31);
                            NursingIntervention.Q32PR = GetString(reader, 32);
                            NursingIntervention.Q33PR = GetString(reader, 33);
                            NursingIntervention.Q34PR = GetString(reader, 34);
                            NursingIntervention.Q35PR = GetString(reader, 35);
                            NursingIntervention.Q36PR = GetString(reader, 36);
                            NursingIntervention.Q37PR = GetString(reader, 37);
                            NursingIntervention.Q38PR = GetString(reader, 38);
                            NursingIntervention.Q39PR = GetString(reader, 39);
                            NursingIntervention.Q40PR = GetString(reader, 40);
                            NursingIntervention.Q41RR = GetString(reader, 41);
                            NursingIntervention.Q42RR = GetString(reader, 42);
                            NursingIntervention.Q43RR = GetString(reader, 43);
                            NursingIntervention.Q44RR = GetString(reader, 44);
                            NursingIntervention.Q45RR = GetString(reader, 45);
                            NursingIntervention.Q46RR = GetString(reader, 46);
                            NursingIntervention.Q47RR = GetString(reader, 47);
                            NursingIntervention.Q48RR = GetString(reader, 48);
                            NursingIntervention.Q49RR = GetString(reader, 49);
                            NursingIntervention.Q50RR = GetString(reader, 50);
                            NursingIntervention.Q51BP1 = GetString(reader, 51);
                            NursingIntervention.Q52BP1 = GetString(reader, 52);
                            NursingIntervention.Q53BP1 = GetString(reader, 53);
                            NursingIntervention.Q54BP1 = GetString(reader, 54);
                            NursingIntervention.Q55BP1 = GetString(reader, 55);
                            NursingIntervention.Q56BP1 = GetString(reader, 56);
                            NursingIntervention.Q57BP1 = GetString(reader, 57);
                            NursingIntervention.Q58BP1 = GetString(reader, 58);
                            NursingIntervention.Q59BP1 = GetString(reader, 59);
                            NursingIntervention.Q60BP1 = GetString(reader, 60);
                            NursingIntervention.Q61BP2 = GetString(reader, 61);
                            NursingIntervention.Q62BP2 = GetString(reader, 62);
                            NursingIntervention.Q63BP2 = GetString(reader, 63);
                            NursingIntervention.Q64BP2 = GetString(reader, 64);
                            NursingIntervention.Q65BP2 = GetString(reader, 65);
                            NursingIntervention.Q66BP2 = GetString(reader, 66);
                            NursingIntervention.Q67BP2 = GetString(reader, 67);
                            NursingIntervention.Q68BP2 = GetString(reader, 68);
                            NursingIntervention.Q69BP2 = GetString(reader, 69);
                            NursingIntervention.Q70BP2 = GetString(reader, 70);
                            NursingIntervention.Q71O2Sat = GetString(reader, 71);
                            NursingIntervention.Q72O2Sat = GetString(reader, 72);
                            NursingIntervention.Q73O2Sat = GetString(reader, 73);
                            NursingIntervention.Q74O2Sat = GetString(reader, 74);
                            NursingIntervention.Q75O2Sat = GetString(reader, 75);
                            NursingIntervention.Q76O2Sat = GetString(reader, 76);
                            NursingIntervention.Q77O2Sat = GetString(reader, 77);
                            NursingIntervention.Q78O2Sat = GetString(reader, 78);
                            NursingIntervention.Q79O2Sat = GetString(reader, 79);
                            NursingIntervention.Q80O2Sat = GetString(reader, 80);
                            NursingIntervention.Q81OrdItm = GetString(reader, 81);
                            NursingIntervention.Q82OrdItm = GetString(reader, 82);
                            NursingIntervention.Q83OrdItm = GetString(reader, 83);
                            NursingIntervention.Q84OrdItm = GetString(reader, 84);
                            NursingIntervention.Q85OrdItm = GetString(reader, 85);
                            NursingIntervention.Q86OrdItm = GetString(reader, 86);
                            NursingIntervention.Q87OrdItm = GetString(reader, 87);
                            NursingIntervention.Q88OrdItm = GetString(reader, 88);
                            NursingIntervention.Q89OrdItm = GetString(reader, 89);
                            NursingIntervention.Q90OrdItm = GetString(reader, 90);
                            NursingIntervention.Q91Outcome = GetString(reader, 91);
                            NursingIntervention.Q92Outcome = GetString(reader, 92);
                            NursingIntervention.Q93Outcome = GetString(reader, 93);
                            NursingIntervention.Q94Outcome = GetString(reader, 94);
                            NursingIntervention.Q95Outcome = GetString(reader, 95);
                            NursingIntervention.Q96Outcome = GetString(reader, 96);
                            NursingIntervention.Q97Outcome = GetString(reader, 97);
                            NursingIntervention.Q98Outcome = GetString(reader, 98);
                            NursingIntervention.Q99Outcome = GetString(reader, 99);
                            NursingIntervention.Q100Outcome = GetString(reader, 100);
                            NursingIntervention.Q101User = GetString(reader, 101);
                            NursingIntervention.Q102User = GetString(reader, 102);
                            NursingIntervention.Q103User = GetString(reader, 103);
                            NursingIntervention.Q104User = GetString(reader, 104);
                            NursingIntervention.Q105User = GetString(reader, 105);
                            NursingIntervention.Q106User = GetString(reader, 106);
                            NursingIntervention.Q107User = GetString(reader, 117);
                            NursingIntervention.Q108User = GetString(reader, 108);
                            NursingIntervention.Q109User = GetString(reader, 109);
                            NursingIntervention.Q110User = GetString(reader, 110);
                            NursingIntervention.QmedOrdSet = GetString(reader, 111);
                            NursingIntervention.Qmed2OrdSet = GetString(reader, 112);
                            NursingIntervention.Qmed3OrdSet = GetString(reader, 113);
                            NursingIntervention.Qmed4OrdSet = GetString(reader, 114);
                            NursingIntervention.Qmed5OrdSet = GetString(reader, 115);
                            NursingIntervention.Qmed6OrdSet = GetString(reader, 116);
                            NursingIntervention.Qmed7OrdSet = GetString(reader, 117);
                            NursingIntervention.Qmed8OrdSet = GetString(reader, 118);
                            NursingIntervention.Qmed9OrdSet = GetString(reader, 119);
                            NursingIntervention.Qmed10OrdSet = GetString(reader, 120);
                            NursingInterventionList.Add(NursingIntervention);
                        }

                        conn.Close();
                        conn.Dispose();
                    }


                }
            }


            return NursingInterventionList;
        }
        public List<MedicationModel> MedicationNew(String EpiNo, String PAADMRowId)
        {

            List<MedicationModel> MedicationList = new List<MedicationModel>();
            List<DoctorNameModel> DoctorNameModelList2 = new List<DoctorNameModel>();
            PatientDao patientDao = new PatientDao();
            DoctorNameModelList2 = patientDao.FindDoctorName(PAADMRowId);
            int i=0;
            if (DoctorNameModelList2 != null)
            {
                if (DoctorNameModelList2.Count > 0)
                {
                    for (i=0;i<DoctorNameModelList2.Count;i++){

                   MedicationModel model = new MedicationModel();
                    DataTable dt = new DataTable();
                    int j = 0;
                    dt = GetMedication(EpiNo, DoctorNameModelList2[i].DoctorCode);
                    if (dt.Rows.Count > 0)
                    {
                        for (j = 0; j < dt.Rows.Count; j++)
                        {
                            if (!String.IsNullOrEmpty(dt.Rows[j]["ARCIM_DESC"].ToString())) { model.ARCIM_DESC = dt.Rows[j]["ARCIM_DESC"].ToString(); } else { model.ARCIM_DESC = ""; }
                            if (!String.IsNullOrEmpty(dt.Rows[j]["CTUOM_Desc"].ToString())) { model.CTUOM_Desc = dt.Rows[j]["CTUOM_Desc"].ToString(); } else { model.CTUOM_Desc = ""; }
                            if (!String.IsNullOrEmpty(dt.Rows[j]["PHCFR_Desc2"].ToString())) { model.PHCFR_Desc2 = dt.Rows[j]["PHCFR_Desc2"].ToString(); } else { model.PHCFR_Desc2 = ""; }
                            if (!String.IsNullOrEmpty(dt.Rows[j]["OEORI_PhQtyOrd"].ToString())) { model.OEORI_PhQtyOrd = dt.Rows[j]["OEORI_PhQtyOrd"].ToString(); } else { model.OEORI_PhQtyOrd = ""; }
                            if (!String.IsNullOrEmpty(dt.Rows[j]["CTPCP_Code"].ToString())) { model.CTPCP_Code2 = dt.Rows[j]["CTPCP_Code"].ToString(); }
                            model.DoctorRowNumber = DoctorNameModelList2[i].DoctorRowNumber;
                            if ((model.OEORI_PhQtyOrd.Trim().Length > 0) && (model.CTUOM_Desc.Trim().Length > 0)) { model.Dose = model.OEORI_PhQtyOrd + " " + model.CTUOM_Desc + " " + model.PHCFR_Desc2; } else model.Dose = "";

                            MedicationList.Add(model);
                        }
                      


                    }

                  


                    }
                    



                }

              
            }



            return MedicationList;
        }
        public List<MedicationModel> Medication(String EpiNo)
        {

            List<MedicationModel> MedicationList = new List<MedicationModel>();

            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                String sql = @"SELECT distinct ARCIM_DESC,CTUOM_Desc,PHCFR_Desc2,OEORI_PhQtyOrd,CTPCP_Code,CTPCP_Desc,CTLOC_Code,CTLOC_Desc,OEORI_DoseQty,PHCFR_Desc1,PHCDU_Desc1,OEORI_DepProcNotes      FROM VSVH_PHAORD WHERE     paadm_ADMNO = '" + EpiNo + "'   AND ORCAT_DESC like 'Medicine%'  and  OSTAT_Desc <>'D/C (Discontinued)'  ORDER BY ARCIM_Desc ,OEORI_PRESCNO desc , OEORI_PRESCSEQNO desc";
            


                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;

                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MedicationModel model = new MedicationModel();

                            model.ARCIM_DESC = GetString(reader, 0);
                            model.CTUOM_Desc = GetString(reader, 1);
                            model.PHCFR_Desc2 = GetString(reader, 2);
                            model.OEORI_PhQtyOrd = GetString(reader, 3);
                            model.CTPCP_Code2 = GetString(reader, 4);
                            model.ORI_CPNameItemOrder = GetString(reader, 5);
                           

                            if (model.OEORI_PhQtyOrd.Trim().Length > 0)
                            {
                                model.ORI_PhQtyOrd2 = " # " + model.OEORI_PhQtyOrd;
                            }
                            else { model.ORI_PhQtyOrd2 = model.OEORI_PhQtyOrd; }
                            model.LocationCode = GetString(reader, 6);
                            model.LocationDesc = GetString(reader, 7);
                            model.ORI_DoseQty = GetString(reader, 8);
                            model.PHCFR_Desc1 = GetString(reader,9);
                            model.PHCDU_Desc1 = GetString(reader, 10);
                            model.OEORIDepProcNotes = GetString(reader, 11);

                            if ((model.OEORI_PhQtyOrd.Trim().Length > 0) && (model.CTUOM_Desc.Trim().Length > 0)) { model.Dose = model.ORI_DoseQty + " " + model.CTUOM_Desc + " " + model.PHCFR_Desc1 + " " + model.PHCDU_Desc1; } else model.Dose = "";


                            MedicationList.Add(model);
                        }

                        conn.Close();
                        conn.Dispose();
                    }


                }
            }


            return MedicationList;
        }
        public List<SocHistModel> SocHistCachecon(string EpiNo)
        {
            List<SocHistModel> SocHistList = new List<SocHistModel>();
            CacheConnection CacheConnect = new CacheConnection(Cache_DB_Connecttion);
            CacheConnect.Open();
            {
                String SQLtext = @"SELECT SCH_RowID, SCH_Habits_DR->HAB_Desc, SCH_OnsetDate, SCH_DuratYear, SCH_DuratMonth, SCH_DuratDays, SCH_Desc, SCH_CTCP_DR->CTPCP_Desc, SCH_UpdateUser_DR->SSUSR_Name, SCH_HabitsQty_DR->QTY_Desc FROM PA_SocHist WHERE (SCH_PAPMI_ParRef = " + EpiNo+ ") ";
                CacheCommand Command = new CacheCommand(SQLtext, CacheConnect);

                // Create the datareader object and read the data stream.
                CacheDataReader reader = Command.ExecuteReader();
                while (reader.Read())
                {
                    SocHistModel model = new SocHistModel();

                    model.SCH_RowID= reader[0] + "";
                    model.HAB_Desc = reader[1] + "";
                    model.SCH_OnsetDate = reader[2] + "";
                    model.SCH_DuratYear = reader[3] + "";
                    model.SCH_DuratMonth= reader[4] + "";
                    model.SCH_DuratDays = reader[5] + "";
                    model.SCH_Desc = reader[6] + "";
                    model.CTPCP_Desc = reader[7] + "";
                    model.SSUSR_Name = reader[8] + "";
                    model.QTY_Desc = reader[9] + "";
                    SocHistList.Add(model);
                }

                CacheConnect.Close();
                CacheConnect.Dispose();
                return SocHistList;
            }

        }
        public List<PAFamilyModel> PAFamilyCachecon(string EpiNo)
        {
            List<PAFamilyModel> PAFamiryList = new List<PAFamilyModel>();
            CacheConnection CacheConnect = new CacheConnection(Cache_DB_Connecttion);
            CacheConnect.Open();
            {
                String SQLtext = @"SELECT FAM_RowID, FAM_MRCICD_DR->MRCID_Desc, FAM_OnsetDate, FAM_DuratYear, FAM_DuratMonth, FAM_DuratDays, FAM_Desc, FAM_CTCP_DR->CTPCP_Desc, FAM_UpdateUser_DR->SSUSR_Name, FAM_Relation_DR->CTRLT_Desc FROM PA_Family WHERE (FAM_PAPMI_ParRef = " + EpiNo + ") ORDER BY FAM_Date, FAM_Time ";
                CacheCommand Command = new CacheCommand(SQLtext, CacheConnect);

                // Create the datareader object and read the data stream.
                CacheDataReader reader = Command.ExecuteReader();
                while (reader.Read())
                {
                    PAFamilyModel model = new PAFamilyModel();

                    model.FAM_RowID = reader[0] + "";
                    model.MRCID_Desc = reader[1] + "";
                    model.FAM_OnsetDate = reader[2] + "";
                    model.FAM_DuratYear = reader[3] + "";
                    model.FAM_DuratMonth = reader[4] + "";
                    model.FAM_Desc = reader[5] + "";
                    model.CTPCP_Desc = reader[6] + "";
                    model.SSUSR_Name = reader[7] + "";
                    model.CTRLT_Desc = reader[8] + "";                
                    PAFamiryList.Add(model);
                }

                CacheConnect.Close();
                CacheConnect.Dispose();
                return PAFamiryList;
            }

        }
        public List<MedicationModel> MedicationDoctorCacheconn(String EpiNo)
        {
            List<MedicationModel> MedicationList = new List<MedicationModel>();

            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))         
            //string ConnStr =
            //    "Server = 10.104.10.47;"
            //    + " Port = 1972;"
            //    + " Namespace = PROD-TRAK;"
            //    + " User ID = superuser;"
            //    + " Password = sys;";
            //CacheConnection CacheConnect;
            CacheConnection CacheConnect = new CacheConnection(Cache_DB_Connecttion);
            //CacheConnect.ConnectionString = ConnStr;
            CacheConnect.Open();
            {
                //String SQLtext = @"SELECT distinct CTPCP_Code,CTPCP_Desc,CTLOC_Code,CTLOC_Desc    FROM VSVH_PHAORD WHERE     paadm_ADMNO = '" + EpiNo + "'   AND ORCAT_DESC like 'Medicine%'  and  OSTAT_Desc <>'D/C (Discontinued)'  ";
                String SQLtext = @"SELECT ""VSVH_PHAORD"".CTPCP_Code,""VSVH_PHAORD"".CTPCP_Desc,""VSVH_PHAORD"".CTLOC_Code,""VSVH_PHAORD"".CTLOC_Desc,""CT_CareProv"".CTPCP_SubSpec_DR->CTSPC_Desc,""CT_CareProv"".CTPCP_SMCNo FROM VSVH_PHAORD ""VSVH_PHAORD"",SQLUser.CT_CareProv ""CT_CareProv"" WHERE ""VSVH_PHAORD"".ORCAT_Desc = 'Medicine%' AND ""VSVH_PHAORD"".OSTAT_Desc <> 'D/C (Discontinued)' AND ""CT_CareProv"".CTPCP_Code = ""VSVH_PHAORD"".CTPCP_Code AND ""VSVH_PHAORD"".PAADM_ADMNo = '"+ EpiNo + @"' GROUP BY ""VSVH_PHAORD"".CTPCP_Code";
                CacheCommand Command = new CacheCommand(SQLtext, CacheConnect);

                // Create the datareader object and read the data stream.
                CacheDataReader reader = Command.ExecuteReader();
                while (reader.Read())
                {
                    MedicationModel model = new MedicationModel();

                    model.CTPCP_Code2 = reader[0] + "";
                    model.CTPCP_Desc = reader[1] + "";
                    model.LocationCode = reader[2] + "";
                    model.LocationDesc = reader[3] + "";
                    model.SubSpec =  reader[4] + "";
                    model.CTPCPSMCNo = reader[5] + "";
                    MedicationList.Add(model);
                }

                CacheConnect.Close();
                CacheConnect.Dispose();
                return MedicationList;
            }
        }
        public List<MedicationModel> MedicationDoctor(String EpiNo)
        {

            List<MedicationModel> MedicationList = new List<MedicationModel>();
            using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            {
                String sql = @"SELECT distinct CTPCP_Code,CTPCP_Desc,CTLOC_Code,CTLOC_Desc    FROM VSVH_PHAORD WHERE     paadm_ADMNO = '" + EpiNo + "'   AND ORCAT_DESC like 'Medicine%'  and  OSTAT_Desc <>'D/C (Discontinued)'  ";



                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (OdbcCommand command = new OdbcCommand())
                {
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;

                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MedicationModel model = new MedicationModel();

                            model.CTPCP_Code2 = GetString(reader, 0);
                            model.CTPCP_Desc = GetString(reader, 1);
                            model.LocationCode = GetString(reader, 2);
                            model.LocationDesc = GetString(reader, 3);
                            MedicationList.Add(model);
                        }

                        conn.Close();
                        conn.Dispose();
                    }


                }
            }


            return MedicationList;
        }

    }
}