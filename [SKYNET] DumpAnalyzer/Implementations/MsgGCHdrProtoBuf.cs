using ProtoBuf;
using SKYNET;
using SKYNET.Steam;
using System.IO;

namespace NetHookAnalyzer2.Implementations
{
	public class MsgGCHdrProtoBuf : Implementations.IGCSerializableHeader, ISteamSerializable
	{
		public uint Msg { get; set; }

		public int HeaderLength { get; set; }

		public CMsgProtoBufHeader Proto { get; set; }

		public void SetEMsg(uint msg)
		{
			Msg = msg;
		}

		public MsgGCHdrProtoBuf()
		{
			Msg = 0u;
			HeaderLength = 0;
			Proto = new CMsgProtoBufHeader();
		}

		public void Serialize(Stream stream)
		{
			MemoryStream msProto = new MemoryStream();
			Serializer.Serialize(msProto, Proto);
			HeaderLength = (int)msProto.Length;
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(MsgUtil.MakeGCMsg(Msg, true));
			binaryWriter.Write(HeaderLength);
			binaryWriter.Write(msProto.ToArray());
			msProto.Dispose();
		}

		public void Deserialize(Stream stream)
		{
			BinaryReader br = new BinaryReader(stream);
			Msg = MsgUtil.GetGCMsg(br.ReadUInt32());
			HeaderLength = br.ReadInt32();
			using (MemoryStream msProto = new MemoryStream(br.ReadBytes(HeaderLength)))
			{
				Proto = Serializer.Deserialize<CMsgProtoBufHeader>(msProto);
			}
		}
	}
}