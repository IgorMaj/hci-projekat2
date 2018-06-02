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
    /// Interaction logic for Tile.xaml
    /// </summary>
    public partial class Tile : UserControl
    {

        public Tile()
        {
            InitializeComponent();
            DataContext = this;
            TileText = "TileText";
            TileColor = "White";
            TileStrokeColor = "White";
            StrokeThickness = 4;
            TextFontSize = 20;
            DropAllowed = false;
            Taken = null;

        }
        public bool DropAllowed { get; set; }
        public string TileText { get; set; }
        public string TileColor { get; set; }
        public string TileStrokeColor { get; set; }
        public int StrokeThickness { get; set; }
        public int TextFontSize { get; set; }
        //kaze koji Term Tile se nalazi nad njim
        public TermTile Taken { get; set; }

        //obican tile moze biti target drag and dropa, ako to dozvolimo(oni koji pokazuju vreme i dane u nedelji ne mogu biti target dropovanja)
        protected override void OnDrop(DragEventArgs e)
        {
            if (!DropAllowed)
            {
                MessageBox.Show("Dropping is not allowed here.");
                return;
            }

            base.OnDrop(e);

            // If the DataObject contains string data, extract it.
            if (e.Data.GetDataPresent("Object"))
            {

                {
                    //TODO postavi podatke
                    Grid grid = ((Grid)this.Parent);
                    var oldTermTile = (TermTile)e.Data.GetData("Object");

                    TermTile termTile = new TermTile(oldTermTile);

                    int row = Grid.GetRow(this);
                    int column = Grid.GetColumn(this);

                    int newTileRowspan = 3 * (termTile.Term.Length);
                    if (row + newTileRowspan > (grid.RowDefinitions.Count))
                    {
                        MessageBox.Show("Impossible to drag. You will break the time limit.");
                        return;
                    }
                    List<Tuple<int, int>> coordinates = TilesUtil.GetAllTermCoordinates(row, column, newTileRowspan);



                    List<Tile> tilesToMark = TilesUtil.GetTiles(grid, coordinates);
                    if (!TilesUtil.CheckAndMarkTiles(tilesToMark,termTile))
                    {
                        MessageBox.Show("Terms can't overlap.");
                        return;
                    }

                    Grid.SetRow(termTile, row);
                    Grid.SetRowSpan(termTile, newTileRowspan);
                    Grid.SetColumn(termTile, column);

                    grid.Children.Add(termTile);
                    int oldRow = Grid.GetRow(oldTermTile);
                    int oldColumn = Grid.GetColumn(oldTermTile);
                    int oldRowSpan = Grid.GetRowSpan(oldTermTile);
                    //oslobadjamo stare plocice gde se termin nekad prostirao :D
                    List<Tuple<int, int>> oldCoordinates = TilesUtil.GetAllTermCoordinates(oldRow, oldColumn, oldRowSpan);
                    tilesToMark = TilesUtil.GetTiles(grid, oldCoordinates);
                    TilesUtil.MarkTiles(tilesToMark, null);

                    //ukoliko je stara plocica bila selektovana pre prevlacenja, nova mora biti isto
                    if (oldTermTile.Equals(TermTile.LastSelectedTermTile)) {
                        TermTile.LastSelectedTermTile = termTile;
                    }

                    grid.Children.Remove(oldTermTile);


                    e.Effects = DragDropEffects.Move;
                    //na osnovu pozicije plocice cemo da provalimo novi termin
                    termTile.UpdateTerm();
                }
            }
            e.Handled = true;


        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (TermTile.LastSelectedTermTile != null) {
                TermTile.LastSelectedTermTile.TileStrokeColor = "White";
                TermTile.LastSelectedTermTile = null;
            }
            
        }
    } 
}
