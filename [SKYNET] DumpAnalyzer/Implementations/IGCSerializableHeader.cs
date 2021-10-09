using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetHookAnalyzer2.Implementations
{
	public interface IGCSerializableHeader : ISteamSerializable
	{
		void SetEMsg(uint msg);
	}
}
