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
        public model.Department department;
        public MainWindow parent;
        private int _noOfErrorsOnScreen = 0;
        public DepartmentForm(MainWindow parent)

        {

            this.parent = parent;
            InitializeComponent();


            department = new model.Department();
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
            parent.application.departments.Add(department);
            parent.application.writeData();
          
            e.Handled = true;
            //Close();
        }

    }
}
