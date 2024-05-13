namespace ProjectCore.Data.Json
{
    public interface IJsonSaver
    {
        void SaveJson<T>(T jsonObj, string jsonPath, string fileName);
    }

    public interface IJsonParser
    {
        T ParseJson<T>(T jsonObj, string jsonPath);
    }
}
