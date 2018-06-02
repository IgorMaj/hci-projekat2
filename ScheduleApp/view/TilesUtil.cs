using ScheduleApp.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ScheduleApp.view
{
    public class TilesUtil
    {

        public static void ClearTermArea(Grid grid,int row, int column, int rowspan) {
            var coordinates = GetAllTermCoordinates(row,column,rowspan);
            var tiles = GetTiles(grid, coordinates);
            MarkTiles(tiles,null);
        }


        //vraca sve koordinate u gridu nad kojim se ovaj term prostire
        public static List<Tuple<int, int>> GetAllTermCoordinates(int row, int column, int rowspan)
        {
            List<Tuple<int, int>> coordinates = new List<Tuple<int, int>>();

            for (int i = row; i < row + rowspan; i++)
            {
                coordinates.Add(new Tuple<int, int>(i, column));
            }
            return coordinates;
        }

        public static void DeleteSelectedTile() {
            if (TermTile.LastSelectedTermTile != null)
            {
                if (MessageBox.Show("Are you sure you want to delete the selected tile?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    var grid = ((Grid)TermTile.LastSelectedTermTile.Parent);
                    int row = Grid.GetRow(TermTile.LastSelectedTermTile);
                    int column = Grid.GetColumn(TermTile.LastSelectedTermTile);
                    int rowspan = Grid.GetRowSpan(TermTile.LastSelectedTermTile);
                    ClearTermArea(grid, row, column, rowspan);
                    JSONUtil.DeleteTermFromCLassroomAndSave(TermTile.LastSelectedTermTile.Term);
                    grid.Children.Remove(TermTile.LastSelectedTermTile);
                
                    TermTile.LastSelectedTermTile = null;
                }
                
            }
        }


        public static List<Tile> GetTiles(Grid grid, List<Tuple<int, int>> coordinates)
        {
            Tuple<int, int> iterTup;
            List<Tile> retVal = new List<Tile>();
            foreach (UIElement child in grid.Children)
            {
                iterTup = new Tuple<int, int>(Grid.GetRow(child), Grid.GetColumn(child));
                if (coordinates.Contains(iterTup) && child is Tile)
                {
                    coordinates.Remove(iterTup);
                    retVal.Add((Tile)child);
                }
            }

            return retVal;
        }

        public static void MarkTiles(List<Tile> tilesToMark, TermTile taken)
        {
            foreach (Tile t in tilesToMark)
            {
                t.Taken = taken;
            }
        }

        //ukoliko su vec zauzete od strane nekog drugog termina vraca false, kako bi obavestili korisnika da se termini ne mogu preklapati
        public static bool CheckAndMarkTiles(List<Tile> tilesToMark,TermTile tTile)
        {
            foreach (Tile t in tilesToMark)
            {
                if (t.Taken!= null && !t.Taken.Equals(tTile))
                {
                    return false;
                }
            }

            //ako su sve plocice slobodne :)
            MarkTiles(tilesToMark, tTile);

            return true;
        }
    }
}
