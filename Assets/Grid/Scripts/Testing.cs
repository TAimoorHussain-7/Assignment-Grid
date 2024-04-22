using UnityEngine;
using ProjectCore.Grid;
using ProjectCore.Data.Json;

public class Testing : MonoBehaviour
{
    [SerializeField] JsonParserGridData JsonMan;
    [SerializeField] GridDataSO GridTileSData;
    [SerializeField] string JsonPath;

    private GridData _gridData;

    void Start()
    {
        _gridData = JsonMan.ParseJson<GridData>(new GridData(), JsonPath);
    }

    public void CheckData()
    {
        if (GridTileSData.GridData != null)
        {
            // Iterate through each row in the grid
            foreach (var row in _gridData.TerrainGrid)
            {
                // Iterate through each tile in the row
                foreach (var tile in row)
                {
                    // Print the TileType of each tile
                    Debug.Log("TileType: " + tile.TileType);
                }
            }
        }
    }
}
