using UnityEngine;
using ProjectCore.Variables;

namespace ProjectCore.Grid
{
    [CreateAssetMenu(fileName = "Table", menuName = "Scriptables/Grid/Objects/Tables")]
    public class TableObject : GridObjectInstantiator
    {
        [SerializeField] Vector2Int[] MyNeighbours;
        [SerializeField] GridObjectInstantiator OtherTable;
        [SerializeField] GridDataSO CurrentGrid;
        [SerializeField] SoGameObject GridObj;
        [SerializeField] GridObjectHolderSO ObjectHolder;
        [SerializeField] int BuildingId;

        GridTile _neighbourTile;
        GridObjectView _newObj;

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
                CanInstantiate = false;
                GridBuilingBlock newTable = new GridBuilingBlock();
                newTable.TilesOccupied.Add(new Vector2Int(StartingTile.XIndex, StartingTile.YIndex));
                newTable.TilesOccupied.Add(new Vector2Int(_neighbourTile.XIndex, _neighbourTile.YIndex));
                newTable.BuildingId = BuildingId;
                _newObj.ActiveObject(newTable);
                _newObj = null;
                GridObj.Obj = null;
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
                            else
                            {
                                DestroyNewObj();
                                ObjectHolder.GridObject = OtherTable;
                                OtherTable.CheckForLocation(currentTile,ObjectParent);
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

            if (_newObj == null)
            {
                GridObj.Obj = Instantiate(CurrentObj, StartingTile.transform.position, StartingTile.transform.rotation);
                _newObj = GridObj.Obj.GetComponent<GridObjectView>();
                _newObj.transform.SetParent(ObjectParent);
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
