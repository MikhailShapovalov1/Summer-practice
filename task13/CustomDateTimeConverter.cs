using System.Text.Json;
using System.Text.Json.Serialization;

namespace task13
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private const string FORMAT = "yyyy-MM-dd";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString()!, FORMAT, null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(FORMAT));
        }
    }
}
