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

namespace ScheduleApp.view
{
    /// <summary>
    /// Interaction logic for SoftwareForClassroomForm.xaml
    /// </summary>
    /// 
    public partial class SoftwareForClassroomForm : Window
    {

        ClassroomForm parent;

        public SoftwareForClassroomForm(ClassroomForm parent)
        {
            this.parent = parent;
            InitializeComponent();
            foreach (ClassroomSoftware soft in  MainWindow.GetApplication().classroomSoft){
                dg_all.Items.Add(soft);
            }
            foreach (ClassroomSoftware soft in parent.Classroom.InstalledSoftware)
            {
                dg_classroom.Items.Add(soft);
            }
            dg_classroom.Items.Refresh();
            dg_all.Items.Refresh();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ClassroomSoftware software = (ClassroomSoftware)dg_all.SelectedItem;
            if (!parent.Classroom.InstalledSoftware.Contains(software) && software != null)
            {
                parent.Classroom.InstalledSoftware.Add(software);
                dg_classroom.Items.Add(software);
                dg_classroom.Items.Refresh();
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            ClassroomSoftware software = (ClassroomSoftware)dg_classroom.SelectedItem;
            if (software != null)
            {
                parent.Classroom.InstalledSoftware.Remove(software);
                dg_classroom.Items.Remove(software);
                dg_classroom.Items.Refresh();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
