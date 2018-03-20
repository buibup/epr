using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Data.Model
{
    public class OrderDateModel
    {
        public DateTime? SttDate { get; set; }
        public String SttDateSTR { get; set; }
        public String SttDateSTRYYYYMMDD { get; set; }

        public List<OrderSetModel> OrderSetModelList { get; set; }

        public OrderDateModel(DateTime? SttDate)
        {
            this.SttDate = SttDate;
            this.SttDateSTR = SttDatSTR1;
            this.SttDateSTRYYYYMMDD = SttDatSTR2;

            OrderSetModelList = new List<OrderSetModel>();
        }

        public String SttDatSTR1
        {
            get
            {
                if (SttDate != null)
                {
                    return SttDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                return "";
            }
        }

        public String SttDatSTR2
        {
            get
            {
                if (SttDate != null)
                {
                    return SttDate.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                }

                return "";
            }
        }

    }

}