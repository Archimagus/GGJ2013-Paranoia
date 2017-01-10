using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class ExtensionMethods
{
	public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
	{
		if (enumerable == null)
			return true;

		return !enumerable.Any();
	}
	public static void Shuffle(this Array array)
	{
		for (int i = 0; i < array.Length; i++)
		{
			for (int j = 0; j < array.Length; j++)
			{
				var element = array.GetValue(j);
				int other = UnityEngine.Random.Range(0, array.Length);
				array.SetValue(array.GetValue(other), j);
				array.SetValue(element, other);
			}
		}
	}
}
