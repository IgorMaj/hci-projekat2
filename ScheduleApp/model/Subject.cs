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
 
        public string Label { get; set; }
        public string Name { get; set; }
        public Department Department { get; set; }
        public string Description { get; set; }
        public int GroupSize { get; set; }
        public int MinimalTermLength { get; set; } //u jedinicama od po 45 minuta



        public int NumRequiredTerms {
            get { return numRequiredTerms; }
           set {
                numRequiredTerms = value;
                OnPropertyChanged("numRequiredTerms");
            } }

        public bool ProjectorRequired { get; set; }
        public bool TableRequired { get; set; }
        public bool SmartTableRequired { get; set; }
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
            return "Label: "+Label+"\nName:"+Name+"\nMinimal term length(in mins): "+MinimalTermLength*45+"\nNumber of required terms: "+NumRequiredTerms+"\n";
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
                        result = "Please enter a label";
                }
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        result = "Please enter a name";
                }
                if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                        result = "Please enter a description";
                }
                if (columnName == "GroupSize")
                {
                    if (GroupSize < 0)
                        result = "Please enter a valid number for group size";
                }
                if (columnName == "MinimalTermLength")
                {
                    if (MinimalTermLength < 0)
                        result = "Please enter a valid number for minimal term length";
                }
                if (columnName == "NumRequiredTerms")
                {
                    if (NumRequiredTerms < 0)
                        result = "Please enter a valid number for number of required terms";
                }
                return result;
            }
        }

        #endregion
    }
}
