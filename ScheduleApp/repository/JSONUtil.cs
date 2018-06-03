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

    public class JSONContainer{
        public ObservableCollection<Classroom> Classrooms { get; set; }
        public ObservableCollection<Subject> AvailableSubjects { get; set; }
    }

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
            t2.Subject = subj2;
            t2.Classroom = classroom1;
            t2.Day = WEEKDAY.THURSDAY;
            t2.Time = new TimeSpan(7,0,0);
            t2.Length = subj2.MinimalTermLength;
            classroom1.Terms.Add(t2);

            Classroom classroom2 = new Classroom() { Label = "U2", Description = "Druga ucionica", NumOfEmployees = 10, OS = ClassroomOS.WINDOWS_AND_LINUX };
            classroom2.InstalledSoftware = new List<ClassroomSoftware>();
            classroom2.InstalledSoftware.Add(new ClassroomSoftware() { Label = "S2", Description = "Ovo je neki softver", Name = "Software2", OS = ClassroomOS.WINDOWS_AND_LINUX, Price = 45 });
            classroom2.Terms = new List<Term>();

            Subject subj3 = new Subject() { Label = "P3", Description = "Ovo je neki treci predmet", OSRequired = ClassroomOS.WINDOWS, NumRequiredTerms = 2, MinimalTermLength = 2, SoftwareRequired = new List<ClassroomSoftware>() };
            Term t3 = new Term();
            t3.Classroom = classroom2;
            t3.Day = WEEKDAY.SATURDAY;
            t3.Subject = subj3;
            t3.Time = new TimeSpan(11, 0, 0);
            t3.Length = subj3.MinimalTermLength;
            classroom2.Terms.Add(t3);

            Subject subj4 = new Subject() { Label = "P4", Description = "Ovo je neki drugi predmet", OSRequired = ClassroomOS.WINDOWS, NumRequiredTerms = 2, MinimalTermLength = 3, SoftwareRequired = new List<ClassroomSoftware>() };
            Term t4 = new Term();
            t4.Classroom = classroom2;
            t4.Day = WEEKDAY.FRIDAY;
            t4.Subject = subj4;
            t4.Time = new TimeSpan(12, 0, 0);
            t4.Length = subj4.MinimalTermLength;
            classroom2.Terms.Add(t4);

            ObservableCollection<Classroom> cLassrooms = new ObservableCollection<Classroom>();
            cLassrooms.Add(classroom1);
            cLassrooms.Add(classroom2);
            JSONContainer container = new JSONContainer();
            container.Classrooms = cLassrooms;
            container.AvailableSubjects = new ObservableCollection<Subject>();
            container.AvailableSubjects.Add(subj1);
            container.AvailableSubjects.Add(subj2);
            container.AvailableSubjects.Add(subj3);
            container.AvailableSubjects.Add(subj4);
            var json = JsonConvert.SerializeObject(container, new JsonSerializerSettings()
            {
               PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented
            });
            sw.Write(json);
            sw.Close();
            
        }

        //container objekat bi trebalo da sadrzi sve ostale
        public static JSONContainer LoadContainer(string path) {

            StreamReader sr = new StreamReader(path);
            string json = sr.ReadToEnd();
            var container = JsonConvert.DeserializeObject<JSONContainer>(json);
            sr.Close();
            Obj = container;
            Path = path;

            return container;
        }

        public static void Save() {
            StreamWriter sw = new StreamWriter(Path);
            var json = JsonConvert.SerializeObject(Obj, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented
            });
            sw.Write(json);
            sw.Close();
        }

        public static void DeleteTermFromCLassroomAndSave(Term t) {
           //TODO delete
            Save();
        }



        
    }
}
