using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Data.Model
{
    public class MedicationModel
    {

        //ARCOS_Desc ORI_ItemDesc ORI_DoseQty ORI_SttDat ORI_OrderStatusDesc ORI_PrescNo ORI_CPNameItemOrder ORI_Billed PAADM_RowID ORI_ItemCategoryCode
        //ORI_OrderCategoryCode OEORI_RowId ORI_UserNameExecuted ORI_CPNameExecuted PHCFR_Desc1 ORI_OrderCategoryDesc ORI_OrderStatusCode
        //ITM_DailyQty ORI_PhQtyOrd ARCIM_UOM_DR ARCIM_PHCDF_DR CTUOM_Code CTUOM_Desc 

        public String ARCOS_Desc { get; set; }
        public String ORI_ItemDesc { get; set; }
        public String ORI_DoseQty { get; set; }
        public DateTime? ORI_SttDat { get; set; }
        public String ORI_PrescNo { get; set; }
        public String ORI_CPNameItemOrder { get; set; }
        public String ORI_Billed { get; set; }
        public String PAADM_RowID { get; set; }
        public String ORI_ItemCategoryCode { get; set; }
        public String ORI_OrderCategoryCode { get; set; }
        public String OEORI_RowId { get; set; }
        public String ORI_UserNameExecuted { get; set; }
        public String ORI_CPNameExecuted { get; set; }
        public String PHCFR_Desc1 { get; set; }
        public String ORI_OrderCategoryDesc { get; set; }
        public String ORI_OrderStatusCode { get; set; }
        public String ORI_OrderStatusDesc { get; set; }
        public String ITM_DailyQty { get; set; }
        public String ORI_PhQtyOrd { get; set; }
        public String ARCIM_UOM_DR { get; set; }
        public String ARCIM_PHCDF_DR { get; set; }
        public String CTUOM_Code { get; set; }
        public String CTUOM_Desc { get; set; }
        public String Dose { get; set; }
        public String OrderCategory_code { get; set; }
        public String OrderCategory { get; set; }
        public String CTPCP_Code2 { get; set; }
        public String ARCIM_DESC { get; set; }
        public String CTPCP_Desc { get; set; }
        public String LocationCode { get; set; }
        public String LocationDesc { get; set; }
        public int DoctorRowNumber { get; set; } 
        public String ORI_PhQtyOrd2 { get; set; }
        public String PHCFR_Desc2 { get; set; }
        public String OEORI_PhQtyOrd { get; set; }
        public String PHCDU_Desc1 { get; set; }
        public String CTPCPSMCNo { get; set; }
        public String SubSpec { get; set; }
        public String OEORIDepProcNotes { get; set; }
 
        public String ORI_SttDatSTR
        {
            get
            {
                if (ORI_SttDat != null)
                {
                    return ORI_SttDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                return "";
            }
        }

    }
}