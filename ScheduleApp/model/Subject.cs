using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleApp.model
{
    //Predmet
    public class Subject : INotifyPropertyChanged
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

        private void OnPropertyChanged(string name)
        {
           
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public override string ToString()
        {
            return "Label: "+Label+"\nName:"+Name+"\nMinimal term length: "+MinimalTermLength+"\nNumber of required terms: "+NumRequiredTerms+"\n";
        }

    }
}
