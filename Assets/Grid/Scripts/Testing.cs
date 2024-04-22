using UnityEngine;
using ProjectCore.Grid;
using ProjectCore.Data.Json;

public class Testing : MonoBehaviour
{
    [SerializeField] JsonParserGridData JsonMan;
    [SerializeField] TerrainDataSO TerainGridSO;
    [SerializeField] string JsonPath;

    private TerrainGridJson _gridData;

    void Start()
    {
        _gridData = JsonMan.ParseJson(_gridData, JsonPath);
    }

    public void CheckData()
    {
        //TerainGridSO.gridData = _gridData;
        foreach (var row in _gridData.TerrainGrid)
        {
            foreach (var tile in row)
            {
                //Debug.Log("TileType: " + tile.TileType);
            }
        }
    }
}
