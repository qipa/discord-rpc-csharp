﻿using DiscordRPC.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordRPC.Serialization
{
	public class EnumSnakeCaseConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType.IsEnum;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.Value == null) return null;
			return TryParseEnum(objectType, (string)reader.Value);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var enumtype = value.GetType();
			var name =	enumtype.GetEnumName(value);
			writer.WriteValue(name.ToSnakeCase());
		}


		public object TryParseEnum(Type enumType, string str)
		{
			if (str == null)
				return null;

			Type type = enumType;
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
				type = type.GetGenericArguments().First();

            string line = str.ToCamelCase();
			return Enum.Parse(type, line, ignoreCase: true);
		}
		
	}
}
