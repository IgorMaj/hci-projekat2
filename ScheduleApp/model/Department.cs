using System;
using System.ComponentModel;

namespace ScheduleApp.model
{
    //smer 
    public class Department : IDataErrorInfo
    {

        
        public string Label { get; set; }
        public string Name { get; set; }

        public DateTime DateOfIntroduction { get; set; }
        public string Description { get; set; }

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