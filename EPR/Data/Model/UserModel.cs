using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Data.Model
{
    public class UserModel
    {
        public string SSUSR_Initials { set; get; }
        public string SSUSR_Password { set; get; }
        public string SSUSR_Name { set; get; }
        public string session { set; get; }
    }
}