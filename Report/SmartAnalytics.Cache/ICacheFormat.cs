namespace SmartAnalytics.Cache
{
    public interface ICacheFormat
    {
        string Serialize<T>(T t);
        
        T Deserialize<T>(string text);
    }
}
