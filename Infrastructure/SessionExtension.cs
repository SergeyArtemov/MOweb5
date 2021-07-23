using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using MOweb.Controllers;

namespace MOweb.Infrastructure
{
    public static class SessionExtension  // для сохранения состояний сессий
    {

        public static void SetJson(this ISession session, string key, object value)
        {

            string json111 = JsonConvert.SerializeObject(value);
            session.SetString(key, json111/*JsonConvert.SerializeObject(value)*/);
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null
                    ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}
