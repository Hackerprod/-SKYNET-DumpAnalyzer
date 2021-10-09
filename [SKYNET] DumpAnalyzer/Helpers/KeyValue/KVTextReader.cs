using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SKYNET2
{
	internal class KVTextReader : StreamReader
	{
		internal static Dictionary<char, char> escapedMapping = new Dictionary<char, char>
	{
		{ '\\', '\\' },
		{ 'n', '\n' },
		{ 'r', '\r' },
		{ 't', '\t' }
	};

		public KVTextReader(KeyValue kv, Stream input) : base(input)
		{
			KeyValue currentKey = kv;
			do
			{
				bool wasQuoted;
				bool wasConditional;
				string s = ReadToken(out wasQuoted, out wasConditional);
				if (!string.IsNullOrEmpty(s))
				{
					if (currentKey == null)
					{
						currentKey = new KeyValue(s);
					}
					else
					{
						currentKey.Name = s;
					}
					s = ReadToken(out wasQuoted, out wasConditional);
					if (wasConditional)
					{
						s = ReadToken(out wasQuoted, out wasConditional);
					}
					if (s != null && s.StartsWith("{") && !wasQuoted)
					{
						currentKey.RecursiveLoadFromBuffer(this);
						currentKey = null;
						continue;
					}
					throw new Exception("LoadFromBuffer: missing {");
				}
				break;
			}
			while (!base.EndOfStream);
		}

		private void EatWhiteSpace()
		{
			while (!base.EndOfStream && char.IsWhiteSpace((char)Peek()))
			{
				Read();
			}
		}

		private bool EatCPPComment()
		{
			if (!base.EndOfStream)
			{
				if ((ushort)Peek() == 47)
				{
					ReadLine();
					return true;
				}
				return false;
			}
			return false;
		}

		public string ReadToken(out bool wasQuoted, out bool wasConditional)
		{
			wasQuoted = false;
			wasConditional = false;
			do
			{
				EatWhiteSpace();
				if (base.EndOfStream)
				{
					return null;
				}
			}
			while (EatCPPComment());
			if (base.EndOfStream)
			{
				return null;
			}
			char next = (char)Peek();
			switch (next)
			{
				case '"':
					{
						wasQuoted = true;
						Read();
						StringBuilder sb = new StringBuilder();
						while (!base.EndOfStream)
						{
							if (Peek() == 92)
							{
								Read();
								char escapedChar = (char)Read();
								char replacedChar;
								if (escapedMapping.TryGetValue(escapedChar, out replacedChar))
								{
									sb.Append(replacedChar);
								}
								else
								{
									sb.Append(escapedChar);
								}
							}
							else
							{
								if (Peek() == 34)
								{
									break;
								}
								sb.Append((char)Read());
							}
						}
						Read();
						return sb.ToString();
					}
				case '{':
				case '}':
					Read();
					return next.ToString();
				default:
					{
						bool bConditionalStart = false;
						int count = 0;
						StringBuilder ret = new StringBuilder();
						while (!base.EndOfStream)
						{
							next = (char)Peek();
							if (next == '"' || next == '{' || next == '}')
							{
								break;
							}
							if (next == '[')
							{
								bConditionalStart = true;
							}
							if (next == ']' && bConditionalStart)
							{
								wasConditional = true;
							}
							if (char.IsWhiteSpace(next))
							{
								break;
							}
							if (count < 1023)
							{
								ret.Append(next);
								Read();
								continue;
							}
							throw new Exception("ReadToken overflow");
						}
						return ret.ToString();
					}
			}
		}
	}
}