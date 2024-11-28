using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Group9_FinalProject.Extensions
{
    public static class SessionExtensions
    {
        // Method to save an object as a JSON string in the session
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            // Serialize the object into a JSON string and store it in session
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // Method to retrieve an object from the session by deserializing the JSON string
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            // Get the serialized string from session
            var value = session.GetString(key);
            // If there's no value, return the default value for the type
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
