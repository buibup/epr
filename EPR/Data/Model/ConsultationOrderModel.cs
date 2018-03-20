using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Data.Model
{
    public class ConsultationOrderModel
    {
        public DateTime? QUESDate { get; set; }
        public String QUESPAAdmDR { get; set; }
        public String QUESTime { get; set; }
        public String QNotSpeci { get; set; }
        public String QSpeci { get; set; }
        public String QUrgent { get; set; }
        public String QNotUrgent { get; set; }
        public String QOPDAppoin { get; set; }
        public String QDate { get; set; }
        public String QProblem { get; set; }
        public String QSpecifiedDoctor { get; set; }
        public String QDepartDesc { get; set; }
        public String cpCode { get; set; }
        public String cpDesc { get; set; }
        public String userUpdate { get; set; }
        public String QSpecified { get; set; }
        public String QUrgentCb { get; set; }
        public String QDepartTXT { get; set; }

        public String QUESDateSTR
        {
            get
            {
                if (QUESDate != null)
                {
                    return QUESDate.Value.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                }

                return "";
            }
        }

    }
}