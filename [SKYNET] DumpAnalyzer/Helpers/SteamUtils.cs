using Microsoft.Win32;
using System;

namespace NetHookAnalyzer2
{
	internal class SteamUtils
	{
		private static string RegistryPathToSteam
		{
			get
			{
				if (!Environment.Is64BitProcess)
				{
					return "HKEY_LOCAL_MACHINE\\Software\\Valve\\Steam";
				}
				return "HKEY_LOCAL_MACHINE\\Software\\Wow6432Node\\Valve\\Steam";
			}
		}

		public static string GetSteamDirectory()
		{
			string result = "";
			try
			{
				result = (string)Registry.GetValue(RegistryPathToSteam, "InstallPath", null);
				return result;
			}
			catch
			{
				return result;
			}
		}
	}
}
