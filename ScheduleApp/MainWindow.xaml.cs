using ScheduleApp.model;
using ScheduleApp.view;
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

namespace ScheduleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public controller.Application application = new controller.Application();

        public UIElement currentMainElement = null;

        public MainWindow()
        {
            InitializeComponent();
            application = application.loadData();
            //JSONUtil.CreateDummyData("data.json");

        }

        public static controller.Application GetApplication() {
            return ((MainWindow)Application.Current.MainWindow).application;
        }


        private void ClassroomForm_Click(object sender, RoutedEventArgs e)
        {
            ClassroomForm classroomForm = new ClassroomForm(this);
            Title = "Classroom form";
            ChangeElement(classroomForm);
        }

        private void SoftwareForm_Click(object sender, RoutedEventArgs e)
        {
            SoftwareForm softwareForm = new SoftwareForm(this);
            Title = "Software form";
            ChangeElement(softwareForm);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainScheduleView scheduleView = new MainScheduleView();
            Title = "Prikaz rasporeda";
            ChangeElement(scheduleView);
            
            
        }

        public void ChangeElement(UIElement element) {
            mainImage.Visibility = Visibility.Collapsed;
            if (currentMainElement != null) {
              grid.Children.Remove(currentMainElement);
            }
            Grid.SetRow(element, 1);
            grid.Children.Add(element);
            currentMainElement = element;
        }

        private void Tables_Click_1(object sender, RoutedEventArgs e)
        {
            TableView scheduleView = new TableView(this);
            Title = "Tabelarni prikaz";
            ChangeElement(scheduleView);
        }

        public void returnOriginal()
        {
            mainImage.Visibility = Visibility.Visible;
            if (currentMainElement != null)
            {
                grid.Children.Remove(currentMainElement);
            }
        }

        public void returnTable(TableView scheduleView)
        {
            mainImage.Visibility = Visibility.Collapsed;
            if (currentMainElement != null)
            {
                grid.Children.Remove(currentMainElement);
            }
            Grid.SetRow(scheduleView, 1);
            grid.Children.Add(scheduleView);
            currentMainElement = scheduleView;
            scheduleView.dataGrid.Items.Refresh();
        }

        private void DepartmentForm_Click(object sender, RoutedEventArgs e)
        {
            DepartmentForm departmentForm = new DepartmentForm(this);
            Title = "Department view";
            ChangeElement(departmentForm);
        }

        private void SubjectForm_Click(object sender, RoutedEventArgs e)
        {
            if (application.departments.Count > 0)
            {
                SubjectForm subjectForm = new SubjectForm(this);
                Title = "Subject view";
                ChangeElement(subjectForm);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Can't create subjects beacause you don't have any departments.", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
