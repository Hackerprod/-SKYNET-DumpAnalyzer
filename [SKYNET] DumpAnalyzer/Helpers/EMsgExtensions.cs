using SKYNET;
using SKYNET.Steam;
using System;
using System.Collections.Generic;

namespace NetHookAnalyzer2
{
	internal static class EMsgExtensions
	{
		public static string GetGCMessageName(uint eMsg, uint appId)
		{
			eMsg = MsgUtil.GetGCMsg(eMsg);
			foreach (Type gCEMsgEnum in GetGCEMsgEnums(appId))
			{
				if (Enum.IsDefined(gCEMsgEnum, (int)eMsg))
				{
					return Enum.GetName(gCEMsgEnum, (int)eMsg);
				}
			}
			return eMsg.ToString();
		}

		private static IEnumerable<Type> GetGCEMsgEnums(uint appId)
		{
            yield return typeof(EDOTAGCMsg);
            yield return typeof(EGCBaseMsg);
            yield return typeof(ESOMsg);
            yield return typeof(EGCItemMsg);
            yield return typeof(EGCBaseClientMsg);
        }
	}
}
