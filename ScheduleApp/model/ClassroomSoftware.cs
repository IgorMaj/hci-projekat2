

using System;
using System.ComponentModel;

namespace ScheduleApp.model
{
    public class ClassroomSoftware : IDataErrorInfo
    {

        public ClassroomSoftware() { }
        
        public string Label { get; set; }
        public string Name { get; set; }
        public ClassroomOS OS { get; set; }
        public string Vendor { get; set; }
        public string Site { get; set; }
        public DateTime YearOfPublication { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

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
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        result = "Please enter a name";
                }
                if (columnName == "Vendor")
                {
                    if (string.IsNullOrEmpty(Vendor))
                        result = "Please enter a vendor";
                }
                if (columnName == "Site")
                {
                    if (string.IsNullOrEmpty(Site))
                        result = "Please enter a site";
                }
                if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                        result = "Please enter a Description";
                }
                if (columnName == "Price")
                {
                    if (Price < 0)
                        result = "Please enter a valid price";
                }
                return result;
            }
        }

        #endregion
    }
}