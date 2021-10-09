using SKYNET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetHookAnalyzer2.Implementations
{
	public class MsgHdr : ISteamSerializableHeader, ISteamSerializable
	{
		public EMsg Msg { get; set; }

		public ulong TargetJobID { get; set; }

		public ulong SourceJobID { get; set; }

		public void SetEMsg(EMsg msg)
		{
			Msg = msg;
		}

		public MsgHdr()
		{
			Msg = EMsg.k_EMsgInvalid;
			TargetJobID = ulong.MaxValue;
			SourceJobID = ulong.MaxValue;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write((int)Msg);
			binaryWriter.Write(TargetJobID);
			binaryWriter.Write(SourceJobID);
		}

		public void Deserialize(Stream stream)
		{
			BinaryReader br = new BinaryReader(stream);
			Msg = (EMsg)br.ReadInt32();
			TargetJobID = br.ReadUInt64();
			SourceJobID = br.ReadUInt64();
		}
	}
}
