using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleApp.model
{
    //Predmet
    public class Subject
    {

        public Subject() { }

 
        public string Label { get; set; }
        public string Name { get; set; }
        public Department Department { get; set; }
        public string Description { get; set; }
        public int GroupSize { get; set; }
        public int MinimalTermLength { get; set; } //u jedinicama od po 45 minuta
        public int NumRequiredTerms { get; set; }
        public bool ProjectorRequired { get; set; }
        public bool TableRequired { get; set; }
        public bool SmartTableRequired { get; set; }
        public ClassroomOS OSRequired { get; set; }
        public virtual List<ClassroomSoftware> SoftwareRequired { get; set; }

    }
}
