using UnityEngine;
using ProjectCore.Variables;
using ProjectCore.Collections;
using System.Collections.Generic;

namespace ProjectCore.Grid
{
    [CreateAssetMenu(fileName = "GridCreator", menuName = "Scriptables/Grid/GridCreator")]
    public class GridCreator : ScriptableObject
    {
        [SerializeField] SOGameObjectsArray TilePrefabs;
        [SerializeField] SOBool CreatingGrid;
        [SerializeField] ProjectGrid CurrentGrid;

        public void CreateGrid(GridJsonData gridData, Vector3 tileSize, Transform tileParent)
        {
            if (gridData.TerrainGrid.Count > 0)
            {
                CurrentGrid.GridTiles = new GridRow[gridData.TerrainGrid.Count];
                for (int r = 0; r < gridData.TerrainGrid.Count; r++)
                {
                    CurrentGrid.GridTiles[r] = new GridRow();
                    CurrentGrid.GridTiles[r].TilesRow = new List<GridTile>();
                    for (int c = 0; c < gridData.TerrainGrid[r].Count; c++)
                    {
                        int tileTypeIndex = gridData.TerrainGrid[r][c].TileType;

                        // Check if the tile type index is valid
                        if (tileTypeIndex >= 0 && tileTypeIndex < TilePrefabs.Objects.Length)
                        {
                            // Instantiate the corresponding tile prefab at the appropriate position
                            GameObject tilePrefab = TilePrefabs.Objects[tileTypeIndex];
                            if (tilePrefab != null)
                            {
                                Vector3 position = new Vector3(c * tileSize.x, 0, r * tileSize.z); // Adjust position as needed
                                GridTile tile = Instantiate(tilePrefab, tileParent).GetComponent<GridTile>();
                                tile.transform.localPosition = position; // Set tile size
                                tile.transform.localScale = new Vector3(tileSize.x, 1f, tileSize.z); // Set tile size
                                tile.TileId = tileTypeIndex;
                                tile.xIndex = r;
                                tile.yIndex = c;
                                CurrentGrid.GridTiles[r].TilesRow.Add(tile);
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
            CreatingGrid.Value = false;
        }

    }
}
