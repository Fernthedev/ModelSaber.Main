using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ModelSaber.Database.Models
{
    public class ModelSaberOld
    {
        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("thumbnail")]
        public ThumbnailUnion Thumbnail { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("bsaber")]
        public string BSaber { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("discordid")]
        public string DiscordId { get; set; }

        [JsonProperty("discord")]
        public string Discord { get; set; }

        [JsonProperty("variationid")]
        public long? VariationId { get; set; }

        [JsonProperty("platform")]
        public Platform Platform { get; set; }

        [JsonProperty("download")]
        public Uri Download { get; set; }

        [JsonProperty("install_link")]
        public string InstallLink { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }
    }

    public struct ThumbnailUnion
    {
        public ThumbnailEnum? Enum;
        public Uri PurpleUri;

        public static implicit operator ThumbnailUnion(ThumbnailEnum @enum) => new() { Enum = @enum };
        public static implicit operator ThumbnailUnion(Uri purpleUri) => new() { PurpleUri = purpleUri };
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new()
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                PlatformConverter.Singleton,
                StatusConverter.Singleton,
                ThumbnailUnionConverter.Singleton,
                ThumbnailEnumConverter.Singleton,
                TypeEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            }
        };
    }

    internal class PlatformConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Platform) || t == typeof(Platform?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "pc")
            {
                return Platform.Pc;
            }
            throw new Exception("Cannot unmarshal type Platform");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Platform)untypedValue;
            if (value == Platform.Pc)
            {
                serializer.Serialize(writer, "pc");
                return;
            }
            throw new Exception("Cannot marshal type Platform");
        }

        public static readonly PlatformConverter Singleton = new();
    }

    internal class StatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "approved")
            {
                return Status.Approved;
            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Status)untypedValue;
            if (value == Status.Approved)
            {
                serializer.Serialize(writer, "approved");
                return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new();
    }

    internal class ThumbnailUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ThumbnailUnion) || t == typeof(ThumbnailUnion?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    switch (stringValue)
                    {
                        case "image.JPG":
                            return new ThumbnailUnion { Enum = ThumbnailEnum.Image };
                        case "image.PNG":
                            return new ThumbnailUnion { Enum = ThumbnailEnum.Image };
                        case "image.gif":
                            return new ThumbnailUnion { Enum = ThumbnailEnum.Video };
                        case "image.jpg":
                            return new ThumbnailUnion { Enum = ThumbnailEnum.Image };
                        case "image.png":
                            return new ThumbnailUnion { Enum = ThumbnailEnum.Image };
                    }
                    try
                    {
                        var uri = new Uri(stringValue);
                        return new ThumbnailUnion { PurpleUri = uri };
                    }
                    catch (UriFormatException) { }
                    break;
            }
            throw new Exception("Cannot unmarshal type ThumbnailUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ThumbnailUnion)untypedValue;
            if (value.Enum != null)
            {
                switch (value.Enum)
                {
                    case ThumbnailEnum.Video:
                        serializer.Serialize(writer, "image.webm");
                        return;
                    case ThumbnailEnum.Image:
                        serializer.Serialize(writer, "image.webp");
                        return;
                }
            }
            if (value.PurpleUri != null)
            {
                serializer.Serialize(writer, value.PurpleUri.ToString());
                return;
            }
            throw new Exception("Cannot marshal type ThumbnailUnion");
        }

        public static readonly ThumbnailUnionConverter Singleton = new();
    }

    internal class ThumbnailEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ThumbnailEnum) || t == typeof(ThumbnailEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "image.JPG":
                    return ThumbnailEnum.Image;
                case "image.PNG":
                    return ThumbnailEnum.Image;
                case "image.gif":
                    return ThumbnailEnum.Video;
                case "image.jpg":
                    return ThumbnailEnum.Image;
                case "image.png":
                    return ThumbnailEnum.Image;
            }
            throw new Exception("Cannot unmarshal type ThumbnailEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ThumbnailEnum)untypedValue;
            switch (value)
            {
                case ThumbnailEnum.Video:
                    serializer.Serialize(writer, "image.webm");
                    return;
                case ThumbnailEnum.Image:
                    serializer.Serialize(writer, "image.webp");
                    return;
            }
            throw new Exception("Cannot marshal type ThumbnailEnum");
        }

        public static readonly ThumbnailEnumConverter Singleton = new();
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "avatar":
                    return TypeEnum.Avatar;
                case "bloq":
                    return TypeEnum.Note;
                case "platform":
                    return TypeEnum.Platform;
                case "saber":
                    return TypeEnum.Saber;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            switch (value)
            {
                case TypeEnum.Avatar:
                    serializer.Serialize(writer, "avatar");
                    return;
                case TypeEnum.Note:
                    serializer.Serialize(writer, "bloq");
                    return;
                case TypeEnum.Platform:
                    serializer.Serialize(writer, "platform");
                    return;
                case TypeEnum.Saber:
                    serializer.Serialize(writer, "saber");
                    return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new();
    }
}
