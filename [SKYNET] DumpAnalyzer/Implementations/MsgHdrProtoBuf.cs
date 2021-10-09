using ProtoBuf;
using SKYNET;
using SKYNET.Steam;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetHookAnalyzer2.Implementations
{
	public class MsgHdrProtoBuf : ISteamSerializableHeader, ISteamSerializable
	{
		public EMsg Msg { get; set; }

		public int HeaderLength { get; set; }

		public CMsgProtoBufHeader Proto { get; set; }

		public void SetEMsg(EMsg msg)
		{
			Msg = msg;
		}

		public MsgHdrProtoBuf()
		{
			Msg = EMsg.k_EMsgInvalid;
			HeaderLength = 0;
			Proto = new CMsgProtoBufHeader();
		}

		public void Serialize(Stream stream)
		{
			MemoryStream msProto = new MemoryStream();
			Serializer.Serialize(msProto, Proto);
			HeaderLength = (int)msProto.Length;
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write((int)MsgUtil.MakeMsg(Msg, true));
			binaryWriter.Write(HeaderLength);
			binaryWriter.Write(msProto.ToArray());
			msProto.Dispose();
		}

		public void Deserialize(Stream stream)
		{
			BinaryReader br = new BinaryReader(stream);
			Msg = MsgUtil.GetMsg((uint)br.ReadInt32());
			HeaderLength = br.ReadInt32();
			using (MemoryStream msProto = new MemoryStream(br.ReadBytes(HeaderLength)))
			{
				Proto = Serializer.Deserialize<CMsgProtoBufHeader>(msProto);
			}
		}
	}

}
