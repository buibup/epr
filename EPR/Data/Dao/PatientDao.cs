using System;
using System.Collections.Generic;
using App.Data.Model;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using InterSystems.Data.CacheClient;
using InterSystems.Data.CacheTypes;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Configuration;

namespace App.Data.Dao
{
    public class PatientDao : BaseDao
    {

        public PatientModel FindByRowId(int HnRowId)
        {
            PatientModel model = new PatientModel();

            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHGetHNByRowId_getData\".('" + Convert.ToInt32(HnRowId) + "')}";
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
                            model.PAFamilyModelList = PAFamilyCachecon(Convert.ToString(HnRowId));
                            model.SocHistModelList = SocHistCachecon(Convert.ToString(HnRowId));
                            model.CurrentConditionModelList = CurrentConditionCachecon(Convert.ToString(HnRowId));
                            model.HistoryMedModelList = HistoryMedModelCachecon(Convert.ToString(HnRowId));
                            //       model.DocScanModelList = DocScanSQLServer(getHN(Convert.ToString(HnRowId)).Replace("-",""));

                        }
                        conn.Close();
                        conn.Dispose();
                    }

                }
            }

            return model;
        }

        public String getHN(String HNrowID)
        {
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                String HN = "";
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHGetHNByRowId_getData\".('" + Convert.ToInt32(HNrowID) + "')}";
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
                            HN = GetString(reader, 1);
                        }

                    }
                }
                conn.Close();
                conn.Dispose();
                return HN;
            }

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

        public ObservationModel ObservationByEpiRowid(String EpiRowid)
        {
            ObservationModel ObservationList = new ObservationModel();
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
                            ObservationList = model;

                        }

                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

            return ObservationList;
        }

        public List<ObservationModel> ObservationLastEpi(String Hn)
        {
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            List<ObservationModel> model = new List<ObservationModel>();
            conn.Open();
            {
                string sql = "select top 1 PAADM_PAPMI_DR->PAPMI_No,PAADM_RowID,PAADM_ADMNo,PAADM_PAPMI_DR	 from pa_adm  where PAADM_PAPMI_DR->PAPMI_No='" + Hn + "' and PAADM_ADMNo <> ''  order by PAADM_AdmDate desc";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand(sql, conn))
                {

                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    //OdbcDataReader reader = command.ExecuteReader();
                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string EPIRowid = GetString(reader, 1);
                            PatientDao patientDao = new PatientDao();
                            model = patientDao.Observation(EPIRowid);
                        }
                    }

                    conn.Close();
                    conn.Dispose();
                }
            }
            return model;

        }

        public List<ObservationModel> ObservationLastEpi2(String Hn)
        {
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            List<ObservationModel> model = new List<ObservationModel>();
            conn.Open();
            {
                string sql = "select top 10 PAADM_PAPMI_DR->PAPMI_No,PAADM_RowID,PAADM_ADMNo,PAADM_PAPMI_DR	 from pa_adm  where PAADM_PAPMI_DR->PAPMI_No='" + Hn + "' and PAADM_ADMNo <> ''  order by PAADM_AdmDate desc";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand(sql, conn))
                {

                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    //OdbcDataReader reader = command.ExecuteReader();
                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string EPIRowid = GetString(reader, 1);
                            PatientDao patientDao = new PatientDao();
                            model.Add(patientDao.ObservationByEpiRowid(EPIRowid));
                        }
                    }

                    conn.Close();
                    conn.Dispose();
                }
            }
            return model;

        }

        public PatientModel FindByHn(String Hn)
        {
            PatientModel model = new PatientModel();
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);

            // using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            conn.Open();
            {


                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHGetByHn_getData\".('" + Hn + "')}";

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand(sql, conn))
                {

                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    //OdbcDataReader reader = command.ExecuteReader();
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
                            model.PAFamilyModelList = PAFamilyCachecon(GetString(reader, 0));
                            model.SocHistModelList = SocHistCachecon(GetString(reader, 0));
                            model.CurrentConditionModelList = CurrentConditionCachecon(GetString(reader, 0));
                            model.HistoryMedModelList = HistoryMedModelCachecon(GetString(reader, 0));
                            model.PAPMI_PrefLanguage_DR = getPatientLanguage(model.PAPMI_No);

                            //model.DocScanModelList = DocScanSQLServer(Hn.Replace("-",""));
                            // model.DoctorCode = DoctorCode;
                        }
                        //reader.Close();
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }


            return model;
        }

        public List<DocScanModel> DocScanSQLServer(string HN)
        {
            List<DocScanModel> DocScanList = new List<DocScanModel>();
            SqlConnection conn = new SqlConnection(DB_Docscan);
            conn.Open();
            {
                String sql = "select hn,episode,docgrp+docsubgrp as doctype,prefixname as docname,itemno,runningpage as page,doctorcode as care,scanuser as u,replace(replace(replace(convert(nvarchar,createdate,20),'-','_'),' ','_'),':','_') as d 	from VW_EPR left join mdr_docsubgroup on docgrp=code and docsubgrp=subcode where hn=" + HN;
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    DocScanModel model;
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //int i = 0;

                        while (reader.Read())
                        {

                            model = new DocScanModel();
                            model.hn = GetString(reader, 0);
                            model.episode = GetString(reader, 1).Replace("-", "");
                            model.doctype = GetString(reader, 2);
                            model.docname = GetString(reader, 3);
                            model.itemno = GetString(reader, 4);
                            model.page = GetString(reader, 5);
                            model.care = GetString(reader, 6);
                            CacheConnection conn2 = new CacheConnection(Cache_DB_Connecttion);
                            String sql2 = "select SSUSR_Name from ss_user where ssusr_initials='0" + model.care + "'";
                            if (conn2.State == ConnectionState.Closed)
                                conn2.Open();
                            using (CacheCommand command2 = new CacheCommand(sql2, conn2))
                            {
                                command2.CommandType = CommandType.Text;
                                command2.CommandText = sql2;
                                command2.CommandTimeout = 3000;
                                using (CacheDataReader reader2 = command2.ExecuteReader())
                                {
                                    while (reader2.Read())
                                    {
                                        model.doctorName = GetString(reader2, 0);
                                    }
                                }
                            }
                            conn2.Close();
                            conn2.Dispose();
                            model.u = GetString(reader, 7);
                            model.d = GetString(reader, 8);
                            DocScanList.Add(model);
                        }
                    }
                }
            }
            conn.Close();
            conn.Dispose();
            return DocScanList;
        }
        public List<DoctorChiefComPlainModel> FindChiefComplintAllList(string EpiRowid)
        {

            List<DoctorChiefComPlainModel> ChiefComPlainList = new List<DoctorChiefComPlainModel>();
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHEprGetClinicalNotes_getData\".('" + EpiRowid + "')}";

                //DataTable dt = new DataTable();

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    DoctorChiefComPlainModel model;
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    // dt.Load(command.ExecuteReader());

                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        int i = 0;

                        while (reader.Read())
                        {

                            model = new DoctorChiefComPlainModel();
                            model.PAADMRowId = EpiRowid;
                            model.DoctorName = GetString(reader, 2);
                            model.LocationDesc = GetString(reader, 4);
                            model.DoctorCode = GetString(reader, 10);
                            model.DoctorRowid = GetString(reader, 11);
                            model.LocationCode = GetString(reader, 3);
                            String ChiefCode = "";
                            ChiefCode = GetString(reader, 5);
                            if ((ChiefCode == "CC") || (ChiefCode == "PN") || (ChiefCode == "PE") || (ChiefCode == "PL"))
                            {

                                if (ChiefComPlainList.Count > 0)
                                {
                                    int count = 0;
                                    for (int j = 0; j <= ChiefComPlainList.Count - 1; j++)
                                    {
                                        if ((model.LocationCode == ChiefComPlainList[j].LocationCode) && (model.DoctorCode == ChiefComPlainList[j].DoctorCode))
                                        {
                                            count = count + 1;

                                        }
                                    }

                                    if (count == 0)
                                    {
                                        ChiefComPlainList.Add(model);
                                    }

                                }
                                else
                                {
                                    ChiefComPlainList.Add(model);
                                }





                            }





                            i = i + 1;

                            //if (NotesHtmlPlainText.Length > 0) { NotesHtmlPlainText = NotesHtmlPlainText + "," + GetString(reader, 7); } else { NotesHtmlPlainText = GetString(reader, 7); }

                        }







                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

            return ChiefComPlainList;
        }



        public List<DoctorChiefComPlainModel> FindChiefComplintLocationAllList(string EpiRowid)
        {

            List<DoctorChiefComPlainModel> ChiefComPlainList = new List<DoctorChiefComPlainModel>();
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHEprGetClinicalNotes_getData\".('" + EpiRowid + "')}";

                //DataTable dt = new DataTable();

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    DoctorChiefComPlainModel model;
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    // dt.Load(command.ExecuteReader());

                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        int i = 0;

                        while (reader.Read())
                        {

                            model = new DoctorChiefComPlainModel();
                            model.PAADMRowId = EpiRowid;
                            model.LocationCode = GetString(reader, 3);
                            String ChiefCode = "";
                            ChiefCode = GetString(reader, 5);


                            if (ChiefComPlainList.Count > 0)
                            {
                                int count = 0;
                                for (int j = 0; j <= ChiefComPlainList.Count - 1; j++)
                                {
                                    if (model.LocationCode == ChiefComPlainList[j].LocationCode)
                                    {
                                        count = count + 1;

                                    }
                                }

                                if (count == 0)
                                {
                                    ChiefComPlainList.Add(model);
                                }

                            }
                            else
                            {
                                ChiefComPlainList.Add(model);
                            }










                            i = i + 1;

                            //if (NotesHtmlPlainText.Length > 0) { NotesHtmlPlainText = NotesHtmlPlainText + "," + GetString(reader, 7); } else { NotesHtmlPlainText = GetString(reader, 7); }

                        }







                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

            return ChiefComPlainList;
        }




        public List<DoctorChiefComPlainModel> FindChiefComplintList(string EpiRowid)
        {

            List<DoctorChiefComPlainModel> ChiefComPlainList = new List<DoctorChiefComPlainModel>();
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHEprGetClinicalNotesWeb_getDiag\".('" + EpiRowid + "')}";

                //DataTable dt = new DataTable();

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    DoctorChiefComPlainModel model;
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    // dt.Load(command.ExecuteReader());

                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        int i = 0;

                        while (reader.Read())
                        {

                            model = new DoctorChiefComPlainModel();
                            model.PAADMRowId = EpiRowid;
                            model.chiefComplaint = GetString(reader, 7);

                            model.DoctorName = GetString(reader, 2);
                            model.LocationDesc = GetString(reader, 4);
                            model.DoctorCode = GetString(reader, 12);
                            model.DoctorRowid = GetString(reader, 13);
                            model.LocationCode = GetString(reader, 3);
                            model.CTPCPSMCNo = GetString(reader, 14);
                            model.SubSpec = GetString(reader, 15);
                            model.DoctorDescENG = getDoctorENG(model.DoctorCode);
                            model.chiefid = i;
                            if (model.chiefComplaint.Trim() != "")
                            {
                                if (model.chiefComplaint.Length > 25)
                                {
                                    model.chiefComplaintShow = "-  " + model.chiefComplaint.Substring(0, 25) + "...";
                                }
                                else
                                {
                                    model.chiefComplaintShow = "-  " + model.chiefComplaint;
                                }
                            }
                            else
                            {
                                model.chiefComplaintShow = "     ";

                            }

                            ChiefComPlainList.Add(model);


                            i = i + 1;

                            //if (NotesHtmlPlainText.Length > 0) { NotesHtmlPlainText = NotesHtmlPlainText + "," + GetString(reader, 7); } else { NotesHtmlPlainText = GetString(reader, 7); }

                        }


                        if (ChiefComPlainList.Count <= 0)
                        {
                            for (i = 0; i < 1; i++)
                            {
                                model = new DoctorChiefComPlainModel();
                                model.PAADMRowId = EpiRowid;
                                model.chiefid = i;
                                model.chiefComplaintShow = "";
                                ChiefComPlainList.Add(model);
                            }

                        }

                        //else if(ChiefComPlainList.Count == 1)
                        //{

                        //        model = new DoctorChiefComPlainModel();
                        //        model.PAADMRowId = EpiRowid;
                        //        model.chiefid = i;
                        //        model.chiefComplaintShow = "";
                        //        ChiefComPlainList.Add(model);


                        //}


                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

            return ChiefComPlainList;
        }





        public List<DoctorChiefComPlainModel> FindChiefComplinNursetList(string EpiRowid)
        {

            List<DoctorChiefComPlainModel> ChiefComPlainList = new List<DoctorChiefComPlainModel>();
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHEprGetClinicalNotesDR_getDiag\".('" + EpiRowid + "')}";

                //DataTable dt = new DataTable();

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    DoctorChiefComPlainModel model;
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    // dt.Load(command.ExecuteReader());

                    using (CacheDataReader reader = command.ExecuteReader())
                    {


                        while (reader.Read())
                        {

                            model = new DoctorChiefComPlainModel();
                            model.PAADMRowId = EpiRowid;
                            model.chiefComplaint = GetString(reader, 7);
                            model.DoctorName = GetString(reader, 2);
                            model.DoctorCode = GetString(reader, 12);
                            model.DoctorRowid = GetString(reader, 13);
                            model.LocationCode = GetString(reader, 3);
                            model.LocationDesc = GetString(reader, 4);

                            ChiefComPlainList.Add(model);



                            //if (NotesHtmlPlainText.Length > 0) { NotesHtmlPlainText = NotesHtmlPlainText + "," + GetString(reader, 7); } else { NotesHtmlPlainText = GetString(reader, 7); }

                        }




                        conn.Close();
                        conn.Dispose();

                    }
                }
            }

            return ChiefComPlainList;
        }
        //    Private Function SelectDataFromDataset(ByVal dt As DataTable, ByVal strFromDate As String) As DataTable
        //    Dim dtResoult As New DataTable

        //    dtResoult = dt.Clone()

        //    Dim MyDataRow As DataRow
        //    For Each MyDataRow In dt.Select(" PAADMAdmDate ='" & strFromDate & "'")
        //        Dim row As Object() = MyDataRow.ItemArray
        //        dtResoult.Rows.Add(row)
        //    Next MyDataRow

        //    Return dtResoult

        //End Function

        //public DataTable SelectDataFromDT(DataTable dt)
        //{
        //    DataTable dtResoult = dt.Clone();
        //    DataRow MyDataRow = new DataRow();
        //     foreach (MyDataRow in dt.Select(" notNurseId not isnull Group by  notNurseId"))
        //     {

        //     }


        //    return dtResoult;

        //}



        public List<DoctorChiefComPlainModel> FindChiefComplintDocList(string EpiRowid)
        {

            List<DoctorChiefComPlainModel> ChiefComPlainList = new List<DoctorChiefComPlainModel>();
            DoctorChiefComPlainModel model;
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHEprGetClinicalNotesWeb_getDiag\".('" + EpiRowid + "')}";

                DataTable dt = new DataTable();

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
                        dt.Load(command.ExecuteReader());
                    }
                    catch { }
                    int i = 0;
                    //String Chifcomplain = "";
                    int count = 0;
                    //int position = 0;
                    if (dt.Rows.Count > 0)
                    {
                        for (i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                model = new DoctorChiefComPlainModel();
                                model.PAADMRowId = EpiRowid;
                                model.DoctorName = dt.Rows[i]["NOTNurseIdDesc"].ToString();
                                model.DoctorCode = dt.Rows[i]["notNurseIdCode"].ToString();
                                model.DoctorRowid = dt.Rows[i]["notNurseId"].ToString();
                                model.LocationCode = dt.Rows[i]["NOTEnterLocCode"].ToString();
                                model.LocationDesc = dt.Rows[i]["NOTEnterLocDesc"].ToString();

                                //Chifcomplain = dt.Rows[i]["NotesHtmlPlainText"].ToString();
                                //model.chiefComplaint = dt.Rows[i]["NotesHtmlPlainText"].ToString();
                                //model.chiefComplaint 
                                model.chiefid = i;
                                if (model.DoctorCode.Length > 0)
                                {
                                    ChiefComPlainList.Add(model);
                                }
                            }

                            else
                            {
                                if (ChiefComPlainList.Count > 0)
                                {
                                    for (int j = 0; j < ChiefComPlainList.Count; j++)
                                    {
                                        if (dt.Rows[i]["notNurseId"].ToString() == ChiefComPlainList[j].DoctorRowid)
                                        {
                                            count = count + 1;
                                            //Chifcomplain =Chifcomplain +","+ dt.Rows[i]["NotesHtmlPlainText"].ToString();
                                            //position = j;
                                        }

                                    }
                                }
                                //if (Chifcomplain.Trim().Length > 0 && count >0)
                                //{

                                //    ChiefComPlainList.RemoveAt(position);
                                //    model = new DoctorChiefComPlainModel();
                                //    model.chiefComplaint =Chifcomplain;
                                //    model.DoctorName = dt.Rows[i]["NOTNurseIdDesc"].ToString();
                                //    model.DoctorCode = dt.Rows[i]["notNurseIdCode"].ToString();
                                //    model.DoctorRowid = dt.Rows[i]["notNurseId"].ToString();
                                //    ChiefComPlainList.Insert(position, model);

                                //}

                                if (count == 0)
                                {
                                    model = new DoctorChiefComPlainModel();
                                    model.PAADMRowId = EpiRowid;
                                    model.DoctorName = dt.Rows[i]["NOTNurseIdDesc"].ToString();
                                    model.DoctorCode = dt.Rows[i]["notNurseIdCode"].ToString();
                                    model.DoctorRowid = dt.Rows[i]["notNurseId"].ToString();
                                    model.LocationCode = dt.Rows[i]["NOTEnterLocCode"].ToString();
                                    model.LocationDesc = dt.Rows[i]["NOTEnterLocDesc"].ToString();
                                    //model.chiefComplaint = dt.Rows[i]["NotesHtmlPlainText"].ToString();
                                    model.chiefid = i;
                                    if (model.DoctorCode.Length > 0)
                                    {
                                        ChiefComPlainList.Add(model);
                                    }
                                }

                            }



                        }
                    }


                }
            }


            conn.Close();
            conn.Dispose();

            return ChiefComPlainList;
        }





        public List<CareProvModel> FindAllCareProve()
        {
            List<CareProvModel> medList = new List<CareProvModel>();
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                //String sql = @"select CTPCP_RowId,CTPCP_Desc   from  (ct_careprov  inner join CT_CarPrvTp on  ct_careprov.CTPCP_CarPrvTp_DR =CT_CarPrvTp.CTCPT_Code )inner join  CT_Spec on  ct_careprov.CTPCP_Spec_DR= CT_Spec.CTSPC_RowId  where CTCPT_InternalType ='DOCTOR'  and (CTPCP_Code like '119%'  and  CTPCP_Desc not like '%ZZ%' and   CTPCP_Desc not like '%Cancel%' and   CTPCP_Desc not like '%xx%'   and   CTPCP_Desc not like '%A %')   order by CTPCP_Desc ";
                String sql = @"select CTPCP_RowId,CTPCP_Desc,CTPCP_Code   from  (ct_careprov  inner join CT_CarPrvTp on  ct_careprov.CTPCP_CarPrvTp_DR =CT_CarPrvTp.CTCPT_Code )inner join  CT_Spec on  ct_careprov.CTPCP_Spec_DR= CT_Spec.CTSPC_RowId  where CTCPT_InternalType ='DOCTOR'  and (CTPCP_Code like '119%'  and  CTPCP_Desc not like '%ZZ%' and   CTPCP_Desc not like '%Cancel%' and   CTPCP_Desc not like '%xx%'   and   CTPCP_Desc not like '%A %')   order by CTPCP_Desc ";

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand(sql, conn))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        CareProvModel model;
                        while (reader.Read())
                        {

                            model = new CareProvModel();
                            model.CTPCP_RowId = GetString(reader, 0);
                            model.CTPCP_Desc = GetString(reader, 1);
                            model.CTPCP_Code = GetString(reader, 2);

                            medList.Add(model);

                        }
                    }
                }
            }

            conn.Close();
            conn.Dispose();
            return medList;
        }



        // public List<EpisodeModel> FindAllEpisode(int HnRowId, string DoctorCode)
        public List<EpisodeModel> FindAllEpisode(int HnRowId)
        {
            List<EpisodeModel> medList = new List<EpisodeModel>();
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            conn.Open();
            {
                String sql = "";

                //if ((!string.IsNullOrEmpty(DoctorCode)) && HnRowId > 0)
                //{
                //    sql = @"SELECT  DISTINCT TOP 20 paadm_rowid,paadm_admno,papmi_rowid,PAADM_AdmDate ,PAADM_AdmTime,CTPCP_Code,CTPCP_Desc   from vs_admission where papmi_rowid=" + HnRowId + " and  PAADM_ADMNo <>''  and  CTPCP_Code ='" + DoctorCode + "'   order by  PAADM_RowID desc";

                //}
                //if ((string.IsNullOrEmpty(DoctorCode)) && HnRowId > 0)
                //{
                sql = @"SELECT  DISTINCT  paadm_rowid,paadm_admno,papmi_rowid,PAADM_AdmDate ,PAADM_AdmTime,CTPCP_Code,CTPCP_Desc   from vs_admission where papmi_rowid=" + HnRowId + " and  PAADM_ADMNo <>''  and Year(PAADM_AdmDate) >=" + (Convert.ToInt32(DateTime.Now.Year) - 2) + "      order by PAADM_AdmDate desc,  PAADM_RowID desc";
                //  sql = @"SELECT  DISTINCT TOP 20 paadm_rowid,paadm_admno,papmi_rowid,PAADM_AdmDate ,PAADM_AdmTime,CTPCP_Code,CTPCP_Desc   from vs_admission where papmi_rowid=" + HnRowId + " and  PAADM_ADMNo <>''  order by  PAADM_RowID desc";




                //}

                //if ((!string.IsNullOrEmpty(DoctorCode)) && HnRowId <= 0)
                //{
                //    sql = @"SELECT  DISTINCT TOP 20 paadm_rowid,paadm_admno,papmi_rowid,PAADM_AdmDate ,PAADM_AdmTime ,CTPCP_Code,CTPCP_Desc  from vs_admission where  PAADM_ADMNo <>''   and  CTPCP_Code ='" + DoctorCode + "'    order by  PAADM_RowID desc";

                //}

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                //using (OdbcCommand command = new OdbcCommand(sql, conn))
                //{
                CacheCommand command = new CacheCommand();
                command.CommandType = CommandType.Text;
                command.Connection = conn;
                command.CommandText = sql;
                command.CommandTimeout = 0;
                try
                {
                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        EpisodeModel model;
                        int i = 0;
                        while (reader.Read())
                        {
                            model = new EpisodeModel();
                            model.PAADMRowId = GetString(reader, 0);
                            model.EpisodeNo = GetString(reader, 1);
                            model.PAPMIRowId = GetString(reader, 2);
                            model.admDate = GetDateTime(reader, 3);
                            model.admTime = GetTimeSTR(reader, 4);
                            model.CTPCP_Code = GetString(reader, 5);
                            model.DiagnosisModelList = Diagnosis(GetString(reader, 0));
                            //model.CTPCP_DescM = GetString(reader, 6);
                            //model.CTPCP_RowId = GetString(reader, 7);
                            //model.chiefComplaint = ChiefComplaint(model.PAADMRowId);

                            model.ERowid = i;
                            model.showPhyList = true;
                            medList.Add(model);
                            i = i + 1;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    command.Dispose();
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }

                //}
            }


            return medList;
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
                            model.ICD10DocName = GetString(reader, 5);
                            model.MRDIADesc = GetString(reader, 6);
                            model.mrdiaDoc = GetString(reader, 7);
                            model.inxCP = GetString(reader, 8);
                            model.LocationCode = GetString(reader, 9);
                            model.LocationDesc = GetString(reader, 10);
                            DiagnosisList.Add(model);
                        }

                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

            return DiagnosisList;
        }
        public String ChiefComplaint(string EpiRowid)
        {
            String NotesHtmlPlainText = "";

            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHEprGetClinicalNotesWeb_getDiag\".('" + EpiRowid + "')}";

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

                            if (NotesHtmlPlainText.Length > 0) { NotesHtmlPlainText = NotesHtmlPlainText + "," + GetString(reader, 7); } else { NotesHtmlPlainText = GetString(reader, 7); }

                        }

                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

            return NotesHtmlPlainText;
        }


        public List<SocHistModel> SocHistCachecon(string EpiNo)
        {
            List<SocHistModel> SocHistList = new List<SocHistModel>();
            CacheConnection CacheConnect = new CacheConnection(Cache_DB_Connecttion);
            CacheConnect.Open();
            {
                String SQLtext = @"SELECT SCH_RowID, SCH_Habits_DR->HAB_Desc, SCH_OnsetDate, SCH_DuratYear, SCH_DuratMonth, SCH_DuratDays, SCH_Desc, SCH_CTCP_DR->CTPCP_Desc, SCH_UpdateUser_DR->SSUSR_Name, SCH_HabitsQty_DR->QTY_Desc FROM PA_SocHist WHERE (SCH_PAPMI_ParRef = " + EpiNo + ") ";
                CacheCommand Command = new CacheCommand(SQLtext, CacheConnect);

                // Create the datareader object and read the data stream.
                CacheDataReader reader = Command.ExecuteReader();
                while (reader.Read())
                {
                    SocHistModel model = new SocHistModel();

                    model.SCH_RowID = reader[0] + "";
                    model.HAB_Desc = reader[1] + "";
                    model.SCH_OnsetDate = reader[2] + "";
                    model.SCH_DuratYear = reader[3] + "";
                    model.SCH_DuratMonth = reader[4] + "";
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
                    model.FAM_DuratDays = reader[5] + "";
                    model.FAM_Desc = reader[6] + "";
                    model.CTPCP_Desc = reader[7] + "";
                    model.SSUSR_Name = reader[8] + "";
                    model.CTRLT_Desc = reader[9] + "";
                    PAFamiryList.Add(model);
                }

                CacheConnect.Close();
                CacheConnect.Dispose();
                return PAFamiryList;
            }

        }


        public List<CurrentConditionModel> CurrentConditionCachecon(string HnRowid)
        {
            List<CurrentConditionModel> CurrentConditionList = new List<CurrentConditionModel>();
            CacheConnection CacheConnect = new CacheConnection(Cache_DB_Connecttion);
            CacheConnect.Open();
            {
                String SQLtext = @"SELECT pa_adm.PAADM_RowID,pa_adm.PAADM_MainMRADM_DR,pa_adm.PAADM_PAPMI_DR,MR_PresentIllness.PRESI_ICDCode_DR->MRCID_Desc,MR_PresentIllness.PRESI_ParRef,MR_PresentIllness.PRESI_EndDate  FROM pa_adm pa_adm,SQLUser.MR_PresentIllness MR_PresentIllness WHERE pa_adm.PAADM_PAPMI_DR = " + HnRowid + " AND pa_adm.PAADM_MainMRADM_DR = MR_PresentIllness.PRESI_ParRef";
                CacheCommand Command = new CacheCommand(SQLtext, CacheConnect);

                // Create the datareader object and read the data stream.
                CacheDataReader reader = Command.ExecuteReader();
                while (reader.Read())
                {
                    CurrentConditionModel model = new CurrentConditionModel();
                    model.PAADM_RowID = reader[0] + "";
                    model.PAADM_MainMRADM_DR = reader[1] + "";
                    model.PAADM_PAPMI_DR = reader[2] + "";
                    model.MRCID_Desc = reader[3] + "";
                    model.PRESI_ParRef = reader[4] + "";
                    model.PRESI_EndDate = reader[5] + "";
                    CurrentConditionList.Add(model);
                }

                CacheConnect.Close();
                CacheConnect.Dispose();
                return CurrentConditionList;
            }

        }


        public List<HistoryMedModel> HistoryMedModelCachecon(string HnRowid)
        {
            List<HistoryMedModel> HistoryMedModelList = new List<HistoryMedModel>();
            CacheConnection CacheConnect = new CacheConnection(Cache_DB_Connecttion);
            CacheConnect.Open();
            {
                String SQLtext = @"SELECT pa_adm.PAADM_PAPMI_DR,MR_Medication.MED_DrgForm_DR->PHCDF_PHCD_ParRef->PHCD_Name,MR_Medication.MED_DurationFree,MR_Medication.MED_Comments,MR_Medication.MED_Details,MR_Medication.MED_Ceased,MR_Medication.MED_LastUpdateDate,MR_Medication.MED_LastUpdateTime,MR_Medication.MED_DSReportFlag FROM pa_adm pa_adm,SQLUser.MR_Medication MR_Medication WHERE pa_adm.PAADM_PAPMI_DR = " + HnRowid + " AND pa_adm.PAADM_MainMRADM_DR = MR_Medication.MED_ParRef";
                CacheCommand Command = new CacheCommand(SQLtext, CacheConnect);

                // Create the datareader object and read the data stream.
                CacheDataReader reader = Command.ExecuteReader();
                while (reader.Read())
                {
                    HistoryMedModel model = new HistoryMedModel();
                    model.PAADM_PAPMI_DR = reader[0] + "";
                    model.PHCD_Name = reader[1] + "";
                    model.MED_DurationFree = reader[2] + "";
                    model.MED_Comments = reader[3] + "";
                    model.MED_Details = reader[4] + "";
                    model.MED_Ceased = reader[5] + "";
                    model.MED_LastUpdateDate = reader[6] + "";
                    model.MED_LastUpdateTime = reader[7] + "";
                    model.MED_DSReportFlag = reader[8] + "";
                    HistoryMedModelList.Add(model);
                }

                CacheConnect.Close();
                CacheConnect.Dispose();
                return HistoryMedModelList;
            }

        }

        public EpisodeModel EpisodeRow(String EpiRowid)
        {
            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHRptEprByEpiTrak_getDiag\".('" + EpiRowid + "')}";

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;

                    //OdbcDataReader reader = command.ExecuteReader();
                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EpisodeModel model = new EpisodeModel();
                            model.PAADMRowId = GetString(reader, 0);
                            model.PAPMIRowId = GetString(reader, 1);
                            model.LocAdmDesc = GetString(reader, 2);
                            model.admCpName = GetString(reader, 3);
                            model.EpisodeNo = GetString(reader, 4);
                            model.admDate = GetDateTime(reader, 5);
                            model.admTime = GetTimeSTR(reader, 6);
                            model.PAPMINo = GetString(reader, 7);
                            model.PAPMIName3 = GetString(reader, 8);
                            model.PAPMIName = GetString(reader, 9);
                            model.PAPMIName2 = GetString(reader, 10);
                            model.AgeY = GetIntegerSTR(reader, 11);
                            model.AgeM = GetIntegerSTR(reader, 12);
                            model.SexCode = GetString(reader, 13);
                            model.wardCode = GetString(reader, 14);
                            model.wardDesc = GetString(reader, 15);
                            model.roomCode = GetString(reader, 16);
                            model.roomDesc = GetString(reader, 17);
                            model.bedCode = GetString(reader, 18);
                            model.BedTypeCode = GetString(reader, 19);
                            model.bedTypeDesc = GetString(reader, 20);
                            model.ward = GetString(reader, 21);
                            model.DOB = GetDateTime(reader, 22);
                            model.chkCC = GetString(reader, 23);
                            model.chkCN = GetString(reader, 24);
                            model.chkTM = GetString(reader, 25);
                            model.chkNN = GetString(reader, 26);
                            model.chkCons = GetString(reader, 27);
                            model.chkObs = GetString(reader, 28);
                            model.chkPhyExam = GetString(reader, 29);
                            model.chkDiag = GetString(reader, 30);
                            model.chkLabRsl = GetString(reader, 31);
                            model.chkXrayRsl = GetString(reader, 32);
                            model.PAADM_FinDischgDate = GetDateTime(reader, 34);
                            model.PAADM_FinDischgTime = GetString(reader, 35);

                            return model;
                        }

                        conn.Close();
                        conn.Dispose();
                    }

                }
            }

            return null;
        }


        public List<DoctorNameModel> FindDoctorName(String EpiRowid)
        {
            List<DoctorNameModel> DoctorNameList = new List<DoctorNameModel>();
            DataTable dt = new DataTable();

            //using (OdbcConnection conn = new OdbcConnection(TRAKCARE_DB_CONNECTION))
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                String sql = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHrptAppointmentByEpi_getData\".('" + EpiRowid + "')}";


                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand(sql, conn))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;






                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        DoctorNameModel model;
                        int i = 1;
                        while (reader.Read())
                        {
                            model = new DoctorNameModel();
                            model.DoctorCode = GetString(reader, 6);
                            model.DoctorDesc = GetString(reader, 7);
                            model.PAADMRowId = EpiRowid;
                            model.LocationCode = GetString(reader, 3);
                            model.LocationDesc = GetString(reader, 4);
                            model.CTPCPSMCNo = GetString(reader, 21);
                            model.SubSpec = GetString(reader, 22);
                            model.DoctorRowNumber = i;
                            model.DoctorDescENG = getDoctorENG(model.DoctorCode);
                            DoctorNameList.Add(model);
                            i = i + 1;

                        }

                    }
                }

            }


            //if (DoctorNameList.Count > 0)
            //{
            //    int count = 0;
            //    for (int i = 0; i <= DoctorNameList.Count - 1; i++)
            //    {
            //        string DRCode = DoctorNameList[i].DoctorCode;
            //        if (DoctorNameList.Count > 0)
            //        {
            //            for (int j = 0; j <= DoctorNameList.Count - 1; j++)
            //            {
            //                if (DoctorNameList[i].DoctorCode == DRCode)
            //                {
            //                    count = count + 1;


            //                }

            //            }
            //            if (count > 0)
            //            {
            //                DoctorNameList.RemoveAt(i);
            //                count = 0;
            //            }
            //        }



            //    }
            //}




            //if (DoctorNameList.Count > 0)
            //{
            //    int count = 0;
            //    for (int i = 0; i <= DoctorNameList.Count - 1; i++)
            //    {
            //        string DRCode = DoctorNameList[i].DoctorCode;
            //        if (DoctorNameList.Count > 0)
            //        {
            //            for (int j = 0; j <= DoctorNameList.Count - 1; j++)
            //            {
            //                if (DoctorNameList[i].DoctorCode == DRCode)
            //                {
            //                    count = count + 1;


            //                }

            //            }
            //            if (count > 0)
            //            {
            //                DoctorNameList.RemoveAt(i);
            //                count = 0;
            //            }
            //        }



            //    }
            //}

            conn.Close();
            conn.Dispose();

            return DoctorNameList;

        }

        public Boolean FindJCIList(String HN)
        {
            Boolean Flag = false;
            SqlConnection conn = new SqlConnection(JCI);
            conn.Open();
            {
                String sql = "select * from [JCI].[dbo].[HNList] where hn='" + HN.Replace("-", "") + "'";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (SqlCommand command = new SqlCommand())
                {

                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {


                        while (reader.Read())
                        {
                            Flag = true;
                        }
                    }


                }
            }
            return Flag;
        }

        public String getPatientLanguage(String HN)
        {
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                String result = "";
                String sql = @"SELECT PAPMI_PrefLanguage_DR  FROM PA_PATMAS  WHERE PAPMI_No ='" + HN + "'";
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
                            result = GetString(reader, 0);
                        }

                    }
                }
                conn.Close();
                conn.Dispose();
                return result;
            }

        }

        public String getDoctorENG(String DoctorCode)
        {
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {
                String result = "";
                String sql = @"select CTPCP_StName from ct_careprov where CTPCP_Code ='" + DoctorCode + "'";
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
                            result = GetString(reader, 0);
                        }

                    }
                }
                conn.Close();
                conn.Dispose();
                return result;
            }

        }


    }
}