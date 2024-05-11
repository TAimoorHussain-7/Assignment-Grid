using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace ProjectCore.Data.Json
{
    [CreateAssetMenu(fileName = "jsonSaver", menuName = "Scriptables/Data/Json/JsonSaver")]
    public class JsonSaver : ScriptableObject, IJsonSaver
    {
        public void SaveJson<T>(T jsonObj, string jsonPath, string fileName)
        {
            try
            {
                string filePath = Path.Combine(jsonPath, fileName + ".json");
                int fileNumber = 1;
                while (File.Exists(filePath))
                {
                    string newName = fileName + "_" + fileNumber;
                    filePath = Path.Combine(jsonPath, newName + ".json");
                    fileNumber++;
                }

                string jsonString = JsonConvert.SerializeObject(jsonObj);
                File.WriteAllText(filePath, jsonString);
                Debug.Log("Object converted to JSON file and saved successfully at path: " + filePath);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error converting object to JSON and saving file at path: " + jsonPath + "\n" + ex.Message);
            }
        }
    }
}
