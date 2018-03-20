using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Data.Model
{
    public class HistoryMedModel
    {       
        
//SELECT "pa_adm".PAADM_PAPMI_DR,"MR_Medication".MED_DrgForm_DR->PHCDF_PHCD_ParRef->PHCD_Name,"MR_Medication".MED_DurationFree,"MR_Medication".MED_Comments,"MR_Medication".MED_Details,"MR_Medication".MED_Ceased,"MR_Medication".MED_LastUpdateDate,"MR_Medication".MED_LastUpdateTime,"MR_Medication".MED_DSReportFlag
//FROM pa_adm "pa_adm",SQLUser.MR_Medication "MR_Medication"
//WHERE "pa_adm".PAADM_PAPMI_DR = 1687137 AND "pa_adm".PAADM_MainMRADM_DR = "MR_Medication".MED_ParRef

        public String PAADM_PAPMI_DR { get; set; }
        public String PHCD_Name { get; set; }
        public String MED_DurationFree { get; set; }
        public String MED_Comments { get; set; }
        public String MED_Details { get; set; }
        public String MED_Ceased { get; set; }
        public String MED_LastUpdateDate { get; set; }
        public String MED_LastUpdateTime { get; set; }
        public String MED_DSReportFlag { get; set; }     

    }
}