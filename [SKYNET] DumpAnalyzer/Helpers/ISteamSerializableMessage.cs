
using SteamKit2.Internal;

namespace SKYNET
{
	public interface ISteamSerializableMessage : ISteamSerializable
	{
		EMsg GetEMsg();
	}
}
