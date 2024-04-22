using UnityEngine;

namespace ProjectCore.Data.Json
{
    [CreateAssetMenu(fileName = "jsonParser", menuName = "Scriptables/Data/Json/jsonParser")]

    public class JsonParser : ScriptableObject, IJsonParser
    {
       public virtual T ParseJson<T>(T jsonObj, string jsonPath)
        {
            string jsonString = Resources.Load<TextAsset>(jsonPath).text;
            jsonObj = JsonUtility.FromJson<T>(jsonString);
            return jsonObj;
        }
    }
}
