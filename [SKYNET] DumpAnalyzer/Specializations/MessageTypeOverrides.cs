using SKYNET;
using System;
using System.Collections.Generic;

namespace NetHookAnalyzer2
{
	internal static class MessageTypeOverrides
	{
		public static Dictionary<EMsg, Type> BodyMap = new Dictionary<EMsg, Type>
		{
			{
				EMsg.k_EMsgClientToGC,
				typeof(CMsgGCClient)
			},
			{
				EMsg.k_EMsgClientFromGC,
				typeof(CMsgGCClient)
			},
		};

		public static Dictionary<uint, Dictionary<uint, Type>> GCBodyMap = new Dictionary<uint, Dictionary<uint, Type>>
		{
			[570] = new Dictionary<uint, Type>
			{
				[4006] = typeof(CMsgClientHello),
				[4004] = typeof(CMsgClientWelcome),
				[4007] = typeof(CMsgClientHello),
				[4005] = typeof(CMsgClientWelcome),
				[4009] = typeof(CMsgConnectionStatus),
				[4010] = typeof(CMsgConnectionStatus),
				[21] = typeof(CMsgSOSingleObject),
				[23] = typeof(CMsgSOSingleObject),
				[22] = typeof(CMsgSOSingleObject),
				[26] = typeof(CMsgSOMultipleObjects),
				[8136] = typeof(CMsgDOTATeamsInfo),
				[8137] = typeof(CMsgDOTAMyTeamInfoRequest),

				//Added

				[7535] = typeof(CMsgDOTAProfileCard),
				[8035] = typeof(CMsgDOTAProfileCard),
				[7539] = typeof(CMsgDOTAProfileCard),
				[8303] = typeof(CMsgSocialFeedRequest),
				[8304] = typeof(CMsgSocialFeedResponse),
				[7055] = typeof(CMsgGenericResult),
				[8330] = typeof(CMsgRequestPlayerRecentAccomplishments),
				[8331] = typeof(CMsgRequestPlayerRecentAccomplishmentsResponse),
				[8135] = typeof(CMsgDOTATeamInfo),
				[8074] = typeof(CMsgDOTAProfileTickets),
				[7226] = typeof(CMsgDOTARequestGuildData),
				[7254] = typeof(CMsgDOTAGuildInviteData),


			},
		};
	}
}
