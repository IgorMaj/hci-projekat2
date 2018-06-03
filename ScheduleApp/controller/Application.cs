using Newtonsoft.Json;
using ScheduleApp.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleApp.controller
{
    public class Application
    {


        public ObservableCollection<Classroom> classrooms = new ObservableCollection<Classroom>();
        public List<Term> terms = new List<Term>();
        public ObservableCollection<Subject> subjects = new ObservableCollection<Subject>();
        public List<Department> departments = new List<Department>();
        public List<ClassroomSoftware> classroomSoft = new List<ClassroomSoftware>();
        public Application() { }
        public void writeData()
        {

            using (StreamWriter file = File.CreateText("data.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file,this );
            }
        }


        public Application loadData()
        {
            using (StreamReader r = new StreamReader("data.json"))
            {
                string json = r.ReadToEnd();
                Application retVal = JsonConvert.DeserializeObject<Application>(json);
                if (retVal == null)
                {
                    return new Application();
                }
                else
                {
                    return retVal;
                }
            }
        }
    }

   

}

