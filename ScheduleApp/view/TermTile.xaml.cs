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
    /// Interaction logic for TermTile.xaml
    /// </summary>
    public partial class TermTile : UserControl
    {
       
        public TermTile()
        {
            InitializeComponent();
            DataContext = this;
            TileText = "Subject";
            TileColor = "White";
            TileStrokeColor = "White";
            StrokeThickness = 5;
            TextFontSize = 20;

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

        public Term Term { get; set; }
        public string TileText { get; set; }
        public string TileColor { get; set; }
        public string TileStrokeColor { get; set; }
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
                time = time.Add(new TimeSpan(0,45,0));
                i++;
            }

            Term.Time = time;
        }
    }
}
