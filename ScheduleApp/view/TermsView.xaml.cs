using ScheduleApp.model;
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
    /// Interaction logic for TermsView.xaml
    /// </summary>
    public partial class TermsView : UserControl
    {
        private ObservableCollection<Term> terms;
        private List<string> timeList = new List<string>();
        private int numWorkingDays = 6;
        private int numPossibleTimes = 20;
        
        public TermsView()
        {
            InitializeComponent();
            terms = new ObservableCollection<Term>(); //ovo ce biti povezano s ucionicom!
            terms.Add(new Term() { Day = WEEKDAY.MONDAY, Time = new TimeSpan(7,45,0),Subject = new Subject() {Label = "P1" } });
            terms.Add(new Term() { Day = WEEKDAY.TUESDAY, Time = new TimeSpan(8, 30, 0), Subject = new Subject() { Label = "P2" } });
            terms.Add(new Term() { Day = WEEKDAY.WEDNESDAY, Time = new TimeSpan(9, 15, 0), Subject = new Subject() { Label = "P3" } });
            terms.Add(new Term() { Day = WEEKDAY.THURSDAY, Time = new TimeSpan(9, 15, 0), Subject = new Subject() { Label = "P4" } });
            DataContext = terms;
            DrawGrid();
            FillGridWithTerms();
        }

       

        
        public void CreateTimelineView()
        {
            InitializeComponent();
            GenerateTimeList();
            int i = 0;
            foreach (string time in timeList)
            {
                var tile = new Tile() { TileColor = "#E8E8E8", TileStrokeColor = "Gray", TileText = time };
                Grid.SetRow(tile,i);
                Grid.SetColumn(tile,0);
                grid.Children.Add(tile);
                i++;
            }


        }

        private string AddZero(int timeNum)
        {
            if (timeNum < 10)
            {
                return "0" + timeNum;
            }
            return timeNum + "";
        }

        private void GenerateTimeList()
        {
            TimeSpan startSpan = new TimeSpan(7, 0, 0);
            TimeSpan endSpan = new TimeSpan(22, 0, 0);
            do
            {
                timeList.Add(AddZero(startSpan.Hours) + ":" + AddZero(startSpan.Minutes));
                startSpan = startSpan.Add(new TimeSpan(0, 45, 0));
            } while (TimeSpan.Compare(startSpan, endSpan) != 0);

            timeList.Add(AddZero(endSpan.Hours) + ":" + AddZero(endSpan.Minutes));

        }
    

    private void DrawGrid()
        {
           
            for (int i = 0; i < numWorkingDays+1; i++) {
                
                grid.ColumnDefinitions.Add(new ColumnDefinition() {MinWidth=150,MaxWidth=150});
            }
            for (int j = 0; j < numPossibleTimes+1; j++) {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            CreateTimelineView();
            for (int i = 0; i < numPossibleTimes+1; i++) {
                for (int j = 1; j < numWorkingDays+1; j++) {
                    var tile = new Tile() { TileText = "", TileStrokeColor = "Gray" };
                    Grid.SetRow(tile,i);
                    Grid.SetColumn(tile,j);
                    tile.DropAllowed = true;
                    grid.Children.Add(tile);
                }
            }
           
            
        }


        private UIElement GetChild(Tuple<int,int> position)
        {
            foreach (UIElement child in grid.Children)
            {
                if (Grid.GetRow(child) == position.Item1
                      &&
                   Grid.GetColumn(child) == position.Item2)
                {
                    return child;
                }
            }

            return null;
        }

        private void FillGridWithTerms()
        {
            foreach(Term t in terms) {
                Tuple<int, int> position = CalculatePosition(t);
                var tile = new TermTile() { Term = t, TileText = t.Subject.Label };
                Grid.SetColumn(tile, position.Item2);
                Grid.SetRow(tile, position.Item1);
                grid.Children.Add(tile);
                tile.TileColor = "Yellow";
            }
        }

        private Tuple<int, int> CalculatePosition(Term t)
        {
            int timeCounter = 0;
            TimeSpan startTime = new TimeSpan(7,0,0);
            
            while (TimeSpan.Compare(startTime, t.Time) != 0) {
                startTime = startTime.Add(new TimeSpan(0,45,0));
                timeCounter++;
            }
            Tuple<int, int> retVal = new Tuple<int, int>(timeCounter, (int)t.Day+1);
            return retVal;
        }

       

        
    }
}
