using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace ProjectCore.Data.Json
{
    [CreateAssetMenu(fileName = "jsonParserGrid", menuName = "Scriptables/Data/Json/jsonParserGrid")]

    public class JsonParserGridData : JsonParser
    {
        public override T ParseJson<T>(T jsonObj, string jsonPath)
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
    }
}
