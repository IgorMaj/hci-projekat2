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
        public ObservableCollection<Department> departments = new ObservableCollection<Department>();
        public ObservableCollection<ClassroomSoftware> classroomSoft = new ObservableCollection<ClassroomSoftware>();
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
            try
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
            catch (IOException) {
                return new Application();
            }
            
        }

        public bool CheckUniqueConstraint(string label) {
            if (label == null) { return false; }

            foreach (Classroom elem in classrooms) {
                if (elem.Label.Equals(label)) { return false; }
            }

            foreach (Department elem in departments)
            {
                if (elem.Label.Equals(label)) { return false; }
            }
            foreach (ClassroomSoftware elem in classroomSoft)
            {
                if (elem.Label.Equals(label)) { return false; }
            }

            foreach (Subject elem in subjects)
            {
                if (elem.Label.Equals(label)) { return false; }
            }
            return true;
        }

        public void RemoveCompletely(object selectedItem)
        {
            if (selectedItem == null) { return; }

            if (selectedItem is Classroom)
            {
                RemoveClassroomCompletely((Classroom)selectedItem);
            }
            else if (selectedItem is ClassroomSoftware)
            {
                RemoveClassroomSoftwareCompletely((ClassroomSoftware)selectedItem);
            }
            else if (selectedItem is Department)
            {
                RemoveDepartmentCompletely((Department)selectedItem);
            }
            else if (selectedItem is Subject) {
                RemoveSubjectCompletely((Subject)selectedItem);
            }

            writeData();
        }

        private void RemoveSubjectCompletely(Subject subj) {
            List<Term> termsToRemove = new List<Term>();
            subjects.Remove(subj);
            foreach (Classroom cls in classrooms) {
                foreach (Term t in cls.Terms) {
                    if (t.Subject.Equals(subj)) {
                        termsToRemove.Add(t); //ne mogu menjati kolekciju dok iteriram po njoj :/
                    }
                    
                }
            }

            foreach (Term t in termsToRemove) {
                t.Classroom.Terms.Remove(t);
                terms.Remove(t);
            }
           
        }

        private void RemoveDepartmentCompletely(Department dep)
        {
            departments.Remove(dep);
            List<Subject> subjectsToRemove = new List<Subject>();
            foreach (Subject subj in subjects) {
                if (subj.Department.Equals(dep)) {
                    subjectsToRemove.Add(subj);
                }
            }

            foreach (Subject s in subjectsToRemove) {
                RemoveSubjectCompletely(s);
            }
        }

        private void RemoveClassroomSoftwareCompletely(ClassroomSoftware selectedItem)
        {
            classroomSoft.Remove(selectedItem);

            foreach (Classroom cls in classrooms) {
                if (cls.InstalledSoftware.Contains(selectedItem)) {
                    cls.InstalledSoftware.Remove(selectedItem);
                }
            }



            foreach (Subject subj in subjects) {
                if (subj.SoftwareRequired.Contains(selectedItem)) {
                    subj.SoftwareRequired.Remove(selectedItem);
                }
            }
        }

        private void RemoveClassroomCompletely(Classroom selectedItem)
        {
            selectedItem.Terms.Clear();
            classrooms.Remove(selectedItem);
        }
    }

   

}

