using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.Data.Odbc;
using System.Globalization;
using InterSystems.Data.CacheClient;
using InterSystems.Data.CacheTypes;
using MySql.Data.MySqlClient;

namespace App.Data.Dao
{

    public class BaseDao
    {
        protected readonly String TRAKCARE_DB_CONNECTION = ConfigurationManager.ConnectionStrings["Trakcare"].ConnectionString;

        protected readonly String Cache_DB_Connecttion = ConfigurationManager.ConnectionStrings["CacheConnect"].ConnectionString;

        protected readonly String DB_Docscan = ConfigurationManager.ConnectionStrings["DB_Docscan"].ConnectionString;

        protected readonly String DB_giendo = ConfigurationManager.ConnectionStrings["DB_giendo"].ConnectionString;

        protected readonly String DB_Quippe = ConfigurationManager.ConnectionStrings["DB_Quippe"].ConnectionString;

        protected readonly String JCI = ConfigurationManager.ConnectionStrings["JCI"].ConnectionString;

        protected readonly String OrderInterestString = ConfigurationManager.ConnectionStrings["OrderInterestString"].ConnectionString;

        public static List<Dictionary<String, Object>> MapToDictionaryCollection(IDataReader reader)
        {
            List<Dictionary<String, Object>> items = new List<Dictionary<string, object>>();
            while (reader.Read())
            {
                Dictionary<String, Object> newObject = new Dictionary<String, Object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.IsDBNull(i))
                    {
                        newObject[reader.GetName(i).ToUpper()] = null;
                    }
                    else
                    {
                        newObject[reader.GetName(i).ToUpper()] = reader.GetValue(i);
                    }
                }
                items.Add(newObject);
            }

            return items;
        }

        public static T MapTo<T>(IDataReader reader) where T : new()
        {
            T result = default(T);
            Type t = typeof(T);
            Hashtable propMap = new Hashtable();
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                propMap[prop.Name.ToUpper()] = prop;
            }

            if (reader.Read())
            {
                T newObject = new T();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    PropertyInfo prop = (PropertyInfo)propMap[reader.GetName(i).ToUpper()];
                    if (prop != null && prop.CanWrite)
                    {
                        if (reader.IsDBNull(i))
                        {
                            prop.SetValue(newObject, null, null);
                        }
                        else
                        {
                            prop.SetValue(newObject, reader.GetValue(i), null);
                        }
                    }
                }
                result = newObject;
            }

            return result;
        }

        public static List<T> MapToCollection<T>(IDataReader reader) where T : new()
        {
            Type t = typeof(T);
            List<T> items = new List<T>();

            Hashtable propMap = new Hashtable();
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                propMap[prop.Name.ToUpper()] = prop;
            }

            while (reader.Read())
            {
                T newObject = new T();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    PropertyInfo prop = (PropertyInfo)propMap[reader.GetName(i).ToUpper()];
                    if (prop != null && prop.CanWrite)
                    {
                        if (reader.IsDBNull(i))
                        {
                            prop.SetValue(newObject, null, null);
                        }
                        else
                        {
                            prop.SetValue(newObject, reader.GetValue(i), null);
                        }
                    }
                }
                items.Add(newObject);
            }

            return items;
        }

        /// <summary>
        /// Alias of MapToCollection method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<T> MapToList<T>(IDataReader reader) where T : new()
        {
            return MapToCollection<T>(reader);
        }

        /// <summary>
        /// Mapping to BindingList collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static BindingList<T> MapToBindingList<T>(IDataReader reader) where T : new()
        {
            Type t = typeof(T);
            BindingList<T> items = new BindingList<T>();

            Hashtable propMap = new Hashtable();
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                propMap[prop.Name.ToUpper()] = prop;
            }

            while (reader.Read())
            {
                T newObject = new T();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    PropertyInfo prop = (PropertyInfo)propMap[reader.GetName(i).ToUpper()];
                    if (prop != null && prop.CanWrite)
                    {
                        if (reader.IsDBNull(i))
                        {
                            prop.SetValue(newObject, null, null);
                        }
                        else
                        {
                            prop.SetValue(newObject, reader.GetValue(i), null);
                        }
                    }
                }
                items.Add(newObject);
            }

            return items;
        }

        public int GetInteger(OdbcDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return Int32.Parse(reader[colIndex].ToString());
            else
                return 0;

        }
        public int GetInteger(MySqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return Int32.Parse(reader[colIndex].ToString());
            else
                return 0;

        }
        public int GetInteger(CacheDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return Int32.Parse(reader[colIndex].ToString());
            else
                return 0;

        }
        public string GetIntegerSTR(OdbcDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                String dataS = reader[colIndex].ToString();
                String[] data = dataS.Split('.');
                return data[0];
            }
            else
                return string.Empty;
        }

        public string GetIntegerSTR(MySqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                String dataS = reader[colIndex].ToString();
                String[] data = dataS.Split('.');
                return data[0];
            }
            else
                return string.Empty;
        }

        public string GetIntegerSTR(CacheDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                String dataS = reader[colIndex].ToString();
                String[] data = dataS.Split('.');
                return data[0];
            }
            else
                return string.Empty;
        }

        public string GetString(OdbcDataReader reader, String colName)
        {
            if (reader[colName] != null)
            {
                String data = reader[colName].ToString();
                return data;
            }
            else
                return string.Empty;
        }

        public string GetString(MySqlDataReader reader, String colName)
        {
            if (reader[colName] != null)
            {
                String data = reader[colName].ToString();
                return data;
            }
            else
                return string.Empty;
        }

        public string GetString(CacheDataReader reader, String colName)
        {
            if (reader[colName] != null)
            {
                String data = reader[colName].ToString();
                return data;
            }
            else
                return string.Empty;
        }
        public string GetString(OdbcDataReader reader, int colIndex)
        {
            if (reader[colIndex] != null)
            {
                String data = reader[colIndex].ToString();
                return data;
            }
            else
                return string.Empty;
        }
        public string GetString(MySqlDataReader reader, int colIndex)
        {
            if (reader[colIndex] != null)
            {
                String data = reader[colIndex].ToString();
                return data;
            }
            else
                return string.Empty;
        }
        public string GetString(System.Data.SqlClient.SqlDataReader reader, int colIndex)
        {
            if (reader[colIndex] != null)
            {
                String data = reader[colIndex].ToString();
                return data;
            }
            else
                return string.Empty;
        }
        public string GetStringDate(System.Data.SqlClient.SqlDataReader reader, int colIndex)
        {
            if (reader[colIndex] != null)
            {
                String data = reader[colIndex].ToString();
                string[] words = data.Split('/');
                data = words[1] + '/' + words[0] + '/' + words[2];
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
        public DateTime? GetDateTime(OdbcDataReader reader, int colIndex)
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

        public DateTime? GetDateTime(MySqlDataReader reader, int colIndex)
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

        public DateTime? GetDateTimeTest(OdbcDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                String type = reader.GetDataTypeName(colIndex);
                String data = reader[colIndex].ToString();
                //String data = "01/03/2015";
                if (type.Equals("DATE"))
                    return Convert.ToDateTime(data);
                else
                {

                    //  return DateTime.ParseExact(dt, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    //  return DateTime.ParseExact(data.ToString("dd, MM", CultureInfo.InvariantCulture)); 

                    //DateTime dt = DateTime.ParseExact(data, "dd/mm/yyyy", CultureInfo.InvariantCulture);

                    //String epi = data.Substring(4, 1);
                    //if (epi.Equals("2"))
                    //{
                    //    DateTime dt = DateTime.ParseExact(data, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    //    return Convert.ToDateTime(dt.AddMonths(1));
                    //}


                    //return Convert.ToDateTime(data);
                    return DateTime.ParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
            }
            else
                return null;
        }


        public string GetTimeSTR(OdbcDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetTime(colIndex).ToString(@"hh\:mm");
            else
                return string.Empty;
        }

        public string GetTimeSTR(CacheDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetTime(colIndex).ToString(@"hh\:mm");
            else
                return string.Empty;
        }

        public string GetTimeSTR(MySqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDateTime(colIndex).ToString(@"hh\:mm");
            else
                return string.Empty;
        }
    }
}