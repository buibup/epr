using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Data.Model
{
    public class QuippeModel
    {
        public string HN { get; set; }
        public int id { get; set; }
        public DateTime time { get; set; }
        public string timeString { get; set; }
        public string code { get; set; }

        public string TemplateName { get; set; }
    }

    public class Encounter
    {
        public int id { get; set; }
        public DateTime time { get; set; }
        public string code { get; set; }
    }

    public class Encounters
    {
        public string patientId { get; set; }
        public List<Encounter> encounter { get; set; }
    }

    public class RootObject
    {
        public Encounters encounters { get; set; }
    }

    public class RootObject2
    {
        public Encounters2 encounters { get; set; }
    }
    public class Encounters2
    {
        public string patientId { get; set; }
        public Encounter encounter { get; set; }
    }
    

    public class QuippeDocumentsModel
    {
        public string DocumentId { get; set; }
        public string DocumentType { get; set; }
        public string PatientId { get; set; }
        public int EncounterId { get; set; }
        public int ProviderId { get; set; }
        public string Data { get; set; }
        public string CUser { get; set; }
        public DateTime CWhen { get; set; }
        public string MUser { get; set; }
        public DateTime MWhen { get; set; }
        public string PatientVisitId { get; set; }

        public string TemplateName { get; set; }

    }

}