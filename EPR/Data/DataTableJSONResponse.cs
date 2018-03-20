using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace App.Data
{
    public class DataTableJSONResponse<T>
    {
        public long iTotalRecords { get; set; }

        public long iTotalDisplayRecords { get; set; }

        public String sEcho { get; set; }

        private List<T> aaData { get; set; }

        public DataTableJSONResponse()
        {
            this.aaData = new List<T>();
        }

        public void Add(T item)
        {
            aaData.Add(item);
        }

        internal void AddAll(IEnumerable<T> rows)
        {
            this.aaData.AddRange(rows);
        }

        public String SerializeJSON()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.WriteStartObject();

                writer.WritePropertyName("sEcho");
                writer.WriteValue(sEcho);

                writer.WritePropertyName("iTotalRecords");
                writer.WriteValue(iTotalRecords);

                writer.WritePropertyName("iTotalDisplayRecords");
                writer.WriteValue(iTotalDisplayRecords);

                writer.WritePropertyName("aaData");
                if (aaData != null)
                {
                    writer.WriteStartArray();

                    foreach (T item in this.aaData)
                    {
                        writer.WriteRawValue(JsonConvert.SerializeObject(item, Formatting.None));                        
                    }

                    writer.WriteEndArray();
                }
                else
                {
                    writer.WriteNull();
                }

                writer.WriteEndObject();
            }
    
            return sb.ToString();
        }

       
    }
}