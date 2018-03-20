using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;


namespace App.Data.Model
{
    public class OrderInterest
    {
        public string ARCIM_Code {set;get;}
        public string ARCIM_Desc { set; get; }
        public DateTime? OEORI_SttDat { set; get; }
        public String OEORI_SttDat_ofString
        {
            get
            {
                if (OEORI_SttDat != null)
                {
                    return OEORI_SttDat.Value.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                }

                return "";
            }
        }
    }
}
   
