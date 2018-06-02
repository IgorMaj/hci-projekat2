using Newtonsoft.Json;
using ScheduleApp.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleApp.repository
{
    public class JSONUtil
    {
        

        static JSONUtil() {
            
        }

        public static string Path;
        public static Object Obj;

        public static void CreateDummyData(string path) {
            StreamWriter sw = new StreamWriter(path);

            Classroom classroom1 = new Classroom() { Label = "U1", Description = "Prva ucionica", NumOfEmployees = 10, OS = ClassroomOS.WINDOWS_AND_LINUX };
            classroom1.InstalledSoftware = new List<ClassroomSoftware>();
            classroom1.InstalledSoftware.Add(new ClassroomSoftware() { Label = "S1", Description = "Ovo je neki softver", Name = "Software1", OS = ClassroomOS.WINDOWS_AND_LINUX, Price = 45 });
            classroom1.Terms = new List<Term>();

            Subject subj1 = new Subject() { Label = "P1", Description = "Ovo je neki predmet", OSRequired = ClassroomOS.WINDOWS, NumRequiredTerms = 2, MinimalTermLength = 2, SoftwareRequired = new List<ClassroomSoftware>() };
            Term t = new Term();
            t.Classroom = classroom1;
            t.Day = WEEKDAY.WEDNESDAY;
            t.Subject = subj1;
            t.Time = new TimeSpan(10, 0, 0);
            t.Length = subj1.MinimalTermLength;
            classroom1.Terms.Add(t);

            Subject subj2 = new Subject() { Label = "P2", Description = "Ovo je neki drugi predmet", OSRequired = ClassroomOS.WINDOWS, NumRequiredTerms = 2, MinimalTermLength = 3, SoftwareRequired = new List<ClassroomSoftware>() };
            Term t2 = new Term();
            t2.Classroom = classroom1;
            t2.Day = WEEKDAY.THURSDAY;
            t2.Subject = subj2;
            t2.Time = new TimeSpan(12, 0, 0);
            t2.Length = subj2.MinimalTermLength;
            classroom1.Terms.Add(t2);

            List<Classroom> cLassrooms = new List<Classroom>();
            cLassrooms.Add(classroom1);
            var json = JsonConvert.SerializeObject(cLassrooms, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            sw.Write(json);
            sw.Close();
            
        }

        public static ObservableCollection<Classroom> LoadClassrooms(string path) {
            StreamReader sr = new StreamReader(path);
            string json = sr.ReadToEnd();
            var classrooms = JsonConvert.DeserializeObject<ObservableCollection<Classroom>>(json);
            sr.Close();
            return classrooms;
        }

        public static void Save() {
            StreamWriter sw = new StreamWriter(Path);
            var json = JsonConvert.SerializeObject(Obj, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            });
            sw.Write(json);
            sw.Close();
        }

        public static void DeleteTermFromCLassroomAndSave(Term t) {
            var classrooms = (ObservableCollection<Classroom>)Obj;
            foreach (Classroom classroom in classrooms) {
                if (classroom.Terms.Contains(t)) {
                    classroom.Terms.Remove(t);
                    break;
                }
            }
            Save();
        }



        
    }
}
