using ProtoBuf;
using SKYNET;
using SKYNET.Steam;
using SKYNET.Steam.GC;
using SteamKit2;
using SteamKit2.GC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NetHookAnalyzer2
{
	internal class MessageTypeFinder
	{
		private static Type SteamKit2MarkerType => typeof(ClientMsgProtobuf);

		private static Assembly SteamKit2Assembly => SteamKit2MarkerType.Assembly;

		public static Type GetProtobufMessageBodyType(uint realEMsg)
		{
			EMsg eMsg = MsgUtil.GetMsg(realEMsg);
			if (MessageTypeOverrides.BodyMap.TryGetValue(eMsg, out Type value))
			{
				return value;
			}
			return SteamKit2Assembly.GetTypes().ToList().Find((Type t) => FilterProtobufMessageBodyType(t, eMsg));
		}

		public static Type GetSteamKitType(string name)
		{
			return SteamKit2Assembly.GetTypes().ToList().Find((Type type) => type.FullName == name);
		}

        public static IEnumerable<Type> GetGCMessageBodyTypeCandidates(uint rawEMsg, uint gcAppid)
        {
            uint gcMsg = MsgUtil.GetGCMsg(rawEMsg);

			Dictionary<uint, Type> gcBodyDict;
            Type bodyType;
            if (MessageTypeOverrides.GCBodyMap.TryGetValue(gcAppid, out gcBodyDict) && gcBodyDict.TryGetValue(gcMsg, out bodyType))
            {
                return Enumerable.Repeat<Type>(bodyType, 1);
            }

            string gcMsgName = EMsgExtensions.GetGCMessageName(rawEMsg, gcAppid);

            string typeMsgName = gcMsgName.Replace("k_", string.Empty).Replace("ESOMsg", string.Empty).TrimStart(new char[]
            {
                '_'
            }).Replace("EMsg", string.Empty).TrimStart("GC");

            var response = from type in typeof(IClientGCMsg).Assembly.GetTypes()
                           from typePrefix in GetPossibleGCTypePrefixes(gcAppid)
                           where type.GetInterfaces().Contains(typeof(IExtensible))
                           where type.FullName.StartsWith(typePrefix) && type.FullName.EndsWith(typeMsgName)
                           select type;
            if (response.Count() == 0)
            {
                response = from type in typeof(IClientGCMsg).Assembly.GetTypes()
                           from typePrefix in GetPossibleGCTypePrefixes(gcAppid)
                           where type.GetInterfaces().Contains(typeof(IExtensible))
                           where type.FullName.StartsWith(typePrefix) && type.FullName.EndsWith(typeMsgName.Replace("DOTA", ""))
                           select type;
            }
            return response;
        }

        public static Type GetNonProtobufMessageBodyType(EMsg eMsg)
		{
			return SteamKit2Assembly.GetTypes().ToList().Find(delegate(Type type)
			{
				if (type.GetInterfaces().ToList().Find((Type @interface) => @interface == typeof(ISteamSerializableMessage)) == null)
				{
					return false;
				}
				return (Activator.CreateInstance(type) as ISteamSerializableMessage).GetEMsg() == eMsg;
			});
		}

		private static bool FilterProtobufMessageBodyType(Type type, EMsg eMsg)
		{
			if (type.GetInterfaces().ToList().Find((Type inter) => inter == typeof(IExtensible)) == null)
			{
				return false;
			}
			if (type.Namespace != "SKYNET")
			{
				return false;
			}
			if (!type.Name.EndsWith(eMsg.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			return true;
		}


		private static IEnumerable<string> GetPossibleGCTypePrefixes(uint appid)
		{
			switch (appid)
			{
			    case 570u:
                    yield return "SKYNET";
                    break;
			}
		}
	}
}
