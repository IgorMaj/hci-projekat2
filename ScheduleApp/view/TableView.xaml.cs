using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for TableView.xaml
    /// </summary>
    public partial class TableView : UserControl
    {

        public controller.Application application = MainWindow.GetApplication();
        private Dictionary<string, Object> options = new Dictionary<string, object>();
        public ObservableCollection<string> optionList = new ObservableCollection<string>();
        private List<string> disallowedColumnNames = new List<String>();
        

        public TableView()
        {
            InitializeComponent();
            DataContext = this;
            initOptions();

        }

        private void initOptions() {
            
            options["Classrooms"] = application.classrooms;
            options["Departments"] = application.departments;
            options["Classroom software"] = application.clasroomSoft;
            options["Subjects"] = application.subjects;
            disallowedColumnNames.Add("Error");
            disallowedColumnNames.Add("Terms");
            disallowedColumnNames.Add("InstalledSoftware");
            disallowedColumnNames.Add("SoftwareRequired");
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid == null) { return; }
            
            string arg = ((ComboBoxItem)selectCollection.SelectedItem).Content.ToString();
            object collection = options[arg];
            dataGrid.ItemsSource = (IEnumerable<Object>)collection;
            
            
           
        }

        

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (disallowedColumnNames.Contains(propertyDescriptor.DisplayName))
            {
                e.Cancel = true;
            }
        }
    }
}
