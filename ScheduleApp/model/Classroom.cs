using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleApp.model
{
    public enum ClassroomOS { WINDOWS, LINUX, WINDOWS_AND_LINUX}
    public class Classroom : IDataErrorInfo
    {
       

        public Classroom() {
            Terms = new List<Term>();
        }
        [DisplayName("Oznaka")]
        public string Label { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }
        [DisplayName("Broj zaposlenih")]
        public int NumOfEmployees { get; set; }
        [DisplayName("Projektor")]
        public bool HasProjector { get; set; }
        [DisplayName("Tabla")]
        public bool HasTable { get; set; }
        [DisplayName("Pametna tabla")]
        public bool HasSmartTable { get; set; }
        [DisplayName("Operativni sistem")]
        public ClassroomOS OS { get; set; }
        public virtual List<ClassroomSoftware> InstalledSoftware { get; set; }
        public virtual List<Term> Terms { get; set; }

        public override string ToString()
        {
            return Label;
        }

        #region IDataErrorInfo Members

        public string Error
        {
            get { return ""; }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "Label")
                {
                    if (string.IsNullOrEmpty(Label))
                        result = "Unesite oznaku učionice";
                }
                if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                        result = "Please enter a Description";
                }
                if (columnName == "NumOfEmployees")
                {
                    if (NumOfEmployees < 0)
                        result = "Unesite celobrojnu vrednost";
                }
                return result;
            }
        }

        #endregion

    }
}
