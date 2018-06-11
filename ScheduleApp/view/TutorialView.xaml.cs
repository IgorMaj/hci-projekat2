using ScheduleApp.controller;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScheduleApp.view
{
    /// <summary>
    /// Interaction logic for TutorialView.xaml
    /// </summary>
    public partial class TutorialView : UserControl
    {

        public Tutorial Tutorial { get; set; }

        public TutorialView(Tutorial t)
        {
            InitializeComponent();
            DataContext = this;
            Tutorial = t;
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
            CustomEvents.TutorialStepCompleted += OnTutorialStepCompleted;
            
        }

        private void OnTutorialStepCompleted(object sender, int e)
        {
            if (e == Tutorial.CurrentStepIndex) {
                nextButton.IsEnabled = true;
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
             
            Tutorial.NextStep();
            Title.Content = Tutorial.CurrentStep.Title;
            Content.Text = Tutorial.CurrentStep.Content;
            nextButton.IsEnabled = false;
        }
    }
}
