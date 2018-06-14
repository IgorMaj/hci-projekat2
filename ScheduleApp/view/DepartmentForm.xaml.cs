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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScheduleApp.view
{
    /// <summary>
    /// Interaction logic for Class1.xaml
    /// </summary>
    public partial class DepartmentForm : UserControl
    {
        public Department department;
        public MainWindow parent;
        private int _noOfErrorsOnScreen = 0;
        string action;
        TableView scheduleView;

        public DepartmentForm(MainWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            department = new Department();
            action = "added";
            grid.DataContext = department;
            HelpProvider.SetHelpKey(this, "dodavanje_sadrzaja");
        }
        public DepartmentForm(MainWindow parent, Department department, TableView scheduleView)
        {
            this.parent = parent;
            this.scheduleView = scheduleView;
            InitializeComponent();
            this.department = department;
            in_label.Text = department.Label;
            in_description.Text = department.Description;
            in_name.Text = department.Name;
            in_date.Text = department.DateOfIntroduction.ToString();
            action = "edited";
            grid.DataContext = department;
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                _noOfErrorsOnScreen++;
            else
                _noOfErrorsOnScreen--;
        }

        private void AddDepartment_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _noOfErrorsOnScreen == 0;
            e.Handled = true;
        }

        private void AddDepartment_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            department.Label = in_label.Text;
            department.Description = in_description.Text;
            department.Name = in_name.Text;
            DateTime dt;
            DateTime.TryParse(in_date.Text, out dt);
            department.DateOfIntroduction = dt;
            if (action.Equals("added"))
            {
                parent.application.departments.Add(department);
                Clear();
                MessageBoxResult result = MessageBox.Show("Uspešno ste dodali departman.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                parent.returnTable(scheduleView);
            }
            parent.application.writeData();
            e.Handled = true;
            controller.CustomEvents.RaiseTutorialStepCompletedEvent(Steps.DEPARTMENT_ADDED);
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
            department = new Department();
            grid.DataContext = department;

            in_label.Text = "";
            in_description.Text = "";
            in_name.Text = "";
            in_date.Text = "";
        }
    }
}
