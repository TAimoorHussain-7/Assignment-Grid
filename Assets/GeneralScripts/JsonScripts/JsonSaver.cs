using System.IO;
using UnityEngine;

namespace ProjectCore.Data.Json
{
    [CreateAssetMenu(fileName = "jsonSaver", menuName = "Scriptables/Data/Json/JsonSaver")]
    public class JsonSaver : ScriptableObject, IJsonSaver
    {
        public void SaveJson<T>(T jsonObj, string jsonPath, string fileName)
        {
            string filePath = Path.Combine(jsonPath, fileName + ".json");
            string jsonString = JsonUtility.ToJson(jsonObj);
            File.WriteAllText(filePath, jsonString);
        }
    }
}
