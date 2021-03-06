﻿using ScheduleApp.model;
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
        public MainWindow parent;
	    private List<string> disallowedColumnNames = new List<String>();

        public TableView(MainWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            DataContext = this;
            initOptions();
            dataGrid_SelectionChanged(null, null);
            HelpProvider.SetHelpKey(this, "tabela");
        }

        private void initOptions() {
            
            options["Ucionice"] = application.classrooms;
            options["Departmani"] = application.departments;
            options["Softver"] = application.classroomSoft;
            options["Predmeti"] = application.subjects;
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

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dataGrid.SelectedItem;
            if (selectedItem != null)
            {
                if (MessageBox.Show("Ovaj entitet će biti obrisan. Takođe će biti obrisani svi oni koji su povezani s njim.", "Pitanje", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    //var selectedItem = dataGrid.SelectedItem;
                    application.RemoveCompletely(selectedItem);
                }
            }
            else
                MessageBox.Show("Niste odabrali entitet koji želite da obrišete.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid == null) { return; }
            initOptions();
            if (selectCollection.SelectedItem == null) {
                MessageBoxResult result = MessageBox.Show("Niste odabrali podatke za prikaz.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);

                return;

            }
            string arg = ((ComboBoxItem)selectCollection.SelectedItem).Content.ToString();
            
            switch (arg)
            {
                case "Ucionice":
                    {
                        Classroom classroom = (Classroom)dataGrid.SelectedItem;
                        if (classroom != null)
                        {
                            ClassroomForm classroomForm = new ClassroomForm(parent, classroom, this);
                            parent.Title = "Izmena učionice";
                            parent.ChangeElement(classroomForm);
                        }
                        else
                        {
                            MessageBoxResult result = MessageBox.Show("Niste odabrali učionicu iz tabele.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    break;
                case "Smerovi":
                    Department department = (Department)dataGrid.SelectedItem;
                    if (department != null)
                    {
                        DepartmentForm departmentForm = new DepartmentForm(parent, department, this);
                        parent.Title = "Izmena departmana";
                        parent.ChangeElement(departmentForm);
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("Niste odabrali departman iz tabele.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    break;
                case "Softver":
                    ClassroomSoftware classroomSoftware = (ClassroomSoftware)dataGrid.SelectedItem;
                    if (classroomSoftware != null)
                    {
                        SoftwareForm softwareForm = new SoftwareForm(parent, classroomSoftware, this);
                        parent.Title = "Izmena softvera";
                        parent.ChangeElement(softwareForm);
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("Niste odabrali softver iz tabele.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    break;
                case "Predmeti":
                    Subject subject = (Subject)dataGrid.SelectedItem;
                    if (subject != null)
                    {
                        SubjectForm subjectForm = new SubjectForm(parent, subject, this);
                        parent.Title = "Izmena predmeta";
                        parent.ChangeElement(subjectForm);
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("Niste odabrali predmet iz tabele.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    break;
            }
        }

    }
}
