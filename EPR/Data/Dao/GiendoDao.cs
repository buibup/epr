using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Data.Dao;
using App.Data.Model;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using InterSystems.Data.CacheClient;
using InterSystems.Data.CacheTypes;

namespace App.Data.Dao
{
    public class GiendoDao : BaseDao
    {
        public List<GiendoModel> FindGiendo(string HN)
        {
            List<GiendoModel> modelList = new List<GiendoModel>();
            SqlConnection conn = new SqlConnection(DB_giendo);
            conn.Open();
            {

                //String sql = "select  * from GIENDTRA where RN='" + HN + "' order by En_date desc";
                String sql = "select  * from GIENDTRA where RN='" + HN + "' union select  * from GIENDTRH where RN='" + HN + "' order by EN_DATE desc";
                //String sql = "select* from GIENDTRA inner join GIENDPIC ON GIENDTRA.ROWID = Giendpic.ROWID where GIENDTRA.RN = '" + HN + "'";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    GiendoModel model;
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //int i = 0;

                        while (reader.Read())
                        {
                            model = new GiendoModel();
                            model.RN = GetString(reader, 0);
                            model.EN_DATE = GetString(reader, 1);
                            model.EN_TIME = GetString(reader, 2);
                            model.PT_NAME = GetString(reader, 3);
                            model.AGE = GetString(reader, 4);
                            model.SEX = GetString(reader, 5);
                            model.ENDOSCOPIST = GetString(reader, 6);
                            model.ANESTHESIOLOGIST = GetString(reader, 7);
                            model.ENDOSCOPIC_NO = GetString(reader, 8);
                            model.INSTRUMENT = GetString(reader, 9);
                            model.ANESTHESIA = GetString(reader, 10);
                            model.PREMEDICATION = GetString(reader, 11);
                            model.INDICATION = GetString(reader, 12);
                            model.CONSENT = GetString(reader, 13);
                            model.EN_PROCEDURE = GetString(reader, 14);
                            model.BIOPSY = GetString(reader, 15);
                            model.QUICK_UREASE = GetString(reader, 16);
                            model.OROPHARYNX = GetString(reader, 17);
                            model.ESOPHAGUS = GetString(reader, 18);
                            model.EG_JUNCTION = GetString(reader, 19);
                            model.STOMACH_CARDIA = GetString(reader, 20);
                            model.FUNDUS = GetString(reader, 21);
                            model.BODY = GetString(reader, 22);
                            model.ANTRUM = GetString(reader, 23);
                            model.PYLORUS = GetString(reader, 24);
                            model.DUODENUM_BULB = GetString(reader, 25);
                            model.SEC_PART = GetString(reader, 26);
                            model.OTHER = GetString(reader, 27);
                            model.OTHERS = GetString(reader, 28);
                            model.DIAGNOSIS = GetString(reader, 29);
                            model.THERAPY = GetString(reader, 30);
                            model.SITE_OF_BIOPSY = GetString(reader, 31);
                            model.RECOMMENDATION = GetString(reader, 32);
                            model.NOTE = GetString(reader, 33);
                            model.ROWID = GetString(reader, 34);
                            model.WARD = GetString(reader, 35);
                            model.HEAD = GetString(reader, 36);
                            model.GiendPicList = getPic(model.ROWID);
                            if (model.GiendPicList.Count() == 0) {
                                model.GiendPicList = getPih(model.ROWID);
                            } 
                            modelList.Add(model);

                        }
                    }
                }
            }

            return modelList;

        }
        public List<GiendPic> getPic(string ROWID)
        {
            List<GiendPic> modelPICList = new List<GiendPic>();
            SqlConnection conn = new SqlConnection(DB_giendo);
            conn.Open();
            {
                String sql = "select  SEQ,PIC,DES,DROWID from GIENDPIC where ROWID='" + ROWID + "'";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    GiendPic model;
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //int i = 0;

                        while (reader.Read())
                        {
                            byte[] b = null;                           
                            model = new GiendPic();
                            model.SEQ = GetString(reader, 0);
                            model.PIC = Convert.ToBase64String((byte[])reader[1]);                           
                            model.Desc = GetString(reader, 2);
                            model.DROWID = GetString(reader, 3);
                            modelPICList.Add(model);
                        }

                    }

                }
            }
            return modelPICList;
        }
        public List<GiendPic> getPih(string ROWID)
        {
            List<GiendPic> modelPICList = new List<GiendPic>();
            SqlConnection conn = new SqlConnection(DB_giendo);
            conn.Open();
            {
                String sql = "select  SEQ,PIC,DES,DROWID from GIENDPIH where ROWID='" + ROWID + "'";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    GiendPic model;
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //int i = 0;

                        while (reader.Read())
                        {
                            byte[] b = null;
                            model = new GiendPic();
                            model.SEQ = GetString(reader, 0);
                            model.PIC = Convert.ToBase64String((byte[])reader[1]);
                            model.Desc = GetString(reader, 2);
                            model.DROWID = GetString(reader, 3);
                            modelPICList.Add(model);
                        }

                    }

                }
            }
            return modelPICList;
        }
    }
}
    