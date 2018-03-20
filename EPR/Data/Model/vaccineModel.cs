using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace App.Data.Model
{
    public class vaccineModel
    {
        public string PAPMI_HN { set; get; }
        public string PAPMI_FirstName { set; get; }
        public string PAPMI_LastName { set; get; }
        public DateTime? ORI_Date { set; get; }
        public string AgeDays { set; get; }
        public string ORI_ItemDesc { set; get; }
        public string ORI_ItemCode { set; get; }
        public String ORI_DateSTR
        {
            get
            {
                if (ORI_Date != null)
                {
                    return ORI_Date.Value.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                }

                return "";
            }
        }
    }
}