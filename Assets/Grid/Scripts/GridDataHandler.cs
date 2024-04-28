using UnityEngine;
using ProjectCore.Grid;
using ProjectCore.Data.Json;

public class GridDataHandler : MonoBehaviour
{
    [SerializeField] JsonParser JsonMan;
    [SerializeField] GridJsonDataSO GridJsonData;
    [SerializeField] string JsonFilePath;

    void Start()
    {
        GetGridDataFromJson();
    }


    private void GetGridDataFromJson()
    {
        GridJsonData.CurrentJsonData = JsonMan.ParseJson<GridJsonData>(new GridJsonData(), JsonFilePath);
    }
}
