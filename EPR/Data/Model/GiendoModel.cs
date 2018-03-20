using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Data.Model
{
    public class GiendoModel
    {
        public string RN { set; get; }
        public string EN_DATE { set; get; }
        public string EN_TIME { set; get; }
        public string PT_NAME { set; get; }
        public string AGE { set; get; }
        public string SEX { set; get; }
        public string ENDOSCOPIST { set; get; }
        public string ANESTHESIOLOGIST { set; get; }
        public string ENDOSCOPIC_NO { set; get; }
        public string INSTRUMENT { set; get; }
        public string ANESTHESIA { set; get; }
        public string PREMEDICATION { set; get; }
        public string INDICATION { set; get; }
        public string CONSENT { set; get; }
        public string EN_PROCEDURE { set; get; }
        public string BIOPSY { set; get; }
        public string QUICK_UREASE { set; get; }
        public string OROPHARYNX { set; get; }
        public string ESOPHAGUS { set; get; }
        public string EG_JUNCTION { set; get; }
        public string STOMACH_CARDIA { set; get; }
        public string FUNDUS { set; get; }
        public string BODY { set; get; }
        public string ANTRUM { set; get; }
        public string PYLORUS { set; get; }
        public string DUODENUM_BULB { set; get; }
        public string SEC_PART { set; get; }
        public string OTHER { set; get; }
        public string OTHERS { set; get; }
        public string DIAGNOSIS { set; get; }
        public string THERAPY { set; get; }
        public string SITE_OF_BIOPSY { set; get; }
        public string RECOMMENDATION { set; get; }
        public string NOTE { set; get; }
        public string ROWID { set; get; }
        public string WARD { set; get; }
        public string HEAD { set; get; }
        public bool marked { get; set; }
        public bool active { get; set; }
        public List<GiendPic> GiendPicList { get; set; }
    }
    public class GiendPic {
        public string SEQ { set; get; }
        public string PIC { set; get; }
        public string Desc { set; get; }
        public string DROWID { set; get; }
    }
}