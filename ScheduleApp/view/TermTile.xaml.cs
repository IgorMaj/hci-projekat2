using ScheduleApp.model;
using ScheduleApp.repository;
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
    /// Interaction logic for TermTile.xaml
    /// </summary>
    public partial class TermTile : UserControl
    {
       
        public TermTile()
        {
            InitializeComponent();
            DataContext = this;
            TileText = "Subject";
            TileColor = "Yellow";
            TileStrokeColor = "White";
            StrokeThickness = 5;
            TextFontSize = 20;
            Focusable = true;

        }

        public TermTile(TermTile other) {
            InitializeComponent();
            DataContext = this;
            TileText = other.TileText;
            TileColor = other.TileColor;
            TileStrokeColor = other.TileStrokeColor;
            StrokeThickness = other.StrokeThickness;
            TextFontSize = other.TextFontSize;
            Term = other.Term;
            Focusable = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Package the data.
                DataObject data = new DataObject();
                data.SetData("Object", this);

                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data,DragDropEffects.Move);
            }
        }

        private string tileStrokeColor;

        public Term Term { get; set; }
        public string TileText { get; set; }
        public string TileColor { get; set; }

        public static TermTile LastSelectedTermTile { get; set; }

        public string TileStrokeColor {
            get { return tileStrokeColor; }

            set {
                tileStrokeColor = value;
                rect.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(tileStrokeColor));
            }
        }

        public int StrokeThickness { get; set; }
        public int TextFontSize { get; set; }

        //na osnovu pozicije plocice s terminom kontamo koji je sad to tacan termin
        public void UpdateTerm()
        {
            if (Term == null) { return; }
            int row = Grid.GetRow(this);
            int column = Grid.GetColumn(this);
            Term.Day = (WEEKDAY)(column-1);
            int i = 0;
            TimeSpan time = new TimeSpan(7, 0, 0);
            while (i < row) {
                time = time.Add(new TimeSpan(0,15,0));
                i++;
            }
            Term.Time = time;
            if (!Term.Classroom.Terms.Contains(Term)) {
                Term.Classroom.Terms.Add(Term);
            }
            JSONUtil.Save();
        }

        

        public bool Equals(TermTile obj) {
            if (obj == null) {
                return false;
            }
            return Term == obj.Term;
        }

        //podesava selektovani element
        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (LastSelectedTermTile != null) {
                LastSelectedTermTile.TileStrokeColor = "White";
            }
            var tile = (TermTile)sender;
            tile.TileStrokeColor = "Red";
            var grid = ((Grid)tile.Parent);
            LastSelectedTermTile = tile;
            

            
        }

        
    }
}
