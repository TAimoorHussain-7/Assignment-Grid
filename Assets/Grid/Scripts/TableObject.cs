using UnityEngine;

namespace ProjectCore.Grid
{
    [CreateAssetMenu(fileName = "Table", menuName = "Scriptables/Grid/Objects/Tables")]
    public class TableObject : GridObjectInstantiator
    {
        [SerializeField] Vector2Int[] MyNeighbours;
        [SerializeField] GridObjectView VerticalTable;
        [SerializeField] ProjectGrid CurrentGrid;

        GridTile _neighbourTile;
        GridObjectView _newObj;
        bool _isHorizontalTable = true;

        public override void CheckForLocation(GridTile currentTile, Transform parent)
        {
            CanInstantiate = false;
            StartingTile = null;
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
                StartingTile.IsOccupied = true;
                _neighbourTile.IsOccupied = true;
                GridTile[] objectLocation = new GridTile[2];
                objectLocation[0] = StartingTile;
                objectLocation[1] = _neighbourTile;
                _newObj.ActiveObject(objectLocation);
                _newObj = null;
                CanInstantiate = false;
            }
        }

        public override void DestroyObject()
        {
            if (_newObj != null)
            {
                Destroy(_newObj.gameObject);
                _newObj = null;
            }
        }

        private void CheckAvailableNeighbour(GridTile currentTile)
        {
            if (!currentTile.IsOccupied)
            {
                for (int t = 0; t < MyNeighbours.Length; t++)
                {
                    Vector2Int nextTile = new Vector2Int(currentTile.xIndex + MyNeighbours[t].x, currentTile.yIndex + MyNeighbours[t].y);
                    if (nextTile.x > -1 && nextTile.x < CurrentGrid.GridTiles.Length && nextTile.y > -1 && nextTile.y < CurrentGrid.GridTiles[nextTile.x].TilesRow.Count)
                    {
                        GridTile newTile = CurrentGrid.GridTiles[nextTile.x].TilesRow[nextTile.y];
                        if (newTile.TileId == RequiredTileId && !newTile.IsOccupied)
                        {
                            if (t == 0)
                            {
                                ShowHorizontalPlacement(currentTile);
                                _neighbourTile = newTile;
                            }
                            else if (t == 1)
                            {
                                ShowHorizontalPlacement(newTile);
                                _neighbourTile = currentTile;
                            }
                            else if (t == 2)
                            {
                                ShowVerticalPlacement(currentTile);
                                _neighbourTile = newTile;
                            }
                            else if (t == 3)
                            {
                                ShowVerticalPlacement(newTile);
                                _neighbourTile = currentTile;
                            }
                        }
                    }
                }
            }
        }

        private void ShowHorizontalPlacement(GridTile StratTile)
        {
            StartingTile = StratTile;

            if(!_isHorizontalTable && _newObj != null)
            {
                DestroyObject();
            }

            if (_newObj == null)
            {
                _newObj = Instantiate(CurrentObj, StartingTile.transform.position, StartingTile.transform.rotation).GetComponent<GridObjectView>();
                _newObj.transform.SetParent(ObjectParent);
                _isHorizontalTable = true;
            }
            _newObj.transform.position = StartingTile.transform.position;
            _newObj.HighlightObject();
           CanInstantiate = true;
        }

        private void ShowVerticalPlacement(GridTile StratTile)
        {
            StartingTile = StratTile;

            if(_isHorizontalTable && _newObj != null)
            {
                DestroyObject();
            }

            if (_newObj == null)
            {
                _newObj = Instantiate(VerticalTable, StartingTile.transform.position, StartingTile.transform.rotation);
                _newObj.transform.SetParent(ObjectParent);
                _isHorizontalTable = false;
            }
            _newObj.transform.position = StartingTile.transform.position;
            _newObj.HighlightObject();
           CanInstantiate = true;
        }
    }
}
