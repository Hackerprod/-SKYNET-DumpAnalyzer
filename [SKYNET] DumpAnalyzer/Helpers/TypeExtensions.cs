using System;
using System.Collections;

namespace NetHookAnalyzer2
{
	internal static class TypeExtensions
	{
		public static bool IsDictionaryType(this Type type)
		{
			Type[] interfaces = type.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				if (interfaces[i] == typeof(IDictionary))
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsEnumerableType(this Type type)
		{
			Type[] interfaces = type.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				if (interfaces[i] == typeof(IEnumerable))
				{
					return true;
				}
			}
			return false;
		}
	}
}
