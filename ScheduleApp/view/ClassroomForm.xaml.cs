using ScheduleApp.model;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Text.RegularExpressions;


namespace ScheduleApp.view
{
    /// <summary>
    /// Interaction logic for ClassroomForm.xaml
    /// </summary>
    public partial class ClassroomForm : Window
    {
        public Classroom Classroom;
        public MainWindow parent;
        private int _noOfErrorsOnScreen = 0;

        public ClassroomForm(MainWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            Classroom = new Classroom();
            Classroom.InstalledSoftware = new List<ClassroomSoftware>();
            OsCb.ItemsSource = Enum.GetValues(typeof(ClassroomOS)).Cast<ClassroomOS>();
            OsCb.SelectedIndex = 0;
            grid.DataContext = Classroom;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                _noOfErrorsOnScreen++;
            else
                _noOfErrorsOnScreen--;
        }

        private void AddClassroom_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _noOfErrorsOnScreen == 0;
            e.Handled = true;
        }

        private void AddClassroom_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Classroom.Label = in_label.Text;
            Classroom.Description = in_description.Text;
            Classroom.NumOfEmployees = Int32.Parse(in_employees.Text);
            Classroom.HasProjector = (bool)check_projector.IsChecked;
            Classroom.HasTable = (bool)check_table.IsChecked;
            Classroom.HasSmartTable = (bool)check_smart.IsChecked;
            Classroom.OS = (ClassroomOS)OsCb.SelectedValue;
            parent.application.classrooms.Add(Classroom);
            parent.application.writeData();
            e.Handled = true;
            Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            SoftwareForClassroomForm sofForClass = new SoftwareForClassroomForm(this);
            sofForClass.Activate();
            sofForClass.Focus();
            sofForClass.ShowDialog();
        }
    }
}
