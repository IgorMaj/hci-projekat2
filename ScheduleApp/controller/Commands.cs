using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScheduleApp.controller
{
    public class Commands
    {
        public static RoutedCommand DeleteCommand { get; set; }
        public static RoutedCommand TableViewEditCommand {get; set;}

        public static RoutedCommand NewSubjectMenuCommand { get; set; }
        public static RoutedCommand NewSoftwareMenuCommand { get; set; }
        public static RoutedCommand NewDepartmentMenuCommand { get; set; }
        public static RoutedCommand NewCLassroomMenuCommand { get; set; }

        //ovde ovako mozete dodati svoje komande i specifisati im precice ima i ovo ModifierKeys.Control da se ubaci
        static Commands() {
            DeleteCommand = new RoutedCommand();
            DeleteCommand.InputGestures.Add(new KeyGesture(Key.Delete));

            TableViewEditCommand = new RoutedCommand();
            TableViewEditCommand.InputGestures.Add(new KeyGesture(Key.E,ModifierKeys.Control));

            NewCLassroomMenuCommand = new RoutedCommand();
            NewCLassroomMenuCommand.InputGestures.Add(new KeyGesture(Key.U,ModifierKeys.Alt));

            NewSoftwareMenuCommand = new RoutedCommand();
            NewSoftwareMenuCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Alt));

            NewDepartmentMenuCommand = new RoutedCommand();
            NewDepartmentMenuCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Alt));

            NewSubjectMenuCommand = new RoutedCommand();
            NewSubjectMenuCommand.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Alt));


        }
    }
}
