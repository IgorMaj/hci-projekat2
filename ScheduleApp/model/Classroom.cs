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

        }
        public string Label { get; set; }
        public string Description { get; set; }
        public int NumOfEmployees { get; set; }
        public bool HasProjector { get; set; }
        public bool HasTable { get; set; }
        public bool HasSmartTable { get; set; }
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
                        result = "Please enter a Label";
                }
                if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                        result = "Please enter a Description";
                }
                if (columnName == "NumOfEmployees")
                {
                    if (NumOfEmployees < 0)
                        result = "Please enter a valid number of employees";
                }
                return result;
            }
        }

        #endregion

    }
}
