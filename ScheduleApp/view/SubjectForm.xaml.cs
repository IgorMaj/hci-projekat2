using ScheduleApp.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ScheduleApp.view
{
    /// <summary>
    /// Interaction logic for SubjectForm.xaml
    /// </summary>
    public partial class SubjectForm : UserControl
    {
        public Subject Subject;
        public MainWindow parent;
        private int _noOfErrorsOnScreen = 0;
        string action;
        TableView scheduleView;

        public SubjectForm(MainWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            Subject = new Subject();
            Subject.SoftwareRequired = new List<ClassroomSoftware>();
            DepCb.ItemsSource = parent.application.departments;
            DepCb.SelectedIndex = 0;
            action = "added";
            grid.DataContext = Subject;
        }

        public SubjectForm(MainWindow parent, Subject Subject, TableView scheduleView)
        {
            this.parent = parent;
            this.scheduleView = scheduleView;
            InitializeComponent();
            this.Subject = Subject;
            DepCb.ItemsSource = parent.application.departments;
            DepCb.SelectedIndex = 0;
            for (int i = 0; i < DepCb.Items.Count; i++)
            {
                if (((Department)DepCb.Items[i]) == Subject.Department)
                {
                    DepCb.SelectedIndex = i;
                    break;
                }
            }
            in_label.Text = Subject.Label;
            in_name.Text = Subject.Name;
            in_description.Text = Subject.Description;
            in_groupSize.Text = Subject.GroupSize.ToString();
            in_minLength.Text = Subject.MinimalTermLength.ToString();
            in_numberRequiredTerms.Text = Subject.NumRequiredTerms.ToString();
            check_projector.IsChecked = Subject.ProjectorRequired;
            check_table.IsChecked = Subject.TableRequired;
            check_smart.IsChecked = Subject.SmartTableRequired;
            action = "edited";
            grid.DataContext = Subject;
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

        private void AddSubject_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _noOfErrorsOnScreen == 0;
            e.Handled = true;
        }

        private void AddSubject_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Subject.Label = in_label.Text;
            Subject.Name = in_name.Text;
            Subject.Department = (Department)DepCb.SelectedValue;
            Subject.Description = in_description.Text;
            Subject.GroupSize = Int32.Parse(in_groupSize.Text);
            Subject.MinimalTermLength = Int32.Parse(in_minLength.Text);
            Subject.NumRequiredTerms = Int32.Parse(in_numberRequiredTerms.Text);
            Subject.ProjectorRequired = (bool)check_projector.IsChecked;
            Subject.TableRequired = (bool)check_table.IsChecked;
            Subject.SmartTableRequired = (bool)check_smart.IsChecked;
            Subject.OSRequired = ClassroomOS.WINDOWS; // za slucaj ako nema softwera
            bool Windows = false;
            bool Linux = false;
            foreach (ClassroomSoftware software in  Subject.SoftwareRequired)
            {
                if(software.OS == ClassroomOS.WINDOWS)
                {
                    Windows = true;
                }
                if (software.OS == ClassroomOS.LINUX)
                {
                    Linux = true;
                }
                if (software.OS == ClassroomOS.WINDOWS_AND_LINUX)
                {
                    Windows = true;
                    Linux = true;
                }
            }
            if (Windows && !Linux) {
                Subject.OSRequired = ClassroomOS.WINDOWS;
            }
            if (!Windows && Linux)
            {
                Subject.OSRequired = ClassroomOS.LINUX;
            }
            if (Windows && Linux)
            {
                Subject.OSRequired = ClassroomOS.WINDOWS_AND_LINUX;
            }
            if (action.Equals("added"))
            {
                parent.application.subjects.Add(Subject);
                Clear();
                MessageBoxResult result = MessageBox.Show("Successfully added subject.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                controller.CustomEvents.RaiseTutorialStepCompletedEvent(Steps.SUBJECT_ADDED);
            }
            else
            {
                parent.returnTable(scheduleView);
            }
            parent.application.writeData();
            e.Handled = true;

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            SoftwareForSubjectForm sofForClass = new SoftwareForSubjectForm(this);
            sofForClass.Activate();
            sofForClass.Focus();
            sofForClass.ShowDialog();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (action.Equals("added"))
            {
                parent.returnOriginal();
            }
            else
            {
                parent.returnTable(scheduleView);
            }
        }

        private void Clear()
        {
            Subject = new Subject();
            Subject.SoftwareRequired = new List<ClassroomSoftware>();
            DepCb.ItemsSource = parent.application.departments;
            DepCb.SelectedIndex = 0;
            grid.DataContext = Subject;

            in_label.Text = "";
            in_description.Text = "";
            in_name.Text = "";
            in_groupSize.Text = "0";
            in_minLength.Text = "0";
            in_numberRequiredTerms.Text = "0";

            check_projector.IsChecked = false;
            check_table.IsChecked = false;
            check_smart.IsChecked = false;
        }
    }
}
