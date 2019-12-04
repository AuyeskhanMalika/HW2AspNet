using System;

namespace PatientCard.Model
{
    public class Journal
    {
        public int Id { get; set; }
        public virtual Patient Patient { get; set; }

        public string Diagnosis { get; set; }

        public DateTime DateVisit { get; set; }
    }
}
