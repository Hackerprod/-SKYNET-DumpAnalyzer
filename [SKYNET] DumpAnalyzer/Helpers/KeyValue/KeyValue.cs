using SKYNET.Steam;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKYNET2
{
	public class KeyValue
	{

		private enum Type : byte
		{
			None = 0,
			String = 1,
			Int32 = 2,
			Float32 = 3,
			Pointer = 4,
			WideString = 5,
			Color = 6,
			UInt64 = 7,
			End = 8,
			Int64 = 10
		}

		/// <summary>
		/// Represents an invalid <see cref="T:SteamKit2.KeyValue" /> given when a searched for child does not exist.
		/// </summary>
		public static readonly KeyValue Invalid = new KeyValue();

		/// <summary>
		/// Gets or sets the name of this instance.
		/// </summary>
		
		
		public string Name
		{
	
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the value of this instance.
		/// </summary>
		
		
		public string Value
		{
	
			get; 
			set;
		}

		/// <summary>
		/// Gets the children of this instance.
		/// </summary>
		public List<KeyValue> Children { get; private set; }

		/// <summary>
		/// Gets or sets the child <see cref="T:SteamKit2.KeyValue" /> with the specified key.
		/// When retrieving by key, if no child with the given key exists, <see cref="F:SteamKit2.KeyValue.Invalid" /> is returned.
		/// </summary>
		public KeyValue this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				KeyValue child = Children.FirstOrDefault((KeyValue c) => string.Equals(c.Name, key, StringComparison.OrdinalIgnoreCase));
				if (child == null)
				{
					return Invalid;
				}
				return child;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				KeyValue existingChild = Children.FirstOrDefault((KeyValue c) => string.Equals(c.Name, key, StringComparison.OrdinalIgnoreCase));
				if (existingChild != null)
				{
					Children.Remove(existingChild);
				}
				value.Name = key;
				Children.Add(value);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:SteamKit2.KeyValue" /> class.
		/// </summary>
		/// <param name="name">The optional name of the root key.</param>
		/// <param name="value">The optional value assigned to the root key.</param>

		public KeyValue(string name = null, string value = null)
		{
			Name = name;
			Value = value;
			Children = new List<KeyValue>();
		}

		/// <summary>
		/// Returns the value of this instance as a string.
		/// </summary>
		/// <returns>The value of this instance as a string.</returns>

		public string AsString()
		{
			return Value;
		}

		/// <summary>
		/// Attempts to convert and return the value of this instance as an unsigned byte.
		/// If the conversion is invalid, the default value is returned.
		/// </summary>
		/// <param name="defaultValue">The default value to return if the conversion is invalid.</param>
		/// <returns>The value of this instance as an unsigned byte.</returns>
		public byte AsUnsignedByte(byte defaultValue = 0)
		{
			byte value;
			if (!byte.TryParse(Value, out value))
			{
				return defaultValue;
			}
			return value;
		}

		/// <summary>
		/// Attempts to convert and return the value of this instance as an unsigned short.
		/// If the conversion is invalid, the default value is returned.
		/// </summary>
		/// <param name="defaultValue">The default value to return if the conversion is invalid.</param>
		/// <returns>The value of this instance as an unsigned short.</returns>
		public ushort AsUnsignedShort(ushort defaultValue = 0)
		{
			ushort value;
			if (!ushort.TryParse(Value, out value))
			{
				return defaultValue;
			}
			return value;
		}

		/// <summary>
		/// Attempts to convert and return the value of this instance as an integer.
		/// If the conversion is invalid, the default value is returned.
		/// </summary>
		/// <param name="defaultValue">The default value to return if the conversion is invalid.</param>
		/// <returns>The value of this instance as an integer.</returns>
		public int AsInteger(int defaultValue = 0)
		{
			int value;
			if (!int.TryParse(Value, out value))
			{
				return defaultValue;
			}
			return value;
		}

		/// <summary>
		/// Attempts to convert and return the value of this instance as an unsigned integer.
		/// If the conversion is invalid, the default value is returned.
		/// </summary>
		/// <param name="defaultValue">The default value to return if the conversion is invalid.</param>
		/// <returns>The value of this instance as an unsigned integer.</returns>
		public uint AsUnsignedInteger(uint defaultValue = 0u)
		{
			uint value;
			if (!uint.TryParse(Value, out value))
			{
				return defaultValue;
			}
			return value;
		}

		/// <summary>
		/// Attempts to convert and return the value of this instance as a long.
		/// If the conversion is invalid, the default value is returned.
		/// </summary>
		/// <param name="defaultValue">The default value to return if the conversion is invalid.</param>
		/// <returns>The value of this instance as a long.</returns>
		public long AsLong(long defaultValue = 0L)
		{
			long value;
			if (!long.TryParse(Value, out value))
			{
				return defaultValue;
			}
			return value;
		}

		/// <summary>
		/// Attempts to convert and return the value of this instance as an unsigned long.
		/// If the conversion is invalid, the default value is returned.
		/// </summary>
		/// <param name="defaultValue">The default value to return if the conversion is invalid.</param>
		/// <returns>The value of this instance as an unsigned long.</returns>
		public ulong AsUnsignedLong(ulong defaultValue = 0uL)
		{
			ulong value;
			if (!ulong.TryParse(Value, out value))
			{
				return defaultValue;
			}
			return value;
		}

		/// <summary>
		/// Attempts to convert and return the value of this instance as a float.
		/// If the conversion is invalid, the default value is returned.
		/// </summary>
		/// <param name="defaultValue">The default value to return if the conversion is invalid.</param>
		/// <returns>The value of this instance as a float.</returns>
		public float AsFloat(float defaultValue = 0f)
		{
			float value;
			if (!float.TryParse(Value, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out value))
			{
				return defaultValue;
			}
			return value;
		}

		/// <summary>
		/// Attempts to convert and return the value of this instance as a boolean.
		/// If the conversion is invalid, the default value is returned.
		/// </summary>
		/// <param name="defaultValue">The default value to return if the conversion is invalid.</param>
		/// <returns>The value of this instance as a boolean.</returns>
		public bool AsBoolean(bool defaultValue = false)
		{
			int value;
			if (!int.TryParse(Value, out value))
			{
				return defaultValue;
			}
			return value != 0;
		}

		/// <summary>
		/// Attempts to convert and return the value of this instance as an enum.
		/// If the conversion is invalid, the default value is returned.
		/// </summary>
		/// <param name="defaultValue">The default value to return if the conversion is invalid.</param>
		/// <returns>The value of this instance as an enum.</returns>

		public T AsEnum<T>(T defaultValue = default(T)) where T : struct
		{
			T value;
			if (!Enum.TryParse<T>(Value, out value))
			{
				return defaultValue;
			}
			return value;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return $"{Name} = {Value}";
		}

		/// <summary>
		/// Attempts to load the given filename as a text <see cref="T:SteamKit2.KeyValue" />.
		/// </summary>
		/// <param name="path">The path to the file to load.</param>
		/// <returns>a <see cref="T:SteamKit2.KeyValue" /> instance if the load was successful, or <c>null</c> on failure.</returns>
		/// <remarks>
		/// This method will swallow any exceptions that occur when reading, use <see cref="M:SteamKit2.KeyValue.ReadAsText(System.IO.Stream)" /> if you wish to handle exceptions.
		/// </remarks>

		public static KeyValue LoadAsText(string path)
		{
			return LoadFromFile(path, false);
		}

		/// <summary>
		/// Attempts to load the given filename as a binary <see cref="T:SteamKit2.KeyValue" />.
		/// </summary>
		/// <param name="path">The path to the file to load.</param>
		/// <param name="keyValue">The resulting <see cref="T:SteamKit2.KeyValue" /> object if the load was successful, or <c>null</c> if unsuccessful.</param>
		/// <returns><c>true</c> if the load was successful, or <c>false</c> on failure.</returns>
		public static bool TryLoadAsBinary(string path, out KeyValue keyValue)
		{
			keyValue = LoadFromFile(path, true);
			return keyValue != null;
		}


		private static KeyValue LoadFromFile(string path, bool asBinary)
		{
			if (!File.Exists(path))
			{
				return null;
			}
			try
			{
				using (FileStream input = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					KeyValue kv = new KeyValue();
					if (asBinary)
					{
						if (!kv.TryReadAsBinary(input))
						{
							return null;
						}
					}
					else if (!kv.ReadAsText(input))
					{
						return null;
					}
					return kv;
				}
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		/// Attempts to create an instance of <see cref="T:SteamKit2.KeyValue" /> from the given input text.
		/// </summary>
		/// <param name="input">The input text to load.</param>
		/// <returns>a <see cref="T:SteamKit2.KeyValue" /> instance if the load was successful, or <c>null</c> on failure.</returns>
		/// <remarks>
		/// This method will swallow any exceptions that occur when reading, use <see cref="M:SteamKit2.KeyValue.ReadAsText(System.IO.Stream)" /> if you wish to handle exceptions.
		/// </remarks>

		public static KeyValue LoadFromString(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(input)))
			{
				KeyValue kv = new KeyValue();
				try
				{
					if (!kv.ReadAsText(stream))
					{
						return null;
					}
					return kv;
				}
				catch (Exception)
				{
					return null;
				}
			}
		}

		/// <summary>
		/// Populate this instance from the given <see cref="T:System.IO.Stream" /> as a text <see cref="T:SteamKit2.KeyValue" />.
		/// </summary>
		/// <param name="input">The input <see cref="T:System.IO.Stream" /> to read from.</param>
		/// <returns><c>true</c> if the read was successful; otherwise, <c>false</c>.</returns>
		public bool ReadAsText(Stream input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			Children = new List<KeyValue>();
			using (new KVTextReader(this, input))
			{
				return true;
			}
		}

		/// <summary>
		/// Opens and reads the given filename as text.
		/// </summary>
		/// <seealso cref="M:SteamKit2.KeyValue.ReadAsText(System.IO.Stream)" />
		/// <param name="filename">The file to open and read.</param>
		/// <returns><c>true</c> if the read was successful; otherwise, <c>false</c>.</returns>
		public bool ReadFileAsText(string filename)
		{
			using (FileStream fs = new FileStream(filename, FileMode.Open))
			{
				return ReadAsText(fs);
			}
		}

		internal void RecursiveLoadFromBuffer(KVTextReader kvr)
		{
			while (true)
			{
				bool wasQuoted;
				bool wasConditional;
				string name = kvr.ReadToken(out wasQuoted, out wasConditional);
				if (name == null || name.Length == 0)
				{
					throw new InvalidDataException("RecursiveLoadFromBuffer: got EOF or empty keyname");
				}
				if (name.StartsWith("}") && !wasQuoted)
				{
					break;
				}
				KeyValue dat = new KeyValue(name);
				dat.Children = new List<KeyValue>();
				Children.Add(dat);
				string value = kvr.ReadToken(out wasQuoted, out wasConditional);
				if (wasConditional && value != null)
				{
					value = kvr.ReadToken(out wasQuoted, out wasConditional);
				}
				if (value == null)
				{
					throw new Exception("RecursiveLoadFromBuffer:  got NULL key");
				}
				if (value.StartsWith("}") && !wasQuoted)
				{
					throw new Exception("RecursiveLoadFromBuffer:  got } in key");
				}
				if (value.StartsWith("{") && !wasQuoted)
				{
					dat.RecursiveLoadFromBuffer(kvr);
					continue;
				}
				if (wasConditional)
				{
					throw new Exception("RecursiveLoadFromBuffer:  got conditional between key and value");
				}
				dat.Value = value;
			}
		}

		/// <summary>
		/// Saves this instance to file.
		/// </summary>
		/// <param name="path">The file path to save to.</param>
		/// <param name="asBinary">If set to <c>true</c>, saves this instance as binary.</param>
		public void SaveToFile(string path, bool asBinary)
		{
			using (FileStream f = File.Create(path))
			{
				SaveToStream(f, asBinary);
			}
		}

		/// <summary>
		/// Saves this instance to a given <see cref="T:System.IO.Stream" />.
		/// </summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> to save to.</param>
		/// <param name="asBinary">If set to <c>true</c>, saves this instance as binary.</param>
		public void SaveToStream(Stream stream, bool asBinary)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (asBinary)
			{
				RecursiveSaveBinaryToStream(stream);
			}
			else
			{
				RecursiveSaveTextToFile(stream);
			}
		}

		private void RecursiveSaveBinaryToStream(Stream f)
		{
			RecursiveSaveBinaryToStreamCore(f);
			f.WriteByte(8);
		}

		private void RecursiveSaveBinaryToStreamCore(Stream f)
		{
			if (Value == null)
			{
				f.WriteByte(0);
				f.WriteNullTermString(GetNameForSerialization(), Encoding.UTF8);
				foreach (KeyValue child in Children)
				{
					child.RecursiveSaveBinaryToStreamCore(f);
				}
				f.WriteByte(8);
			}
			else
			{
				f.WriteByte(1);
				f.WriteNullTermString(GetNameForSerialization(), Encoding.UTF8);
				f.WriteNullTermString(Value, Encoding.UTF8);
			}
		}

		private void RecursiveSaveTextToFile(Stream stream, int indentLevel = 0)
		{
			WriteIndents(stream, indentLevel);
			WriteString(stream, GetNameForSerialization(), true);
			WriteString(stream, "\n");
			WriteIndents(stream, indentLevel);
			WriteString(stream, "{\n");
			foreach (KeyValue child in Children)
			{
				if (child.Value == null)
				{
					child.RecursiveSaveTextToFile(stream, indentLevel + 1);
					continue;
				}
				WriteIndents(stream, indentLevel + 1);
				WriteString(stream, child.GetNameForSerialization(), true);
				WriteString(stream, "\t\t");
				WriteString(stream, EscapeText(child.Value), true);
				WriteString(stream, "\n");
			}
			WriteIndents(stream, indentLevel);
			WriteString(stream, "}\n");
		}

		private static string EscapeText(string value)
		{
			foreach (KeyValuePair<char, char> kvp in KVTextReader.escapedMapping)
			{
				string textToReplace = new string(kvp.Value, 1);
				string escapedReplacement = "\\" + kvp.Key;
				value = value.Replace(textToReplace, escapedReplacement);
			}
			return value;
		}

		private void WriteIndents(Stream stream, int indentLevel)
		{
			WriteString(stream, new string('\t', indentLevel));
		}

		private static void WriteString(Stream stream, string str, bool quote = false)
		{
			byte[] bytes = Encoding.UTF8.GetBytes((quote ? "\"" : "") + str.Replace("\"", "\\\"") + (quote ? "\"" : ""));
			stream.Write(bytes, 0, bytes.Length);
		}

		/// <summary>
		/// Populate this instance from the given <see cref="T:System.IO.Stream" /> as a binary <see cref="T:SteamKit2.KeyValue" />.
		/// </summary>
		/// <param name="input">The input <see cref="T:System.IO.Stream" /> to read from.</param>
		/// <returns><c>true</c> if the read was successful; otherwise, <c>false</c>.</returns>
		public bool TryReadAsBinary(Stream input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return TryReadAsBinaryCore(input, this, null);
		}

		private static bool TryReadAsBinaryCore(Stream input, KeyValue current,  KeyValue parent)
		{
			current.Children = new List<KeyValue>();
			while (true)
			{
				Type type = (Type)input.ReadByte();
				if (type == Type.End)
				{
					break;
				}
				current.Name = input.ReadNullTermString(Encoding.UTF8);
				switch (type)
				{
					case Type.None:
						{
							KeyValue child = new KeyValue();
							if (!TryReadAsBinaryCore(input, child, current))
							{
								return false;
							}
							break;
						}
					case Type.String:
						current.Value = input.ReadNullTermString(Encoding.UTF8);
						break;
					case Type.WideString:
						return false;
					case Type.Int32:
					case Type.Pointer:
					case Type.Color:
						current.Value = Convert.ToString(input.ReadInt32());
						break;
					case Type.UInt64:
						current.Value = Convert.ToString(input.ReadUInt64());
						break;
					case Type.Float32:
						current.Value = Convert.ToString(input.ReadFloat());
						break;
					case Type.Int64:
						current.Value = Convert.ToString(input.ReadInt64());
						break;
					default:
						return false;
				}
				parent?.Children.Add(current);
				current = new KeyValue();
			}
			return true;
		}

		private string GetNameForSerialization()
		{
			if (Name == null)
			{
				throw new InvalidOperationException("Cannot serialise a KeyValue object with a null name!");
			}
			return Name;
		}
	}
}
