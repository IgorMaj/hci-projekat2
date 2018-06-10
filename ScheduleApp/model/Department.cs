using System;
using System.ComponentModel;

namespace ScheduleApp.model
{
    //smer 
    public class Department : IDataErrorInfo
    {

        [DisplayName("Oznaka")]
        public string Label { get; set; }
        [DisplayName("Naziv")]
        public string Name { get; set; }
        [DisplayName("Datum osnivanja")]
        public DateTime DateOfIntroduction { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }

        public override string ToString()
        {
            return Label;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) {
                return false;
            }
            if (!(obj is Department)) {
                return false;
            }

            if (((Department)obj).Label == null)
            {
                return false;
            }

            return ((Department)obj).Label.Equals(Label);
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
                if (columnName == "Name")
                {

                    if (string.IsNullOrEmpty(Name))
                        result = "Please enter a name of department";
                }
                return result;
            }
        }

        #endregion
    }
}