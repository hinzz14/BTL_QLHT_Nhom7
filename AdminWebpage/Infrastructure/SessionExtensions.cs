using Newtonsoft.Json;
using System.Text.Json;
namespace AdminWebpage.Infrastructure
{
    public static class SessionExtensions
    {
        //public static void SetJson(this ISession session, string key, object value)
        //{
        //    session.SetString(key, JsonSerializer.Serialize(value));
        //}

        //public static T? GetJson<T>(this ISession session, string key)
        //{
        //    var sesionData = session.GetString(key);
        //    return sesionData == null ? default(T) : JsonSerializer.Deserialize<T>(sesionData);
        //}
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T? GetJson<T>(this ISession session, string key)
        {
            var sesionData = session.GetString(key);
            return sesionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sesionData);
        }
    }
}


