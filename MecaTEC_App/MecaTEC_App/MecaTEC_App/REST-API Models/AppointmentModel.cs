﻿namespace MecaTEC_App.REST_API_AppointmentModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;
    /// <summary>
    /// Class that packs recipe objects into an array
    /// author Jose Antonio Espinoza.
    /// </summary>
    public partial class AppointmentModel
    {
        [JsonProperty("newbill")]
        public Appointment[] Appointments { get; set; }
    }
    /// <summary>
    /// Class for creating a Recipe object from a json file.
    /// author Jose Antonio Espinoza.
    /// </summary>
    public partial class Appointment
    {
        [JsonProperty("service")]
        public string Service { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("parts")]
        public JArray Parts { get; set; }

        [JsonProperty("mechanic")]
        public string Mechanic { get; set; }

    }

    public partial class BillModel
    {
        /// <summary>
        /// Methods that converts a json file to an object type
        /// author Jose Antonio Espinoza.
        /// </summary>
        public static BillModel FromJson(string json) => JsonConvert.DeserializeObject<BillModel>(json, MecaTEC_App.REST_API_BillModel.Converter.Settings);
    }

    public static class Serialize
    {
        /// <summary>
        /// Methods that converts an object type to a json file
        /// author Jose Antonio Espinoza.
        /// </summary>
        public static string ToJson(this BillModel self) => JsonConvert.SerializeObject(self, MecaTEC_App.REST_API_BillModel.Converter.Settings);
    }
    /// <summary>
    /// Methods autogenerated by Newtonsoft.json
    /// author Jose Antonio Espinoza.
    /// </summary>
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }
        /// <summary>
        /// Creates singleton instance of the string parser.
        /// author Jose Antonio Espinoza.
        /// </summary>
        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}