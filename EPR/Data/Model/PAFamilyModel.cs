using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Data.Model
{
    public class PAFamilyModel
    {
        //SELECT FAM_RowID, FAM_MRCICD_DR->MRCID_Desc, 
        //FAM_OnsetDate, FAM_DuratYear, 
        //FAM_DuratMonth, FAM_DuratDays, FAM_Desc, 
        //FAM_CTCP_DR->CTPCP_Desc, 
        //FAM_UpdateUser_DR->SSUSR_Name, 
        //FAM_Relation_DR->CTRLT_Desc 
        //FROM PA_Family 
        //WHERE (FAM_PAPMI_ParRef = 1641125) 
        //ORDER BY FAM_Date, FAM_Time 

        public String FAM_RowID { set; get; }
        public String MRCID_Desc { set; get; }
        public String FAM_OnsetDate { set; get; }
        public String FAM_DuratYear { set; get; }
        public String FAM_DuratMonth { set; get; }
        public String FAM_DuratDays { set; get; }
        public String FAM_Desc { set; get; }
        public String CTPCP_Desc { set; get; }
        public String SSUSR_Name { set; get; }
        public String CTRLT_Desc { set; get; }
    }
}