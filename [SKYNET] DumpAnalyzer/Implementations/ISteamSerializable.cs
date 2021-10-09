using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetHookAnalyzer2.Implementations
{
	public interface ISteamSerializable
	{
		void Serialize(Stream stream);

		void Deserialize(Stream stream);
	}
}
