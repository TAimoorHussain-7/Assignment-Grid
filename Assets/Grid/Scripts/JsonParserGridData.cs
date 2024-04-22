using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using ProjectCore.Grid;

namespace ProjectCore.Data.Json
{
    [CreateAssetMenu(fileName = "jsonParserGrid", menuName = "Scriptables/Data/Json/jsonParserGrid")]

    public class JsonParserGridData : JsonParser
    {
        public override T ParseJson<T>(T jsonObj, string jsonPath)
        {
            string jsonString = File.ReadAllText(jsonPath);
            TerrainGridJson terrainGridJson = JsonConvert.DeserializeObject<TerrainGridJson>(jsonString);

            // Convert the jagged array to a 2D array
            int[,] terrainGrid = new int[terrainGridJson.TerrainGrid.Length, terrainGridJson.TerrainGrid[0].Length];
            for (int i = 0; i < terrainGridJson.TerrainGrid.Length; i++)
            {
                for (int j = 0; j < terrainGridJson.TerrainGrid[i].Length; j++)
                {
                    terrainGrid[i, j] = terrainGridJson.TerrainGrid[i][j];
                }
            }

            // Print the terrain grid
            for (int i = 0; i < terrainGrid.GetLength(0); i++)
            {
                for (int j = 0; j < terrainGrid.GetLength(1); j++)
                {
                    Debug.Log("TileType at row " + i + ", column " + j + " is: " + terrainGrid[i, j]);
                }
            }

            return jsonObj;
        }
    }
}
