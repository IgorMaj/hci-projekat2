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

        public MainScheduleView()
        {
            InitializeComponent();
            DataContext = this;
            var container = MainWindow.GetApplication();
            Classrooms = container.classrooms;
            AvailableSubjects = container.subjects;
            classroomPick.ItemsSource = Classrooms;
            SubjectsView.ItemsSource = AvailableSubjects;
            SubjectsView.DataContext = AvailableSubjects;
            if (classroomPick.SelectedItem != null)
            {
                termsView.ChosenClassroom = (Classroom)classroomPick.SelectedItem;
            }
            
        }

        private void SubjectsView_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Package the data.
                var subject = (Subject)((ListView)sender).SelectedItem;
                DataObject data = new DataObject();
                data.SetData("Object", subject);
                data.SetData("ChosenClassroom",termsView.ChosenClassroom);

                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
            }
        }

        private void classroomPick_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (classroomPick.SelectedItem != null)
            {
                termsView.ChosenClassroom = (Classroom)classroomPick.SelectedItem;
            }
        }

        public void DeleteTerm(object sender, ExecutedRoutedEventArgs e)
        {
            TilesUtil.DeleteSelectedTile();
        }
    }
}
