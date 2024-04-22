using System.IO;
using UnityEngine;

namespace ProjectCore.Data.Json
{
    [CreateAssetMenu(fileName ="jsonManager", menuName ="Scriptables/Data/Json/JsonManager")]
    public class JsonManager : ScriptableObject, IJsonParser, IJsonSaver
    {
        public T ParseJson<T>(T jsonObj, string jsonPath)
        {
            string filePath = Path.Combine(Application.dataPath, jsonPath);
            string jsonString = File.ReadAllText(filePath); 
            jsonObj = JsonUtility.FromJson<T>(jsonString);
            return jsonObj;
        }

        public void SaveJson<T>(T jsonObj, string jsonPath, string fileName)
        {
            string filePath = Path.Combine(jsonPath, fileName + ".json");
            string jsonString = JsonUtility.ToJson(jsonObj);
            File.WriteAllText(filePath, jsonString);
        }
    }
}