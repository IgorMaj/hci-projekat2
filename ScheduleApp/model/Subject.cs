using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleApp.model
{
    //Predmet
    public class Subject: INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Subject() { }

        private int numRequiredTerms;

        [DisplayName("Oznaka")]
        public string Label { get; set; }
        [DisplayName("Naziv")]
        public string Name { get; set; }
        [DisplayName("Smer")]
        public Department Department { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }
        [DisplayName("Velicina grupe")]
        public int GroupSize { get; set; }
        [DisplayName("Minimalna duzina")]
        public int MinimalTermLength { get; set; } //u jedinicama od po 45 minuta


        [DisplayName("Broj obaveznih termina")]
        public int NumRequiredTerms {
            get { return numRequiredTerms; }
           set {
                numRequiredTerms = value;
                OnPropertyChanged("numRequiredTerms");
            } }

        [DisplayName("Projektor")]
        public bool ProjectorRequired { get; set; }
        [DisplayName("Tabla")]
        public bool TableRequired { get; set; }
        [DisplayName("Pametna tabla")]
        public bool SmartTableRequired { get; set; }
        [DisplayName("Operativni sistem")]
        public ClassroomOS OSRequired { get; set; }
        public virtual List<ClassroomSoftware> SoftwareRequired { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private void OnPropertyChanged(string name)
        {
           
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Subject))
            {
                return false;
            }
            if (((Subject)obj).Label == null) {
                return false;
            }
            return ((Subject)obj).Label.Equals(Label);
        }

        public override string ToString()
        {
            return "Oznaka: "+Label+"\nNaziv:"+Name+"\nMinimalna duzina(u min): "+MinimalTermLength*45+"\nBroj obaveznih termina: "+NumRequiredTerms+"\n";
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
                if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                        result = "Please enter a description";
                }
                if (columnName == "GroupSize")
                {
                    if (GroupSize < 0)
                        result = "Unesite celobrojnu vrednost";
                }
                if (columnName == "MinimalTermLength")
                {
                    if (MinimalTermLength < 0)
                        result = "Unesite celobrojnu vrednost";
                }
                if (columnName == "NumRequiredTerms")
                {
                    if (NumRequiredTerms < 0)
                        result = "Unesite celobrojnu vrednost";
                }
                return result;
            }
        }

        #endregion
    }
}
