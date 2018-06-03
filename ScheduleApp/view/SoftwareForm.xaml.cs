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
    /// Interaction logic for SoftwareForm.xaml
    /// </summary>
    public partial class SoftwareForm : Window
    {
        public ClassroomSoftware Software;
        public MainWindow parent;
        private int _noOfErrorsOnScreen = 0;

        public SoftwareForm(MainWindow parent)
        {
            this.parent = parent;
            Software = new ClassroomSoftware();
            InitializeComponent();
            OsCb.ItemsSource = Enum.GetValues(typeof(ClassroomOS)).Cast<ClassroomOS>();
            OsCb.SelectedIndex = 0;
            grid.DataContext = Software;
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

        private void AddSoftware_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _noOfErrorsOnScreen == 0;
            e.Handled = true;
        }

        private void AddSoftware_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Software.Label = in_label.Text;
            Software.Description = in_description.Text;
            Software.Name = in_name.Text;
            Software.Price = Double.Parse(in_price.Text);
            Software.Site = in_site.Text;
            Software.Vendor = in_vendor.Text;
            Software.OS = (ClassroomOS)OsCb.SelectedValue;
            DateTime dt;
            DateTime.TryParse(in_date.Text, out dt);
            Software.YearOfPublication = dt;
            parent.application.clasroomSoft.Add(Software);
            parent.application.writeData();
            e.Handled = true;
            Close();
        }

    }
}
