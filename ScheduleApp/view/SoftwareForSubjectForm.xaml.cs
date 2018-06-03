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
    /// Interaction logic for SoftwareForSubjectForm.xaml
    /// </summary>
    public partial class SoftwareForSubjectForm : Window
    {
        SubjectForm parent;

        public SoftwareForSubjectForm(SubjectForm parent)
        {
            this.parent = parent;
            InitializeComponent();

            foreach (ClassroomSoftware soft in parent.parent.application.clasroomSoft)
            {
                dg_all.Items.Add(soft);
            }
            foreach (ClassroomSoftware soft in parent.Subject.SoftwareRequired)
            {
                dg_subject.Items.Add(soft);
            }
            dg_subject.Items.Refresh();
            dg_all.Items.Refresh();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ClassroomSoftware software = (ClassroomSoftware)dg_all.SelectedItem;
            if (!parent.Subject.SoftwareRequired.Contains(software) && software != null)
            {
                parent.Subject.SoftwareRequired.Add(software);
                dg_subject.Items.Add(software);
                dg_subject.Items.Refresh();
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            ClassroomSoftware software = (ClassroomSoftware)dg_subject.SelectedItem;
            if (software != null)
            {
                parent.Subject.SoftwareRequired.Remove(software);
                dg_subject.Items.Remove(software);
                dg_subject.Items.Refresh();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
