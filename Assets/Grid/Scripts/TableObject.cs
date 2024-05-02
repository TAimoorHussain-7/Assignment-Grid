using UnityEngine;

namespace ProjectCore.Grid
{
    [CreateAssetMenu(fileName = "Table", menuName = "Scriptables/Grid/Objects/Tables")]
    public class TableObject : GridObjectInstantiator
    {
        [SerializeField] Vector2Int[] MyNeighbours;

        private GridTile StartingTile;
        
        public override void CheckForLocation(GridTile currentTile)
        {
            CanInstantiate = false;
            StartingTile = null;
            if (currentTile.TileId == RequiredTileId)
            {
                CheckAvailableNeighbour(currentTile);
            }
        }

        public override void InstantiateObject()
        {

        }

        private void CheckAvailableNeighbour(GridTile currentTile)
        {
            for (int t =0; t<MyNeighbours.Length; t++)
            {
                Vector2Int nextTile = new Vector2Int(currentTile.xIndex + MyNeighbours[t].x, currentTile.yIndex + MyNeighbours[t].y);
                if (CurrentGrid.GridTiles[nextTile.x].TilesRow[nextTile.y] != null)
                {
                    GridTile newTile = CurrentGrid.GridTiles[nextTile.x].TilesRow[nextTile.y];
                    if (newTile.TileId == RequiredTileId && !newTile.IsOccupied)
                    {
                        CanInstantiate = true;
                        if (t == 0)
                        {
                            ShowPlacement(currentTile);
                        }
                        else if (t == 1)
                        {
                            ShowPlacement(newTile);
                        }
                        else
                        {

                        }
                    }
                }
            }
        }

        private void ShowPlacement(GridTile StratTile)
        {

        }
    }
}
