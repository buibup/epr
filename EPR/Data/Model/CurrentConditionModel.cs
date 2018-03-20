using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Data.Model
{
    public class CurrentConditionModel
    {

//SELECT "pa_adm".PAADM_RowID,"pa_adm".PAADM_MainMRADM_DR,"pa_adm".PAADM_PAPMI_DR,"MR_PresentIllness".PRESI_ICDCode_DR->MRCID_Desc,"MR_PresentIllness".PRESI_ParRef,"MR_PresentIllness".PRESI_EndDate
//FROM pa_adm "pa_adm",SQLUser.MR_PresentIllness "MR_PresentIllness"
//WHERE "pa_adm".PAADM_PAPMI_DR = 57068 AND "pa_adm".PAADM_MainMRADM_DR = "MR_PresentIllness".PRESI_ParRef

        public String PAADM_RowID { get; set; }
        public String PAADM_MainMRADM_DR { get; set; }
        public String PAADM_PAPMI_DR { get; set; }
        public String MRCID_Desc { get; set; }
        public String PRESI_ParRef { get; set; }
        public String PRESI_EndDate { get; set; }
    }
}