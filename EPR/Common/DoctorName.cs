using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Common
{
    public class DoctorName
    {
       //When get episode from Trakcare HIS, epi also contain episode+hn, then system must remove HN from episode before used in other section.
       // ฟังก์์ชั่นนี้จะลำดับคำนำหน้าชื่อหมอภาษาไทยให้เหมาะสม 
       // โดยเรียง นพ. และ พญ. ไว้ด้านหน้า
        public static String clean(String name){
            string nName = name.Replace("  ", " ");
            nName = nName.Replace(" ,น", ",น");
            nName = nName.Replace(", น", ",น");
            nName = nName.Replace(" ,พ", ",พ");
            nName = nName.Replace(", พ", ",พ");

            if (String.IsNullOrEmpty(nName))
            {
                return nName;
            }
            else if (nName.EndsWith(",นพ.") || nName.EndsWith(" ,นพ."))
            {
                nName = nName.Replace(",นพ.", "");
                nName = "นพ." + nName;
                return nName;
            }
            else if (nName.EndsWith(",น.พ.") || nName.EndsWith(" ,น.พ."))
            {
                nName = nName.Replace(",น.พ.", "");
                nName = "นพ." + nName;
                return nName;
            }
            else if (nName.EndsWith(",พญ.") || nName.EndsWith(" ,พญ."))
            {
                nName = nName.Replace(",พญ.", "");
                nName = "พญ." + nName;
                return nName;
            }
            else if (nName.EndsWith(",พ.ญ.") || nName.EndsWith(" ,พ.ญ."))
            {
                nName = nName.Replace(",พ.ญ.", "");
                nName = "พญ." + nName;
                return nName;
            }
            else if (nName.Contains(",") && (nName.Contains("ค") || nName.Contains("ท") || nName.Contains("น") || nName.Contains("ผ") || nName.Contains("พ")))
            {
                String pname = nName.Substring(0, nName.IndexOf(","));
                String pre = nName.Substring(nName.IndexOf(",") + 1).Trim();
                nName = pre + " " + pname;
                return nName;
            }
            else
            {
                return nName;
            }
            
        }
    }
}