using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class StreamHelpers
{
	[ThreadStatic]
	private static byte[] data;

	private static void EnsureInitialized()
	{
		if (data == null)
		{
			data = new byte[8];
		}
	}

	public static short ReadInt16(this Stream stream)
	{
		EnsureInitialized();
		stream.Read(data, 0, 2);
		return BitConverter.ToInt16(data, 0);
	}

	public static ushort ReadUInt16(this Stream stream)
	{
		EnsureInitialized();
		stream.Read(data, 0, 2);
		return BitConverter.ToUInt16(data, 0);
	}

	public static int ReadInt32(this Stream stream)
	{
		EnsureInitialized();
		stream.Read(data, 0, 4);
		return BitConverter.ToInt32(data, 0);
	}

	public static long ReadInt64(this Stream stream)
	{
		EnsureInitialized();
		stream.Read(data, 0, 8);
		return BitConverter.ToInt64(data, 0);
	}

	public static uint ReadUInt32(this Stream stream)
	{
		EnsureInitialized();
		stream.Read(data, 0, 4);
		return BitConverter.ToUInt32(data, 0);
	}

	public static ulong ReadUInt64(this Stream stream)
	{
		EnsureInitialized();
		stream.Read(data, 0, 8);
		return BitConverter.ToUInt64(data, 0);
	}

	public static float ReadFloat(this Stream stream)
	{
		EnsureInitialized();
		stream.Read(data, 0, 4);
		return BitConverter.ToSingle(data, 0);
	}

	public static string ReadNullTermString(this Stream stream, Encoding encoding)
	{
		int characterSize = encoding.GetByteCount("e");
		using (MemoryStream ms = new MemoryStream())
		{
			while (true)
			{
				byte[] data = new byte[characterSize];
				stream.Read(data, 0, characterSize);
				if (encoding.GetString(data, 0, characterSize) == "\0")
				{
					break;
				}
				ms.Write(data, 0, data.Length);
			}
			return encoding.GetString(ms.GetBuffer(), 0, (int)ms.Length);
		}
	}

	public static void WriteNullTermString(this Stream stream, string value, Encoding encoding)
	{
		int dataLength = encoding.GetByteCount(value);
		byte[] data = new byte[dataLength + 1];
		encoding.GetBytes(value, 0, value.Length, data, 0);
		data[dataLength] = 0;
		stream.Write(data, 0, data.Length);
	}
}

