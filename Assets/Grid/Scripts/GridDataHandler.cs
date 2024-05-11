using UnityEngine;
using ProjectCore.Grid;
using ProjectCore.Data.Json;

public class GridDataHandler : MonoBehaviour
{
    [SerializeField] JsonParser JsonMan;
    [SerializeField] GridJsonDataSO GridJsonData;
    [SerializeField] GridDataSO CurrentGrid;
    [SerializeField] string JsonFilePath;

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
        if (CurrentGrid.GridRows.Length > 0)
        {
            for (int r = 0; r < CurrentGrid.GridRows.Length; r++) 
            { 
                for (int c = 0; c < CurrentGrid.GridRows[r].TilesRow.Count; c++)
                {

                }
            }
        }
        else
        {
            Debug.LogError("No GridData Available to convert");
        }
        return newGrid;
    }
}
