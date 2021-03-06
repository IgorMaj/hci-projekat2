﻿using ScheduleApp.model;
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
    /// Interaction logic for ClassroomForm.xaml
    /// </summary>
    public partial class ClassroomForm : UserControl
    {
        public Classroom Classroom;
        public MainWindow parent;
        private int _noOfErrorsOnScreen = 0;
        string action;
        TableView scheduleView;

        public ClassroomForm(MainWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            Classroom = new Classroom();
            Classroom.InstalledSoftware = new List<ClassroomSoftware>();
            OsCb.ItemsSource = Enum.GetValues(typeof(ClassroomOS)).Cast<ClassroomOS>();
            OsCb.SelectedIndex = 0;
            action = "added";
            grid.DataContext = Classroom;
            HelpProvider.SetHelpKey(this, "dodavanje_sadrzaja");

        }

        public ClassroomForm(MainWindow parent, Classroom Classroom, TableView scheduleView)
        {
            this.parent = parent;
            this.scheduleView = scheduleView;
            InitializeComponent();
            this.Classroom = Classroom;
            OsCb.ItemsSource = Enum.GetValues(typeof(ClassroomOS)).Cast<ClassroomOS>();
            OsCb.SelectedIndex = 0;
            for (int i=0; i< OsCb.Items.Count; i++)
            {
                if (((ClassroomOS)OsCb.Items[i]) == Classroom.OS)
                {
                    OsCb.SelectedIndex = i;
                    break;
                }
            }
            in_label.Text = Classroom.Label;

            in_description.Text = Classroom.Description;
            in_employees.Text = Classroom.NumOfEmployees.ToString();
            check_projector.IsChecked = Classroom.HasProjector;
            check_table.IsChecked = Classroom.HasTable;
            check_smart.IsChecked = Classroom.HasSmartTable;
            action = "edited";
            grid.DataContext = Classroom;

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

        private void AddClassroom_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _noOfErrorsOnScreen == 0;
            e.Handled = true;
        }

        private void AddClassroom_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Classroom.Label = in_label.Text;

            if (!MainWindow.GetApplication().CheckUniqueConstraint(Classroom.Label))
            {
                MessageBox.Show("Entitet nema jedinstvenu oznaku na nivou sistema.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Classroom.Description = in_description.Text;
            Classroom.NumOfEmployees = Int32.Parse(in_employees.Text);
            Classroom.HasProjector = (bool)check_projector.IsChecked;
            Classroom.HasTable = (bool)check_table.IsChecked;
            Classroom.HasSmartTable = (bool)check_smart.IsChecked;
            Classroom.OS = (ClassroomOS)OsCb.SelectedValue;

            if (action.Equals("added"))
            {
                parent.application.classrooms.Add(Classroom);
                Clear();
                MessageBoxResult result = MessageBox.Show("Uspešno ste dodali učionicu.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                parent.returnTable(scheduleView);
            }
            parent.application.writeData();
            e.Handled = true;
            controller.CustomEvents.RaiseTutorialStepCompletedEvent(Steps.CLASSROOM_ADDED);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            SoftwareForClassroomForm sofForClass = new SoftwareForClassroomForm(this);
            sofForClass.Activate();
            sofForClass.Focus();
            sofForClass.ShowDialog();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (action.Equals("added"))
            {
                parent.returnOriginal();
            }
            else
            {
                parent.returnTable(scheduleView);
            }
        }

        private void Clear()
        {
            Classroom = new Classroom();
            Classroom.InstalledSoftware = new List<ClassroomSoftware>();
            OsCb.ItemsSource = Enum.GetValues(typeof(ClassroomOS)).Cast<ClassroomOS>();
            OsCb.SelectedIndex = 0;
            grid.DataContext = Classroom;

            in_label.Text = "";
            in_description.Text = "";
            in_employees.Text = "0";
            check_projector.IsChecked = false;
            check_table.IsChecked = false;
            check_smart.IsChecked = false;
        }
    }
}
