using ProtoBuf;
using SKYNET;
using SKYNET.Steam;
using SteamKit2;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace NetHookAnalyzer2
{
	internal class NetHookItem
	{
		public enum PacketDirection
		{
			In,
			Out
		}

		private static Regex NameRegex = new Regex("(?<num>\\d+)_(?<direction>in|out)_(?<emsg>\\d+)_k_EMsg(?<name>[\\w_<>]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		private string innerMessageName;

		public string Name
		{
			get;
			private set;
		}

		public int Sequence
		{
			get;
			private set;
		}

		public DateTime Timestamp
		{
			get;
			private set;
		}

		public PacketDirection Direction
		{
			get;
			private set;
		}

		public EMsg EMsg
		{
			get;
			private set;
		}

		public string InnerMessageName => innerMessageName ?? (innerMessageName = ReadInnerMessageName());

		public FileInfo FileInfo
		{
			get;
			private set;
		}

		public Stream OpenStream()
		{
			return FileInfo.OpenRead();
		}

		public bool LoadFromFile(FileInfo fileInfo)
		{
			Match match = NameRegex.Match(fileInfo.Name);
			if (!match.Success)
			{
				return false;
			}
			if (!int.TryParse(match.Groups["num"].Value, out int result))
			{
				return false;
			}
			Timestamp = fileInfo.LastWriteTime;
			if (!Enum.TryParse(match.Groups["direction"].Value, ignoreCase: true, out PacketDirection result2))
			{
				return false;
			}
			if (!uint.TryParse(match.Groups["emsg"].Value, out uint result3))
			{
				return false;
			}
			FileInfo = fileInfo;
			Sequence = result;
			Direction = result2;
			EMsg = (EMsg)result3;
			Name = match.Groups["name"].Value;
			return true;
		}

		private string ReadInnerMessageName()
		{
			try
			{
				return ReadInnerMessageNameCore();
			}
			catch (IOException)
			{
				return null;
			}
		}

		private string ReadInnerMessageNameCore()
		{
			EMsg eMsg = EMsg;
			if (eMsg <= EMsg.k_EMsgServiceMethodCallFromClient)
			{
				//if ((uint)(eMsg - 146) <= 1u || eMsg == EMsg.ServiceMethodCallFromClient)
				//{
				//	byte[] buffer = File.ReadAllBytes(FileInfo.FullName);
				//	MsgHdrProtoBuf msgHdrProtoBuf = new MsgHdrProtoBuf();
				//	using (MemoryStream stream = new MemoryStream(buffer))
				//	{
				//		msgHdrProtoBuf.Deserialize(stream);
				//	}
				//	return msgHdrProtoBuf.Proto.target_job_name;
				//}
			}
			else
			{
				if ((uint)(eMsg - 5452) <= 1u)
				{
					ClientMsgProtobuf<CMsgGCClient> clientMsgProtobuf = ReadAsProtobufMsg<CMsgGCClient>();
					string text = EMsgExtensions.GetGCMessageName(clientMsgProtobuf.Body.msgtype, clientMsgProtobuf.Body.appid);
					string text2 = "k_EMsg";
					if (text.StartsWith(text2))
					{
						text = text.Substring(text2.Length);
					}
					return text;
				}
			}
			return string.Empty;
		}

		private ClientMsgProtobuf<T> ReadAsProtobufMsg<T>() where T : IExtensible, new()
		{
			byte[] data = File.ReadAllBytes(FileInfo.FullName);
			return new ClientMsgProtobuf<T>(new PacketClientMsgProtobuf(EMsg, data));
		}
	}
}
