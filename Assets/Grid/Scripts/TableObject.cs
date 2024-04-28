using UnityEngine;

namespace ProjectCore.Grid
{
    [CreateAssetMenu(fileName = "Table", menuName = "Scriptables/Grid/Objects/Tables")]
    public class TableObject : GridObjectInstantiator
    {
        [SerializeField] Vector2Int[] MyNeighbours;
        
        public override void CheckForLocation(GridTile currentTile)
        {
            if (currentTile.TileId == RequiredTileId)
            {
                int availableNeighbour = CheckAvailableNeighbour(currentTile.xIndex, currentTile.yIndex);
                if (availableNeighbour != -1)
                {

                }
            }
        }

        public override void InstantiateObject()
        {

        }

        private int CheckAvailableNeighbour(int x , int y)
        {
            for (int t =0; t<MyNeighbours.Length; t++)
            {
                Vector2Int nextTile = new Vector2Int(x + MyNeighbours[t].x, y + MyNeighbours[t].y);
                if (CurrentGrid.GridTiles[nextTile.x].TilesRow[nextTile.y] != null)
                {
                    GridTile tile = CurrentGrid.GridTiles[nextTile.x].TilesRow[nextTile.y];
                    if (tile.TileId == RequiredTileId && !tile.IsOccupied)
                    {
                        return t;
                    }
                }
            }
            return -1;
        }

        private void ShowActiveTile(int neighbourIndex)
        {

        }
    }
}
