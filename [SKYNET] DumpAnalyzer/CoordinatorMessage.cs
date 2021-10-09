using NetHookAnalyzer2;
using ProtoBuf;
using ProtoBuf.Meta;
using SKYNET.Steam;
using SKYNET.Steam.GC;
using SteamKit2.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKYNET
{
    public class CoordinatorMessage
    {
        public CMsgGCClient GCClient;
        public IExtensible Body;
        public MsgGCHdrProtoBuf Header;
        public MemoryStream Payload { get; }
        public uint MsgType;

        public CoordinatorMessage(CMsgGCClient client)
        {
            GCClient = client;
            Header = new MsgGCHdrProtoBuf();
            Payload = new MemoryStream();
            MsgType =  MsgUtil.GetGCMsg(client.msgtype);

            Deserialize(client.payload);
        }
        public void Deserialize(byte[] data)
        {
            //throw new Exception(typeof(TBody).ToString());
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            MemoryStream ms = new MemoryStream(data);
            Header.Deserialize(ms);
            foreach (var type in MessageTypeFinder.GetGCMessageBodyTypeCandidates(GCClient.msgtype, GCClient.appid))
            {
                Body = Serializer.Deserialize(type, ms) as IExtensible;
            }
            // the rest of the data is the payload
            int payloadOffset = (int)ms.Position;
            int payloadLen = (int)(ms.Length - ms.Position);

            Payload.Write(data, payloadOffset, payloadLen);
        }

    }

}
