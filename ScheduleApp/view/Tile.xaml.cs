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
            
        }
        public bool DropAllowed { get; set; }
        public string TileText { get; set; }
        public string TileColor { get; set; }
        public string TileStrokeColor { get; set; }
        public int StrokeThickness { get; set; }
        public int TextFontSize { get; set; }

        //obican tile moze biti target drag and dropa, ako to dozvolimo(oni koji pokazuju vreme i dane u nedelji ne mogu biti target dropovanja)
        protected override void OnDrop(DragEventArgs e) {
            if (!DropAllowed) {
                return;
            }

            base.OnDrop(e);

            // If the DataObject contains string data, extract it.
            if (e.Data.GetDataPresent("Object"))
            {
              
                {
                    //TODO postavi podatke
                    var oldTermTile = (TermTile)e.Data.GetData("Object");
                    
                    TermTile termTile = new TermTile(oldTermTile);

                    int row = Grid.GetRow(this);
                    int column = Grid.GetColumn(this);
                    int newTileRowspan = 3 * (termTile.Term.Length);
                    if (row + newTileRowspan > ((Grid)this.Parent).RowDefinitions.Count) {
                        MessageBox.Show("Impossible to drag. You will break the time limit.");
                        return;
                    }
                    Grid.SetRow(termTile,row);
                    Grid.SetRowSpan(termTile,newTileRowspan);
                    Grid.SetColumn(termTile,column);
                    ((Grid)this.Parent).Children.Add(termTile);
                    ((Grid)this.Parent).Children.Remove(oldTermTile);

                    e.Effects = DragDropEffects.Move;
                    //na osnovu pozicije plocice cemo da provalimo novi termin
                    termTile.UpdateTerm();
                }
            }
            e.Handled = true;


        }


    }
}
