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
    /// Interaction logic for TableView.xaml
    /// </summary>
    public partial class TableView : UserControl
    {

        public controller.Application application = MainWindow.GetApplication();
        private Dictionary<string, Object> options = new Dictionary<string, object>();
        public ObservableCollection<string> optionList = new ObservableCollection<string>();

        

        public TableView()
        {
            InitializeComponent();
            DataContext = this;


        }

        private void initOptions() {
            
            options["Classrooms"] = application.classrooms;
            options["Departments"] = application.departments;
            options["Classroom software"] = application.classroomSoft;
            options["Subjects"] = application.subjects;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid == null) { return; }
            initOptions();
            string arg = ((ComboBoxItem)selectCollection.SelectedItem).Content.ToString();
            object collection = options[arg];
            dataGrid.ItemsSource = (IEnumerable<Object>)collection;
            
            
           
        }
    }
}
