using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Reflection;
using System.ComponentModel;

namespace App.Common.Helpers
{
    public static class SQLDataMapper
    {
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
    }
}