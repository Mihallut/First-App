using System.Text.Json;
using System.Text.Json.Serialization;

namespace First_App.Server.Helpers
{
    public class NullableEnumConverter<T> : JsonConverter<T?> where T : struct, Enum
    {
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            string value = reader.GetString();

            if (string.IsNullOrEmpty(value) || !Enum.TryParse<T>(value, true, out var result))
            {
                return null;
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString());
        }
    }

}
