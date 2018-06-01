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
    /// Interaction logic for WeekDayView.xaml
    /// </summary>
    public partial class WeekDayView : UserControl
    {
        private List<string> days = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday","Friday","Saturday"};
        private Tile firstTile = new Tile() { TileText = "Days:"};
        public WeekDayView()
        {
            InitializeComponent();
            DayTileWidth = 150;
            firstTile.Width = DayTileWidth;
            stack.Children.Add(firstTile);
            foreach(string day in days){
                stack.Children.Add(new Tile() { TileText=day,TileStrokeColor="Gray",Width=DayTileWidth,TileColor= "#E8E8E8"});
            }

        }

        public int DayTileHeight { get; set; }
        public int DayTileWidth { get; set; }
        public double FirstTileWidth { get { return firstTile.Width; } }
    }
}
