using UnityEngine;
using ProjectCore.Variables;
using ProjectCore.Collections;

namespace ProjectCore.Grid
{
    [CreateAssetMenu(fileName = "GridCreator", menuName = "Scriptables/Grid/GridCreator")]
    public class GridCreator : ScriptableObject
    {
        [SerializeField] SOGameObjectsArray TilePrefabs;
        [SerializeField] SOBool CreatingGrid;

        public void CreateGrid(GridDataSO gridData, Vector3 tileSize, Transform tileParent)
        {
            if (gridData.GridRows.Length > 0)
            {
                for (int r = 0; r < gridData.GridRows.Length; r++)
                {
                    int[] row = gridData.GridRows[r].IntList.ToArray();

                    for (int c = 0; c < row.Length; c++)
                    {
                        int tileTypeIndex = row[c];

                        // Check if the tile type index is valid
                        if (tileTypeIndex >= 0 && tileTypeIndex < TilePrefabs.Objects.Length)
                        {
                            // Instantiate the corresponding tile prefab at the appropriate position
                            GameObject tilePrefab = TilePrefabs.Objects[tileTypeIndex];
                            if (tilePrefab != null)
                            {
                                Vector3 position = new Vector3(c * tileSize.x, 0, r * tileSize.z); // Adjust position as needed
                                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, tileParent);
                                tile.transform.localScale = new Vector3(tileSize.x, 1f, tileSize.z); // Set tile size
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
