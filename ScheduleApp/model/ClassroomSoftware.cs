

using System;
using System.ComponentModel;

namespace ScheduleApp.model
{
    public class ClassroomSoftware : IDataErrorInfo
    {

        public ClassroomSoftware() { }

        [DisplayName("Oznaka")]
        public string Label { get; set; }
        [DisplayName("Naziv")]
        public string Name { get; set; }
        [DisplayName("Operativni sistem")]
        public ClassroomOS OS { get; set; }
        [DisplayName("Proizvodjac")]
        public string Vendor { get; set; }
        [DisplayName("Sajt")]
        public string Site { get; set; }
        [DisplayName("Godina izdanja")]
        public DateTime YearOfPublication { get; set; }
        [DisplayName("Cena")]
        public double Price { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is ClassroomSoftware))
            {
                return false;
            }
            if (((ClassroomSoftware)obj).Label == null)
            {
                return false;
            }
            return ((ClassroomSoftware)obj).Label.Equals(Label);
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
                        result = "Unesite oznaku";
                }
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        result = "Unesite naziv";
                }
                if (columnName == "Vendor")
                {
                    if (string.IsNullOrEmpty(Vendor))
                        result = "Unesite prodavca";
                }
                if (columnName == "Site")
                {
                    if (string.IsNullOrEmpty(Site))
                        result = "Unesite sajt";
                }
                if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                        result = "Please enter a Description";
                }
                if (columnName == "Price")
                {
                    if (Price < 0)
                        result = "Unesite celobrojnu vrednost";
                }
                return result;
            }
        }

        #endregion
    }
}