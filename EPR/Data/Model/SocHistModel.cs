using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Data.Model
{
    public class SocHistModel
    {
//        SELECT SCH_RowID, SCH_Habits_DR->HAB_Desc, 
//SCH_OnsetDate, SCH_DuratYear, 
//SCH_DuratMonth, SCH_DuratDays, SCH_Desc, 
//SCH_CTCP_DR->CTPCP_Desc, 
//SCH_UpdateUser_DR->SSUSR_Name, 
//SCH_HabitsQty_DR->QTY_Desc 
//FROM PA_SocHist 
//WHERE (SCH_PAPMI_ParRef = 1641125) 
        public String SCH_RowID { set; get; }
        public String HAB_Desc { set; get; }
        public String SCH_OnsetDate { set; get; }
        public String SCH_DuratYear { set; get; }
        public String SCH_DuratMonth { set; get; }
        public String SCH_DuratDays { set; get; }
        public String SCH_Desc { set; get; }
        public String CTPCP_Desc { set; get; }
        public String SSUSR_Name { set; get; }
        public String QTY_Desc { set; get; }
    }
}