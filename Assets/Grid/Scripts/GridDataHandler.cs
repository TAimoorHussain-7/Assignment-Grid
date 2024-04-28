using UnityEngine;
using ProjectCore.Grid;
using ProjectCore.Events;
using ProjectCore.Data.Json;
using System.Collections.Generic;

public class GridDataHandler : MonoBehaviour
{
    [SerializeField] JsonManager JsonMan;
    [SerializeField] GridDataSO GridTileSData;
    [SerializeField] SOEvents SaveGridEvent;
    [SerializeField] string JsonFilePath,JsonPath, JsonFileName;

    private GridData _gridData;

    private void OnEnable()
    {
        SaveGridEvent.Handler += SaveGridDataJson;
    }

    private void OnDisable()
    {
        SaveGridEvent.Handler -= SaveGridDataJson;
    }

    void Start()
    {
        GetGridDataFromJson();
    }


    private void GetGridDataFromJson()
    {
        _gridData = JsonMan.ParseJson<GridData>(new GridData(), JsonFilePath);
        Invoke(nameof(ConvertJsonDataToSO),1);
    }


    private void ConvertJsonDataToSO()
    {
        GridTileSData.GridRows = new GridRow[_gridData.TerrainGrid.Count];

        for (int row = 0; row < _gridData.TerrainGrid.Count; row++)
        {
            GridTileSData.GridRows[row] = new GridRow();
            GridTileSData.GridRows[row].IntList = new List<int>();
            for (int col = 0; col < _gridData.TerrainGrid[row].Count; col++)
            {
                GridTileSData.GridRows[row].IntList.Add(_gridData.TerrainGrid[row][col].TileType);
            }
        }
    }

    private GridData ConvertToGridData(GridDataSO gridTileSData)
    {
        GridData gridData = new GridData();
        gridData.TerrainGrid = new List<List<GridTileIndex>>();

        if (gridTileSData == null || gridTileSData.GridRows == null)
        {
            Debug.LogError("GridTileSData or GridRows is null.");
            return gridData;
        }

        for (int row = 0; row < gridTileSData.GridRows.Length; row++)
        {
            List<GridTileIndex> rowList = new List<GridTileIndex>();

            if (gridTileSData.GridRows[row] != null && gridTileSData.GridRows[row].IntList != null)
            {
                for (int col = 0; col < gridTileSData.GridRows[row].IntList.Count; col++)
                {
                    GridTileIndex tileIndex = new GridTileIndex();
                    tileIndex.TileType = gridTileSData.GridRows[row].IntList[col];
                    rowList.Add(tileIndex);
                }
            }
            else
            {
                Debug.LogWarning("IntList is null at row " + row);
            }

            gridData.TerrainGrid.Add(rowList);
        }

        return gridData;
    }

    private void SaveGridDataJson()
    {
        JsonMan.SaveJson<GridData>(ConvertToGridData(GridTileSData), JsonPath, JsonFileName);
    }
}
