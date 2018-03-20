using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Data.Model
{
    public class ClinicalNotesModel
    {
        public DateTime? NOTDate { get; set; }
        public String NOTTime { get; set; }
        public String NOTNurseIdDesc { get; set; }
        public String NOTEnterLocCode { get; set; }
        public String NOTEnterLocDesc { get; set; }
        public String NOTClinNoteTypeCode { get; set; }
        public String NOTClinNoteTypeDesc { get; set; }
        public String NotesHtmlPlainText { get; set; }
        public String NOTEditCPDesc { get; set; }
        public String ReasonError { get; set; }
        public String notNurseId { get; set; }
        public String notNurseIdCode { get; set; }
      
        public String inxCP { get; set; }
        public String NotesHtmlPlainTextIllness { get; set; }
        public String NOTDateStr
        {
            get
            {
                if (NOTDate != null)
                {
                    return NOTDate.Value.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture) + " " + NOTTime.Substring(0, 5); ;
                }
                else { return "";  } 

                
            }
        }
        public String notNurseSMCNo { set; get; }
        public String notNurseSubSpec { set; get; }

      

    }
}