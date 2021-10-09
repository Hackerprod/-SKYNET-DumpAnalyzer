using System;
using System.Net;
using System.Net.Sockets;

namespace NetHookAnalyzer2
{

	internal static class NetHelpers
	{
		public static IPAddress GetLocalIP(Socket activeSocket)
		{
			IPEndPoint ipEndPoint = activeSocket.LocalEndPoint as IPEndPoint;
			if (ipEndPoint == null || ipEndPoint.Address == IPAddress.Any)
			{
				throw new InvalidOperationException("Socket not connected");
			}
			return ipEndPoint.Address;
		}

		public static IPAddress GetIPAddress(uint ipAddr)
		{
			byte[] bytes = BitConverter.GetBytes(ipAddr);
			Array.Reverse((Array)bytes);
			return new IPAddress(bytes);
		}

		public static uint GetIPAddress(IPAddress ipAddr)
		{
			byte[] addressBytes = ipAddr.GetAddressBytes();
			Array.Reverse((Array)addressBytes);
			return BitConverter.ToUInt32(addressBytes, 0);
		}

		public static uint EndianSwap(uint input)
		{
			return (uint)IPAddress.NetworkToHostOrder((int)input);
		}

		public static ulong EndianSwap(ulong input)
		{
			return (ulong)IPAddress.NetworkToHostOrder((long)input);
		}

		public static ushort EndianSwap(ushort input)
		{
			return (ushort)IPAddress.NetworkToHostOrder((short)input);
		}

		public static bool TryParseIPEndPoint(string stringValue, out IPEndPoint endPoint)
		{
			string[] endpointParts = stringValue.Split(new char[1] { ':' });
			if (endpointParts.Length != 2)
			{
				endPoint = null;
				return false;
			}
			IPAddress address;
			if (!IPAddress.TryParse(endpointParts[0], out address))
			{
				endPoint = null;
				return false;
			}
			ushort port;
			if (!ushort.TryParse(endpointParts[1], out port))
			{
				endPoint = null;
				return false;
			}
			endPoint = new IPEndPoint(address, port);
			return true;
		}
	}
}