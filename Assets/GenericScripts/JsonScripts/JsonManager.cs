using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace ProjectCore.Data.Json
{
    [CreateAssetMenu(fileName ="jsonManager", menuName ="Scriptables/Data/Json/JsonManager")]
    public class JsonManager : ScriptableObject, IJsonParser, IJsonSaver
    {
        public T ParseJson<T>(T jsonObj, string jsonPath)
        {
            if (!File.Exists(jsonPath))
            {
                Debug.LogError("JSON file not found at path: " + jsonPath);
                return default(T);
            }


            try
            {
                string jsonString = File.ReadAllText(jsonPath);
                T parsedObject = JsonConvert.DeserializeObject<T>(jsonString);
                return parsedObject;
            }
            catch (JsonReaderException jsonEx)
            {
                Debug.LogError("JSON parsing error at path: " + jsonPath + "\n" + jsonEx.Message);
                return default(T);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error parsing JSON at path: " + jsonPath + "\n" + ex.Message);
                return default(T);
            }
        }

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