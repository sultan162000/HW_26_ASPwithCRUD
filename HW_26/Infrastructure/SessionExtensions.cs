using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HW_26.Infrastructure
{
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session,string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionDate = session.GetString(key);
            return sessionDate == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionDate);
        }
    }
}
