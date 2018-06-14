using ScheduleApp.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScheduleApp.view
{
    /// <summary>
    /// Interaction logic for MainScheduleView.xaml
    /// </summary>
    public partial class MainScheduleView : UserControl
    {
        private ObservableCollection<Subject> availableSubjects;
        public ObservableCollection<Classroom> Classrooms { get; set; }
        public ObservableCollection<Subject> AvailableSubjects { get { return availableSubjects; }
            set {
                availableSubjects = value;
                
            }
        }

        public ObservableCollection<Subject> AllSubjects { get; set; }

        public MainScheduleView()
        {
            InitializeComponent();
            DataContext = this;
            var container = MainWindow.GetApplication();
            Classrooms = container.classrooms;
            AllSubjects = container.subjects;
            classroomPick.ItemsSource = Classrooms;
            SetupSubjects();
            HelpProvider.SetHelpKey(this, "raspored");


        }

        private ObservableCollection<Subject> FilterSubjects(ObservableCollection<Subject> subjects)
        {
            ObservableCollection<Subject> retVal = new ObservableCollection<Subject>();

            foreach (Subject sub in subjects){
                if (ClassroomSupportsSubject(sub)) {
                    retVal.Add(sub);
                }
            }

            return retVal;
        }

        private bool CheckProperty(bool ClassroomProperty, bool SubjectProperty) {
            //ako predmet zahteva nesto sto ucionica nema  onda vracamo da ucionica ne podrzava ;)
            if (SubjectProperty && !ClassroomProperty) {
                return false;
            }
            //inace vracamo true
            return true;
        }

        private bool ClassroomSupportsSubject(Subject sub)
        {
            if (termsView.ChosenClassroom == null) {
                return false;
            }
            var classroom = termsView.ChosenClassroom;
            return CheckProperty(classroom.HasProjector, sub.ProjectorRequired) &&
            CheckProperty(classroom.HasTable, sub.TableRequired) &&
            CheckProperty(classroom.HasSmartTable, sub.SmartTableRequired) && 
            CheckOS(classroom.OS, sub.OSRequired) && CheckSoftware(classroom.InstalledSoftware,sub.SoftwareRequired);
            
        }

        private bool CheckSoftware(List<ClassroomSoftware> installedSoftware, List<ClassroomSoftware> softwareRequired)
        {
            foreach (ClassroomSoftware soft in softwareRequired) {
                if (!installedSoftware.Contains(soft)) {
                    return false;
                }
            }

            return true;
        }

        private bool CheckOS(ClassroomOS oS, ClassroomOS oSRequired)
        {
            if (oS == oSRequired || oS == ClassroomOS.WINDOWS_AND_LINUX) {
                return true;
            }
            return false;
        }

        private void SubjectsView_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Package the data.
                var subject = (Subject)((ListView)sender).SelectedItem;
                if (subject == null) { return; }
                DataObject data = new DataObject();
                data.SetData("Object", subject);
                data.SetData("ChosenClassroom",termsView.ChosenClassroom);

                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
            }
        }

        private void SetupSubjects() {

            if (classroomPick.SelectedItem != null)
            {
                termsView.ChosenClassroom = (Classroom)classroomPick.SelectedItem;
                AvailableSubjects = FilterSubjects(AllSubjects);
                SubjectsView.ItemsSource = AvailableSubjects;
                SubjectsView.DataContext = AvailableSubjects;
            }
        }

        private void classroomPick_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetupSubjects();
        }

        public void DeleteTerm(object sender, ExecutedRoutedEventArgs e)
        {
            TilesUtil.DeleteSelectedTile();
        }
    }
}
