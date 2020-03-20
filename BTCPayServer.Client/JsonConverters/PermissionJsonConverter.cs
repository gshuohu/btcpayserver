﻿using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using NBitcoin.JsonConverters;

namespace BTCPayServer.Client.JsonConverters
{
    public class PermissionJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Permission).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            if (reader.TokenType != JsonToken.String)
                throw new JsonObjectException("Type 'Permission' is expected to be a 'String'", reader);
            if (reader.ReadAsString() is String s && Permission.TryParse(s, out var permission))
                return permission;
            throw new JsonObjectException("Invalid 'Permission' String", reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is Permission v)
                writer.WriteValue(v.ToString());
        }
    }
}