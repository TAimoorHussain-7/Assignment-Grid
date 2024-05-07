using UnityEngine;

namespace ProjectCore.Grid
{
    [CreateAssetMenu(fileName = "Table", menuName = "Scriptables/Grid/Objects/Tables")]
    public class TableObject : GridObjectInstantiator
    {
        [SerializeField] Vector2Int[] MyNeighbours;

        private GridTile _startingTile, _neighbourTile;
        
        public override void CheckForLocation(GridTile currentTile)
        {
            CanInstantiate = false;
            _startingTile = null;
            if (currentTile.TileId == RequiredTileId)
            {
                CheckAvailableNeighbour(currentTile);
            }
        }

        public override void InstantiateObject(Transform parent)
        {
            if (CanInstantiate)
            {
                _startingTile.IsOccupied = true;
                _neighbourTile.IsOccupied = true;
                GameObject newObj = Instantiate(CurrentObj, parent);
                newObj.transform.position = _startingTile.transform.position;
            }
        }

        private void CheckAvailableNeighbour(GridTile currentTile)
        {
            if (!currentTile.IsOccupied)
            {
                for (int t = 0; t < MyNeighbours.Length; t++)
                {
                    Vector2Int nextTile = new Vector2Int(currentTile.xIndex + MyNeighbours[t].x, currentTile.yIndex + MyNeighbours[t].y);
                    if (nextTile.x < CurrentGrid.GridTiles.Length && nextTile.y < CurrentGrid.GridTiles[nextTile.x].TilesRow.Count)
                    {
                        GridTile newTile = CurrentGrid.GridTiles[nextTile.x].TilesRow[nextTile.y];
                        if (newTile.TileId == RequiredTileId && !newTile.IsOccupied)
                        {
                            CanInstantiate = true;
                            if (t == 0)
                            {
                                ShowPlacement(currentTile);
                                _neighbourTile = newTile;
                            }
                            else if (t == 1)
                            {
                                ShowPlacement(newTile);
                                _neighbourTile = currentTile;
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
        }

        private void ShowPlacement(GridTile StratTile)
        {
            _startingTile = StratTile;
        }
    }
}
