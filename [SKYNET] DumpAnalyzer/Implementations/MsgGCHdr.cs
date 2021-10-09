using System.IO;

namespace NetHookAnalyzer2.Implementations
{
	public class MsgGCHdr : IGCSerializableHeader, ISteamSerializable
	{
		public ushort HeaderVersion { get; set; }

		public ulong TargetJobID { get; set; }

		public ulong SourceJobID { get; set; }

		public void SetEMsg(uint msg)
		{
		}

		public MsgGCHdr()
		{
			HeaderVersion = 1;
			TargetJobID = ulong.MaxValue;
			SourceJobID = ulong.MaxValue;
		}

		public void Serialize(Stream stream)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(HeaderVersion);
			binaryWriter.Write(TargetJobID);
			binaryWriter.Write(SourceJobID);
		}

		public void Deserialize(Stream stream)
		{
			BinaryReader br = new BinaryReader(stream);
			HeaderVersion = br.ReadUInt16();
			TargetJobID = br.ReadUInt64();
			SourceJobID = br.ReadUInt64();
		}
	}
}