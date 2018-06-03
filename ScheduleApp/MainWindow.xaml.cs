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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                TilesUtil.DeleteSelectedTile();
                e.Handled = true;
            }
        }

        private void ClassroomForm_Click(object sender, RoutedEventArgs e)
        {
            ClassroomForm classroomForm = new ClassroomForm(this);
            
            Title = "Classroom form";
            classroomForm.Activate();
            classroomForm.Focus();
            classroomForm.ShowDialog();
            //ChangeElement(ClassroomForm);
        }

        private void SoftwareForm_Click(object sender, RoutedEventArgs e)
        {
            SoftwareForm softwareForm = new SoftwareForm(this);
            Title = "Software form";
            //softwareForm.Activate();
            //softwareForm.Focus();
            //softwareForm.ShowDialog();
            ChangeElement(softwareForm);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainScheduleView scheduleView = new MainScheduleView();
            Title = "Schedule view";
            ChangeElement(scheduleView);
            
            
        }

        private void ChangeElement(UIElement element) {
            mainImage.Visibility = Visibility.Collapsed;
            if (currentMainElement != null) {
              grid.Children.Remove(currentMainElement);
            }
            Grid.SetRow(element, 1);
            grid.Children.Add(element);
            currentMainElement = element;
        }
    }
}
