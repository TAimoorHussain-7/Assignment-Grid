using UnityEngine;
using ProjectCore.Variables;
using ProjectCore.Collections;
using System.Collections.Generic;

namespace ProjectCore.Grid
{
    [CreateAssetMenu(fileName = "GridCreator", menuName = "Scriptables/Grid/GridCreator")]
    public class GridCreator : ScriptableObject
    {
        [SerializeField] GridTile TileObj;
        [SerializeField] SOGameObjectsArray TilePrefabs, BuildingPrefabs;
        [SerializeField] SOBool CreatingGrid;
        [SerializeField] GridDataSO CurrentGrid;

        public void CreateGrid(GridJsonData gridData, Vector3 tileSize, Transform tileParent, Transform buildingParent)
        {
            if (gridData.TerrainGrid.Count > 0)
            {
                CurrentGrid.GridRows = new GridRow[gridData.TerrainGrid.Count];
                for (int r = 0; r < gridData.TerrainGrid.Count; r++)
                {
                    CurrentGrid.GridRows[r] = new GridRow();
                    CurrentGrid.GridRows[r].TilesRow = new List<GridTile>();
                    for (int c = 0; c < gridData.TerrainGrid[r].Count; c++)
                    {
                        int tileTypeIndex = gridData.TerrainGrid[r][c].TileType;

                        // Check if the tile type index is valid
                        if (tileTypeIndex >= 0 && tileTypeIndex < TilePrefabs.Objects.Length)
                        {
                            // Instantiate the corresponding tile prefab at the appropriate position
                            GameObject spriteObj = TilePrefabs.Objects[tileTypeIndex];
                            if (spriteObj != null)
                            {
                                Vector3 position = new Vector3(c * tileSize.x, r * tileSize.y, 0); // Adjust position as needed
                                GridTile newTile = Instantiate(TileObj, tileParent);
                                newTile.transform.localPosition = position; // Set tile size
                                newTile.transform.localScale = new Vector3(tileSize.x, tileSize.y,1); // Set tile size
                                newTile.TileId = tileTypeIndex;
                                newTile.XIndex = r;
                                newTile.YIndex = c;
                                CurrentGrid.GridRows[r].TilesRow.Add(newTile);
                                Instantiate(spriteObj, newTile.transform);
                            }
                            else
                            {
                                Debug.LogWarning("Tile prefab is null for tile type index: " + tileTypeIndex);
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("No GridData Available to create Grid");
            }

            if (gridData.GridBuildings.Count > 0)
            {
                foreach (GridBuilingBlock building in gridData.GridBuildings)
                {
                    GridObjectView newBuilding = Instantiate(BuildingPrefabs.Objects[building.BuildingId], buildingParent).GetComponent<GridObjectView>();
                    newBuilding.transform.position = CurrentGrid.GridRows[building.TilesOccupied[0].x].TilesRow[building.TilesOccupied[0].y].transform.position;
                    newBuilding.ActiveObject(building);
                }
            }
            else
            {
                Debug.Log("No GridObject Available to create");
            }
            CreatingGrid.Value = false;
        }

    }
}
