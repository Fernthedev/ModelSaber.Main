using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModelSaber.Main.Helpers
{
    public class JsonConverters
    {
        public static JsonConverter[] Converters = { new UlongJsonConverter(), new NullableUlongJsonConverter() };
    }

    public class UlongJsonConverter : JsonConverter<ulong>
    {
        public override ulong Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => Convert.ToUInt64(reader.GetString());

        public override void Write(Utf8JsonWriter writer, ulong value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString());
    }

    public class NullableUlongJsonConverter : JsonConverter<ulong?>
    {
        public override ulong? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            return string.IsNullOrWhiteSpace(str) ? null : Convert.ToUInt64(str);
        }

        public override void Write(Utf8JsonWriter writer, ulong? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteStringValue(value.ToString());
            else
                writer.WriteNullValue();
        }
    }
}
