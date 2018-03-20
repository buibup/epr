using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Data.Model
{
    public class ProceduresModel
    {
        //OperCode OperDesc 

        public DateTime? ProcDate { get; set; }
        public String ProcTime { get; set; }
        public String OperCode { get; set; }
        public String OperDesc { get; set; }
        public String OperCatDesc { get; set; }
        public String OperCpDesc { get; set; }
        public String AnaesDesc { get; set; }

        public String ProcDateSTR
        {
            get
            {
                if (ProcDate != null)
                {
                    return ProcDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " " + ProcTime.Substring(0, 5);
                }

                return "";
            }
        }

    }
}