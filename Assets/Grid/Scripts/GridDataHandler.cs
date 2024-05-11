using UnityEngine;
using ProjectCore.Grid;
using ProjectCore.Events;
using ProjectCore.Data.Json;
using System.Collections.Generic;

public class GridDataHandler : MonoBehaviour
{
    [SerializeField] JsonManager JsonMan;
    [SerializeField] GridJsonDataSO GridJsonData;
    [SerializeField] GridDataSO CurrentGrid;
    [SerializeField] SOEvents SaveGridEvent;
    [SerializeField] string JsonFilePath, JsonPath, JsonFileName;


    private void OnEnable()
    {
        SaveGridEvent.Handler += SaveGridData;
    }

    private void OnDisable()
    {
        SaveGridEvent.Handler -= SaveGridData;
    }

    void Start()
    {
        GetGridDataFromJson();
    }


    void GetGridDataFromJson()
    {
        GridJsonData.CurrentJsonData = JsonMan.ParseJson<GridJsonData>(new GridJsonData(), JsonFilePath);
    }

    GridJsonData ConvertGridDataToObject()
    {
        GridJsonData newGrid = new GridJsonData();
        newGrid.TerrainGrid = new List<List<GridTileIndex>>();
        foreach (GridRow row in CurrentGrid.GridRows)
        {
            List<GridTileIndex> tileIndices = new List<GridTileIndex>();
            foreach (GridTile tile in row.TilesRow)
            {
                GridTileIndex tileIndex = new GridTileIndex();
                tileIndex.TileType = tile.TileId;
                tileIndices.Add(tileIndex);
            }
            newGrid.TerrainGrid.Add(tileIndices);
        }

        // Convert GridBuildings
        newGrid.GridBuildings = new List<GridBuilingBlock>();
        foreach (GridBuilingBlock buildingBlock in CurrentGrid.GridBuildings)
        {
            GridBuilingBlock newBuildingBlock = new GridBuilingBlock();
            newBuildingBlock.BuildingId = buildingBlock.BuildingId;
            foreach (Vector2Int tilePos in buildingBlock.TilesOccupied)
            {
                newBuildingBlock.TilesOccupied.Add(tilePos);
            }
            newGrid.GridBuildings.Add(newBuildingBlock);
        }
        return newGrid;
    }

    void SaveGridData()
    {
        JsonMan.SaveJson<GridJsonData>(ConvertGridDataToObject(),JsonPath,JsonFileName);
    }
}
