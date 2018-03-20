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
using System.Configuration;

namespace App.Data.Dao
{
    public class DocscanDAO
    {
        protected readonly String DB_Docscan = ConfigurationManager.ConnectionStrings["DB_Docscan"].ConnectionString;
        protected readonly String Cache_DB_Connecttion = ConfigurationManager.ConnectionStrings["CacheConnect"].ConnectionString;
        public List<DocScanModel> DocScanSQLServer(string HN)
        {
            List<DocScanModel> DocScanList = new List<DocScanModel>();
            SqlConnection conn = new SqlConnection(DB_Docscan);
            conn.Open();
            {
                String sql = "select hn,episode,docgrp+docsubgrp as doctype,prefixname as docname,itemno,runningpage as page,doctorcode as care,scanuser as u,replace(replace(replace(convert(nvarchar,createdate,20),'-','_'),' ','_'),':','_') as d 	from VW_EPR left join mdr_docsubgroup on docgrp=code and docsubgrp=subcode where hn='" + HN.Replace("-","")+"'";
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
        public string GetString(SqlDataReader reader, int colIndex)
        {
            if (reader[colIndex] != null)
            {
                String data = reader[colIndex].ToString();
                return data;
            }
            else
                return string.Empty;
        }
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


    }
    
}