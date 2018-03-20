using App.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using System.Xml;
using System.Net;
using Newtonsoft.Json;
using System.Globalization;
using System.Configuration;
using System.Collections.Specialized;

namespace App.Data.Dao
{
    public class QuippeDao : BaseDao
    {
        public QuippeDocumentsModel FindQuippe(string Code, int id)
        {
            List<QuippeDocumentsModel> modelList = new List<QuippeDocumentsModel>();
            QuippeDocumentsModel model = new QuippeDocumentsModel();


            MySqlConnection conn = new MySqlConnection(DB_Quippe);
            conn.Open();
            {
                String sql = "select * from quippe.documents where ProviderId='" + Code + "' AND EncounterId='" + id + "' order by ProviderId desc";

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (MySqlCommand command = new MySqlCommand())
                {

                    command.Connection = conn;
                    //command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    //command.CommandTimeout = 3000;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        //int i = 0;

                        while (reader.Read())
                        {
                            model = new QuippeDocumentsModel();
                            model.DocumentId = GetString(reader, 0);





                            model.Data = GetString(reader, 5);





                            model.PatientVisitId = GetString(reader, 10);



                            var doc = new XmlDocument();
                            doc.LoadXml(model.Data);
                            foreach (XmlNode item in doc.SelectNodes("Document"))
                            {
                                var TemplateName = item.Attributes["TemplateName"].Value;

                                model.TemplateName = TemplateName;
                            }

                            modelList.Add(model);

                        }
                    }
                }
            }

            return model;

        }


        public List<QuippeModel> FindAllQuippe(string HN)
        {
            string setHN;
            if (!(HN.Contains("-")))
            {
                var HHN = HN.Insert(4, "-");
                setHN = HHN.Insert(2, "-");
            }
            else
            {
                setHN = HN;
            }
            setHN = HN;

            //string getUrl = ConfigurationManager.AppSettings["QuippeUrl"];
            //string getCookie = getCookieByLogin();

            //HttpWebRequest request = WebRequest.Create(getUrl) as HttpWebRequest;
            //request.Headers.Add(HttpRequestHeader.Cookie, getCookie);


            //string url = getUrl + "/ws.aspx/Quippe/PatientData/PatientEncounters?PatientId=" + setHN + "&DataFormat=2&Culture=en-US";
           
            //get QuippeList 
            List<QuippeModel> model = new List<QuippeModel>();
            try
            {
                //var w = new WebClient();
                //w.Headers.Add(HttpRequestHeader.Cookie, getCookie);
                //var json_data = w.DownloadString(url);

                //try
                //{
                    //var modell = JsonConvert.DeserializeObject<RootObject>(json_data);
                    var modell = GetEncounterByHN(HN);

                    // model = json.tolist();
                    if (modell.encounters.encounter != null)
                    {
                        foreach (var item in modell.encounters.encounter)
                        {
                            QuippeModel m = new QuippeModel();
                            m.HN = HN;
                            m.code = item.code;
                            m.id = item.id;
                            m.time = item.time;
                            m.timeString = item.time.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                            QuippeDao quippeDAO2 = new QuippeDao();
                            QuippeDocumentsModel getQuippe = quippeDAO2.FindQuippe(item.code, item.id);

                            m.TemplateName = getQuippe.TemplateName;

                            model.Add(m);
                        }
                    }
                //}
                //catch (Exception e)
                //{
                //    var modell = JsonConvert.DeserializeObject<RootObject2>(json_data);

                    // model = json.tolist();
                    //if (modell.encounters.encounter != null)
                    //{
                    //    RootObject Mo = new RootObject();
                    //    Encounters Enc = new Encounters();
                    //    List<Encounter> LEN = new List<Encounter>();
                    //    Encounter En = new Encounter();

                    //    var SpatientId = modell.encounters.patientId;
                    //    var iitem = modell.encounters.encounter;
                    //    En.code = iitem.code;
                    //    En.id = iitem.id;
                    //    En.time = iitem.time;

                    //    LEN.Add(En);
                    //    Enc.patientId = SpatientId;
                    //    Enc.encounter = LEN;

                    //    Mo.encounters = Enc;

                    //    foreach (var item in Mo.encounters.encounter)
                    //    {
                    //        QuippeModel m = new QuippeModel();
                    //        m.HN = HN;
                    //        m.code = item.code;
                    //        m.id = item.id;
                    //        m.time = item.time;
                    //        m.timeString = item.time.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                    //        QuippeDao quippeDAO2 = new QuippeDao();
                    //        QuippeDocumentsModel getQuippe = quippeDAO2.FindQuippe(item.code, item.id);

                    //        m.TemplateName = getQuippe.TemplateName;

                    //        model.Add(m);
                    //    }

                    //}
                //}






            }
            catch (Exception ex)
            {

            }

            return model;
        }

        public string getCookieByLogin()
        {
            string QuippeCookie = "";

            string getUrl = ConfigurationManager.AppSettings["QuippeUrl"];

            string getQUsername = ConfigurationManager.AppSettings["QUsername"];
            string getQPassword = ConfigurationManager.AppSettings["QPassword"];

            using (WebClient client = new WebClient())
            {
                byte[] response =
                //client.UploadValues(getUrl + "/ws.aspx/Quippe/Security/Login?DataFormat=0&Culture=en-US", new NameValueCollection()
                client.UploadValues(getUrl + "/ws.aspx/Quippe/Security/Login?DataFormat=0&Culture=en-US", new NameValueCollection()
                {
                       { "Username", getQUsername },
                       { "Password", getQPassword },
                       { "Persistent", "True" },
                       { "RequestId", "" },
                });

                string result = System.Text.Encoding.UTF8.GetString(response);

                String[] getArr = result.Split('"');

                string findCookie = "";
                if (getArr.Length > 6)
                {
                    findCookie = getArr[5];
                }

                QuippeCookie = ".QuippeDemoSiteAuth=" + findCookie;

            }

            return QuippeCookie;
        }

        public RootObject GetEncounterByHN(string HN)
        {
            RootObject result = new RootObject();
            List<Encounter> modelList = new List<Encounter>();
            Encounter model = new Encounter();


            MySqlConnection conn = new MySqlConnection(DB_Quippe);
            conn.Open();
            {
                String sql = "select * from quippe.documents where HN='" + HN + "' order by EncounterId";

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (MySqlCommand command = new MySqlCommand())
                {

                    command.Connection = conn;
                    //command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    //command.CommandTimeout = 3000;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        //int i = 0;

                        while (reader.Read())
                        {
                            model = new Encounter();

                            model.id = GetInteger(reader, 3);
                            //model.time = GetDateTime(reader, 7).Value;
                            model.code = GetString(reader, 2);
                            
                            modelList.Add(model);

                        }
                    }
                }
            }

            
            Encounters obj = new Encounters();
            List<Encounter> objE = new List<Encounter>();

            foreach (var item in modelList)
            {
                Encounter mo = new Encounter();
                mo.id = item.id;
                mo.code = item.code;
                mo.time = item.time;

                objE.Add(mo);
            }
            obj.patientId = HN;
            obj.encounter = objE;
            result.encounters = obj;

            return result;
        }

    }
}