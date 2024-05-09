using UnityEngine;

namespace ProjectCore.Grid
{
    [CreateAssetMenu(fileName = "Table", menuName = "Scriptables/Grid/Objects/Tables")]
    public class TableObject : GridObjectInstantiator
    {
        [SerializeField] Vector2Int[] MyNeighbours;

        GridTile _startingTile, _neighbourTile;
        GridObjectView _newObj;

        
        public override void CheckForLocation(GridTile currentTile, Transform parent)
        {
            CanInstantiate = false;
            _startingTile = null;
            ObjectParent = parent;
            if (currentTile.TileId == RequiredTileId)
            {
                CheckAvailableNeighbour(currentTile);
            }
            else
            {
                if (_newObj != null)
                {
                    _newObj.HideObject();
                }
            }
        }

        public override void InstantiateObject()
        {
            if (CanInstantiate)
            {
                _startingTile.IsOccupied = true;
                _neighbourTile.IsOccupied = true;
                GridTile[] objectLocation = new GridTile[2];
                objectLocation[0] = _startingTile;
                objectLocation[1] = _neighbourTile;
                _newObj.ActiveObject(objectLocation);
                _newObj = null;
            }
        }

        public void DestroyObject()
        {
            Destroy(_newObj);
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
            if (_newObj == null)
            {
                _newObj = Instantiate(CurrentObj, _startingTile.transform.position, _startingTile.transform.rotation);
                _newObj.transform.SetParent(ObjectParent);
            }
            _newObj.transform.position = _startingTile.transform.position;
            _newObj.HighlightObject();
           CanInstantiate = true;
        }
    }
}
