using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Data.Model
{
    public class LaboratoryTestSetModel
    {
        public String tsCode { get; set; }
        public String tsName { get; set; }

        public List<LaboratoryResultsModel> LaboratoryResultsModelList { get; set; }

        public LaboratoryTestSetModel(String tsCode, String tsName)
        {
            this.tsCode = tsCode;
            this.tsName = tsName;

            LaboratoryResultsModelList = new List<LaboratoryResultsModel>();
        }

    }

}