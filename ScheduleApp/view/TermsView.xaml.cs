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
        
        private List<string> timeList = new List<string>();
        private int numWorkingDays = 6; //neophodno zbog grida
        private int numPossibleTimes = 60; //isto

        private TimeSpan startSpan = new TimeSpan(7,0,0);
        private TimeSpan incrementSpan = new TimeSpan(0, 15, 0);
        private TimeSpan endSpan = new TimeSpan(22,0,0);

        private Classroom chosenClassroom;
        
        public Classroom ChosenClassroom { get { return chosenClassroom; }
            set {
                chosenClassroom = value;
                ClearGridOfTerms();
                FillGridWithTerms();
            } }

        private void ClearGridOfTerms()
        {
            List<TermTile> childrenToRemove = new List<TermTile>();

            foreach (UIElement child in grid.Children) {
                if (child is Tile)
                {
                    ((Tile)child).Taken = null;
                }
                else if (child is TermTile) {
                    childrenToRemove.Add((TermTile)child);
                }

            }

            foreach(TermTile tTile in childrenToRemove) {
                grid.Children.Remove(tTile);
            }
        }

        public TermsView()
        {
            InitializeComponent();

            ChosenClassroom = new Classroom() { Terms = new List<Term>() };
            DataContext = ChosenClassroom;
            DrawGrid();
            
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
            TimeSpan startTime = startSpan;
            TimeSpan endTime = endSpan;
            do
            {
                timeList.Add(AddZero(startTime.Hours) + ":" + AddZero(startTime.Minutes));
                startTime = startTime.Add(incrementSpan);
            } while (TimeSpan.Compare(startTime, endTime) != 0);

            //timeList.Add(AddZero(endTime.Hours) + ":" + AddZero(endTime.Minutes));

        }
    

    private void DrawGrid()
        {
           
            for (int i = 0; i < numWorkingDays+1; i++) {
                
                grid.ColumnDefinitions.Add(new ColumnDefinition() {MinWidth=150,MaxWidth=150});
            }
            for (int j = 0; j < numPossibleTimes; j++) {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            CreateTimelineView();
            for (int i = 0; i < numPossibleTimes; i++) {
                for (int j = 1; j < numWorkingDays+1; j++) {
                    var tile = new Tile() { TileText = "", TileStrokeColor = "Gray" };
                    Grid.SetRow(tile,i);
                    Grid.SetColumn(tile,j);
                    tile.DropAllowed = true;
                    grid.Children.Add(tile);
                }
            }
           
            
        }


        

        

        private void FillGridWithTerms()
        {
            foreach(Term t in ChosenClassroom.Terms) {
                Tuple<int, int> position = CalculatePosition(t);

                var tile = new TermTile() { Term = t, TileText = t.Subject.Label };
                Grid.SetColumn(tile, position.Item2);
                Grid.SetRow(tile, position.Item1);
                Grid.SetRowSpan(tile,t.Length*3);
                var coordinates = TilesUtil.GetAllTermCoordinates(position.Item1,position.Item2,Grid.GetRowSpan(tile));
                var tiles = TilesUtil.GetTiles(grid, coordinates);
                TilesUtil.MarkTiles(tiles,tile);

                grid.Children.Add(tile);
                tile.TileColor = "Yellow";
            }
        }

        private Tuple<int, int> CalculatePosition(Term t)
        {
            int timeCounter = 0;
            TimeSpan startTime = startSpan;
            
            while (TimeSpan.Compare(startTime, t.Time) != 0) {
                startTime = startTime.Add(incrementSpan);
                timeCounter++;
            }
            Tuple<int, int> retVal = new Tuple<int, int>(timeCounter, (int)t.Day+1);
            return retVal;
        }

       

        
    }
}
