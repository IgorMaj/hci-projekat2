

namespace ScheduleApp.model
{
    public class ClassroomSoftware
    {

        public ClassroomSoftware() { }
        
        public string Label { get; set; }
        public string Name { get; set; }
        public ClassroomOS OS { get; set; }
        public string Vendor { get; set; }
        public string Site { get; set; }
        public int YearOfPublication { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

    }
}