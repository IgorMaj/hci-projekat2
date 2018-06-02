using ScheduleApp.model;
using ScheduleApp.repository;
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
        public ObservableCollection<Classroom> Classrooms { get; set; }

        public MainScheduleView()
        {
            InitializeComponent();
            DataContext = this;
            classroomPickButton.IsEnabled = false;
           
        }

        

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.DefaultExt = "";
            dlg.Filter = "JSON Files (*.json)|*.json";
            if (dlg.ShowDialog().Equals(System.Windows.Forms.DialogResult.OK)) {
                string filename = dlg.FileName;
                pathTextBox.Text = filename;
                JSONUtil.Path = filename;
            }
                
           
            
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            
            Classrooms = JSONUtil.LoadClassrooms(pathTextBox.Text);
            classroomPick.ItemsSource = Classrooms;
            classroomPickButton.IsEnabled = true;
            JSONUtil.Obj = Classrooms;

        }

        private void classroomPickButton_Click(object sender, RoutedEventArgs e)
        {
            if (classroomPick.SelectedItem != null) {
                termsView.ChosenClassroom = (Classroom)classroomPick.SelectedItem;
            }
        }

        
    }
}
