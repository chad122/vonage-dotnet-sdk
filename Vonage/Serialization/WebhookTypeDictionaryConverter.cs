using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Vonage.Common;

namespace Vonage.Serialization
{
    public class WebhookTypeDictionaryConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return TryGetEnumType(objectType, out _);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var result = new Dictionary<Webhook.Type, Webhook>();
            var jObject = JObject.Load(reader);

            foreach (var x in jObject)
            {
                Webhook.Type key = x.Key.ToEnum<Webhook.Type>();
                Webhook value = (Webhook) x.Value.ToObject(typeof(Webhook));
                result.Add(key, value);
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // handle null
            if (value is null)
            {
                writer.WriteNull();
                return;
            }

            // get dictionary & key type
            if (value is not IDictionary dictionary || !TryGetEnumType(value.GetType(), out Type enumType))
                throw new InvalidOperationException(
                    $"Can't parse value type '{value.GetType().FullName}' as a supported dictionary type."); // shouldn't be possible since we check in CanConvert

            Type enumValueType = Enum.GetUnderlyingType(enumType);

            // serialize
            writer.WriteStartObject();
            foreach (DictionaryEntry pair in dictionary)
            {
                StringBuilder sb = new StringBuilder();
                using (TextWriter textWriter = new StringWriter(sb))
                {
                    serializer.Serialize(textWriter, pair.Key, enumValueType);
                }

                var propertyName = sb.ToString().Replace("\"", "");
                writer.WritePropertyName(propertyName);
                serializer.Serialize(writer, pair.Value);
            }

            writer.WriteEndObject();
        }

        private bool TryGetEnumType(Type objectType, out Type keyType)
        {
            // ignore if type can't be dictionary
            if (!objectType.IsGenericType || objectType.IsValueType)
            {
                keyType = null;
                return false;
            }

            // ignore if not a supported dictionary
            {
                Type genericType = objectType.GetGenericTypeDefinition();
                if (genericType != typeof(IDictionary<,>) && genericType != typeof(Dictionary<,>))
                {
                    keyType = null;
                    return false;
                }
            }

            // extract key type
            keyType = objectType.GetGenericArguments().First();
            if (!keyType.IsEnum)
                keyType = null;

            return keyType != null;
        }

        
    }
}