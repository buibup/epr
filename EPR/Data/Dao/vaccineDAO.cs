using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using InterSystems.Data.CacheClient;
using InterSystems.Data.CacheTypes;
using System.Configuration;
using App.Data.Model;
using System.Globalization;

namespace App.Data.Dao
{
    public class vaccineDAO
    {
        public string GetString(CacheDataReader reader, int colIndex)
        {
            if (reader[colIndex] != null)
            {
                String data = reader[colIndex].ToString();
                return data;
            }
            else
                return string.Empty;
        }
        public DateTime? GetDateTime(CacheDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                String type = reader.GetDataTypeName(colIndex);
                String data = reader[colIndex].ToString();
                if (type.Equals("DATE"))
                    return Convert.ToDateTime(data);
                else
                {
                    return DateTime.ParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
            }
            else
                return null;
        }

        public List<vaccineModel> getvaccinceModelList(string HN)
        {
            List<vaccineModel> vaccineList = new List<vaccineModel>();
            String Cache_DB_Connecttion = ConfigurationManager.ConnectionStrings["CacheConnect"].ConnectionString;
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            conn.Open();
            {

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    vaccineModel model;
                    CacheCommand cmd = new CacheCommand(@"select distinct * from (SELECT OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_No AS PAPMI_HN,OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_Name AS PAPMI_FirstName,OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_Name2 AS PAPMI_LastName,OE_OrdItem->OEORI_Date AS ORI_Date,
                                                            OE_OrdItem->OEORI_Date-OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_DOB As AGE,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_Desc AS ORI_ItemDesc,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_PHCDF_DR->PHCDF_PHCD_ParRef->PHCD_PHCSC_DR->PHCSC_Code,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_RowId,
                                                            OE_OrdItem->OEORI_ItemStat_DR->OSTAT_Code,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_Code As  ORI_ItemCode
                                                            FROM oe_order where OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_No='" + HN + @"' and OE_OrdItem->OEORI_ItmMast_DR->ARCIM_PHCDF_DR->PHCDF_PHCD_ParRef->PHCD_PHCSC_DR->PHCSC_Code='17B' 

                                                            union

                                                            SELECT OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_No AS PAPMI_HN,OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_Name AS PAPMI_FirstName,OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_Name2 AS PAPMI_LastName,OE_OrdItem->OEORI_Date AS ORI_Date,
                                                            OE_OrdItem->OEORI_Date-OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_DOB As AGE,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_Desc AS ORI_ItemDesc,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_PHCDF_DR->PHCDF_PHCD_ParRef->PHCD_PHCSC_DR->PHCSC_Code,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_RowId,
                                                            OE_OrdItem->OEORI_ItemStat_DR->OSTAT_Code,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_Code As  ORI_ItemCode
                                                            FROM oe_order where OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_No='" + HN + @"' and  (OE_OrdItem->OEORI_ItmMast_DR->ARCIM_RowId in ('10074||1','10075||1','25580||1','10073||1'))

                                                            union

                                                            SELECT OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_No AS PAPMI_HN,OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_Name AS PAPMI_FirstName,OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_Name2 AS PAPMI_LastName,OE_OrdItem->OEORI_Date AS ORI_Date,
                                                            OE_OrdItem->OEORI_Date-OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_DOB As AGE,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_Desc AS ORI_ItemDesc,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_PHCDF_DR->PHCDF_PHCD_ParRef->PHCD_PHCSC_DR->PHCSC_Code,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_RowId,
                                                            OE_OrdItem->OEORI_ItemStat_DR->OSTAT_Code,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_Code As  ORI_ItemCode
                                                            FROM oe_order where OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_No='" + HN + @"' and OE_OrdItem->OEORI_ItmMast_DR->ARCIM_Desc LIKE 'je%'


                                                            union

                                                            SELECT OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_No AS PAPMI_HN,OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_Name AS PAPMI_FirstName,OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_Name2 AS PAPMI_LastName,OE_OrdItem->OEORI_Date AS ORI_Date,
                                                            OE_OrdItem->OEORI_Date-OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_DOB As AGE,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_Desc AS ORI_ItemDesc,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_PHCDF_DR->PHCDF_PHCD_ParRef->PHCD_PHCSC_DR->PHCSC_Code,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_RowId,
                                                            OE_OrdItem->OEORI_ItemStat_DR->OSTAT_Code,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_Code As  ORI_ItemCode
                                                            FROM oe_order where OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_No='" + HN + @"' and OE_OrdItem->OEORI_ItmMast_DR->ARCIM_Desc LIKE '%วัคซีนลูกรัก%'

                                                            union

                                                            SELECT OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_No AS PAPMI_HN,OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_Name AS PAPMI_FirstName,OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_Name2 AS PAPMI_LastName,OE_OrdItem->OEORI_Date AS ORI_Date,
                                                            OE_OrdItem->OEORI_Date-OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_DOB As AGE,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_Desc AS ORI_ItemDesc,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_PHCDF_DR->PHCDF_PHCD_ParRef->PHCD_PHCSC_DR->PHCSC_Code,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_RowId,
                                                            OE_OrdItem->OEORI_ItemStat_DR->OSTAT_Code,
                                                            OE_OrdItem->OEORI_ItmMast_DR->ARCIM_Code As  ORI_ItemCode
                                                            FROM oe_order where OEORD_Adm_DR->PAADM_PAPMI_DR->PAPMI_No='" + HN + @"' and OE_OrdItem->OEORI_ItmMast_DR->ARCIM_Desc LIKE '%Vaccine%' and OE_OrdItem->OEORI_ItmMast_DR->ARCIM_Desc not LIKE '%นัด%' and OE_OrdItem->OEORI_ItmMast_DR->ARCIM_Desc not LIKE '%ชุด%') where OSTAT_Code <> 'D'  and ORI_ItemDesc not like '%ชุด%' and  ORI_ItemDesc not like '%นัด%'  order by ORI_Date 
                    ", conn);
                    using (CacheDataReader reader = cmd.ExecuteReader())
                    {
                        int i = 0;
                        DateTime tempDate;
                        while (reader.Read())
                        {
                            i = i + 1;
                            model = new vaccineModel();
                            model.PAPMI_HN = GetString(reader, 0);
                            model.PAPMI_FirstName = GetString(reader, 1);
                            model.PAPMI_LastName = GetString(reader, 2);
                            model.ORI_Date = GetDateTime(reader, 3);                           
                            model.AgeDays = CalAge(GetString(reader, 4));
                            model.ORI_ItemDesc = GetString(reader, 5);
                            model.ORI_ItemCode = GetString(reader, 9);

                            vaccineList.Add(model);
                        }


                        conn.Dispose();
                        conn.Close();
                    }
                }
            }
            return vaccineList;
        }
        public List<AppointModel> getAppointmentList(string HN)
        {
            String Cache_DB_Connecttion = ConfigurationManager.ConnectionStrings["CacheConnect"].ConnectionString;
            CacheConnection conn = new CacheConnection(Cache_DB_Connecttion);
            List<AppointModel> AppointmentList = new List<AppointModel>();
            conn.Open();
            {
                string sql = "select  PAADM_AdmDate,PAADM_AdmTime,PAADM_DepCode_DR->CTLOC_Desc,PAADM_AdmDocCodeDR->CTPCP_Desc,PAADM_PAPMI_DR->PAPMI_RowId,PAADM_PAPMI_DR->PAPMI_IPNo,PAADM_ADMNo,PAADM_VisitStatus from pa_adm where (PAADM_AdmDate > getDate())  and (PAADM_PAPMI_DR->PAPMI_IPNo='" + HN + "') and PAADM_VisitStatus <> 'C'";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (CacheCommand command = new CacheCommand())
                {
                    AppointModel model = new AppointModel();
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    using (CacheDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            model.PAADM_AdmDate = GetString(reader, 0);
                            model.PAADM_AdmTime = GetString(reader, 1);
                            model.CTLOC_Des = GetString(reader, 2);
                            model.CTPCP_Desc = GetString(reader, 3);
                            model.PAPMI_RowId = GetString(reader, 4);
                            model.PAPMI_IPNo = GetString(reader, 5);
                            model.PAADM_VisitStatus = GetString(reader, 7);
                            AppointmentList.Add(model);
                        }
                    }
                }

            }
            return AppointmentList;
        }

        public string CalAge(string Age)
        {
            string FullAge = "";
            int numberofAge = Int32.Parse(Age) + 1;
            if (numberofAge > 30)
            {
                FullAge = FullAge + (numberofAge / 365).ToString() + " Y " + ((numberofAge % 365) / 30).ToString() + " M " + ((numberofAge % 365) % 30).ToString() + " D";
            }
            else
            {
                FullAge = numberofAge.ToString() + " D";
            }
            return FullAge;
        }

    }

}