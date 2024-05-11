using UnityEngine;
using ProjectCore.Variables;

namespace ProjectCore.Grid
{
    [CreateAssetMenu(fileName = "Table", menuName = "Scriptables/Grid/Objects/Tables")]
    public class TableObject : GridObjectInstantiator
    {
        [SerializeField] Vector2Int[] MyNeighbours;
        [SerializeField] GridObjectView VerticalTable;
        [SerializeField] GridDataSO CurrentGrid;
        [SerializeField] SoGameObject GridObj;
        [SerializeField] int BuildingId;

        GridTile _neighbourTile;
        GridObjectView _newObj;
        bool _isCurrentTable = true;

        public override void CheckForLocation(GridTile currentTile, Transform parent)
        {
            CanInstantiate = false;
            StartingTile = null;
            ObjectParent = parent;
            if (!currentTile.IsOccupied && currentTile.TileId == RequiredTileId)
            {
                CheckAvailableNeighbour(currentTile);
            }
            else
            {
                currentTile.RemoveHighlight();
                currentTile.HighlightTile(0);

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
                GridBuilingBlock newTable = new GridBuilingBlock();
                newTable.TilesOccupied.Add(new Vector2Int(StartingTile.XIndex, StartingTile.YIndex));
                newTable.TilesOccupied.Add(new Vector2Int(_neighbourTile.XIndex, _neighbourTile.YIndex));
                newTable.BuildingId = BuildingId;
                CurrentGrid.GridBuildings.Add(newTable);
                _newObj = null;
                GridObj.Obj = null;
                CanInstantiate = false;
            }
        }

        private void CheckAvailableNeighbour(GridTile currentTile)
        {
            if (!currentTile.IsOccupied)
            {
                for (int t = 0; t < MyNeighbours.Length; t++)
                {
                    Vector2Int nextTile = new Vector2Int(currentTile.XIndex + MyNeighbours[t].x, currentTile.YIndex + MyNeighbours[t].y);
                    if (nextTile.x > -1 && nextTile.x < CurrentGrid.GridRows.Length && nextTile.y > -1 && nextTile.y < CurrentGrid.GridRows[nextTile.x].TilesRow.Count)
                    {
                        GridTile newTile = CurrentGrid.GridRows[nextTile.x].TilesRow[nextTile.y];
                        if (newTile.TileId == RequiredTileId && !newTile.IsOccupied)
                        {
                            if (t == 0)
                            {
                                ShowCurrentTablePlacement(currentTile);
                                _neighbourTile = newTile;
                            }
                            else if (t == 1)
                            {
                                ShowCurrentTablePlacement(newTile);
                                _neighbourTile = currentTile;
                            }
                            else if (t == 2)
                            {
                                ShowOtherTablePlacement(currentTile);
                                _neighbourTile = newTile;
                            }
                            else if (t == 3)
                            {
                                ShowOtherTablePlacement(newTile);
                                _neighbourTile = currentTile;
                            }
                            return;
                        }
                    }
                }
                currentTile.RemoveHighlight();
                currentTile.HighlightTile(0);
            }
        }

        private void ShowCurrentTablePlacement(GridTile StratTile)
        {
            StartingTile = StratTile;

            if(!_isCurrentTable && _newObj != null)
            {
                DestroyNewObj();
            }

            if (_newObj == null)
            {
                GridObj.Obj = Instantiate(CurrentObj, StartingTile.transform.position, StartingTile.transform.rotation);
                _newObj = GridObj.Obj.GetComponent<GridObjectView>();
                _newObj.transform.SetParent(ObjectParent);
                _isCurrentTable = true;
            }
            _newObj.transform.position = StartingTile.transform.position;
            _newObj.HighlightObject();
           CanInstantiate = true;
        }

        private void ShowOtherTablePlacement(GridTile StratTile)
        {
            StartingTile = StratTile;

            if(_isCurrentTable && _newObj != null)
            {
                DestroyNewObj();
            }

            if (_newObj == null)
            {
                _newObj = Instantiate(VerticalTable, StartingTile.transform.position, StartingTile.transform.rotation);
                GridObj.Obj = _newObj.gameObject;
                _newObj.transform.SetParent(ObjectParent);
                _isCurrentTable = false;
            }
            _newObj.transform.position = StartingTile.transform.position;
            _newObj.HighlightObject();
           CanInstantiate = true;
        }

        private void DestroyNewObj()
        {
            GridObj.DestroyObject();
            _newObj = null;
        }
    }
}
