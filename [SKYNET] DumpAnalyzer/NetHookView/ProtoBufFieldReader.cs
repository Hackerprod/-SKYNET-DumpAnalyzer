using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;

namespace NetHookAnalyzer2
{
	internal class ProtoBufFieldReader
	{
		public static Dictionary<int, List<object>> ReadProtobuf(Stream stream)
		{
			using (ProtoReader protoReader = new ProtoReader(stream, null, null))
			{
				Dictionary<int, List<object>> dictionary = new Dictionary<int, List<object>>();
				while (true)
				{
					int num = protoReader.ReadFieldHeader();
					if (num == 0)
					{
						break;
					}
					object obj = null;
					switch (protoReader.WireType)
					{
					case WireType.Variant:
					case WireType.Fixed64:
					case WireType.Fixed32:
					case WireType.SignedVariant:
						try
						{
							obj = protoReader.ReadInt64();
						}
						catch (Exception)
						{
							obj = "Unable to read Variant (debugme)";
						}
						break;
					case WireType.String:
						try
						{
							obj = protoReader.ReadString();
						}
						catch (Exception)
						{
							obj = "Unable to read String (debugme)";
						}
						break;
					default:
						obj = $"{protoReader.WireType} is not implemented";
						break;
					}
					if (!dictionary.ContainsKey(num))
					{
						dictionary[num] = new List<object>();
					}
					dictionary[num].Add(obj);
				}
				if (dictionary.Count > 0)
				{
					return dictionary;
				}
			}
			return null;
		}
	}
}
