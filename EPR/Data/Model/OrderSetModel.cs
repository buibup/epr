using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Data.Model
{
    public class OrderSetModel
    {
        public String orderSetDesc { get; set; }

        public List<MedicationModel> MedicationModelList { get; set; }

        public OrderSetModel(String orderSetDesc)
        {
            this.orderSetDesc = orderSetDesc;

            MedicationModelList = new List<MedicationModel>();
        }

    }

}