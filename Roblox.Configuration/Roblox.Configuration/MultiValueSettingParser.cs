using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Roblox.Configuration;

public static class MultiValueSettingParser
{
	public delegate bool TryParse<T>(string value, out T parsedValue);

	[Obsolete("Use TryParseCommaDelimitedString instead")]
	public static HashSet<T> ParseCommaDelimitedListString<T>(string settingValue, Func<string, T> parser)
	{
		return TryParseCommaDelimitedListString(settingValue, parser);
	}

	public static HashSet<T> TryParseCommaDelimitedListString<T>(string settingValue, Func<string, T> parser, Action<Exception> exceptionHandler = null)
	{
		if (!string.IsNullOrEmpty(settingValue))
		{
			IEnumerable<string> enumerable = Enumerable.Select(settingValue.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries), (string s) => s.Trim());
			HashSet<T> hashSet = new HashSet<T>();
			{
				foreach (string item in enumerable)
				{
					try
					{
						hashSet.Add(parser(item));
					}
					catch (Exception obj)
					{
						exceptionHandler?.Invoke(obj);
					}
				}
				return hashSet;
			}
		}
		return new HashSet<T>();
	}

	public static HashSet<T> ParseSeparatorDelimitedListString<T>(string settingValue, TryParse<T> tryParse, string separator = ",")
	{
		HashSet<T> hashSet = new HashSet<T>();
		if (!string.IsNullOrEmpty(settingValue))
		{
			foreach (string item in Enumerable.Select(settingValue.Split(new string[1] { separator }, StringSplitOptions.RemoveEmptyEntries), (string s) => s.Trim()))
			{
				if (tryParse(item, out var parsedValue))
				{
					hashSet.Add(parsedValue);
				}
			}
		}
		return hashSet;
	}

	public static HashSet<string> ParseCommaDelimitedListString(string settingValue)
	{
		return new HashSet<string>(ParseCommaDelimitedListStringMaintainOrdering(settingValue));
	}

	public static IDictionary<TKey, TVal> ParseAsMap<TKey, TVal>(string settingValue, Action<Exception> exceptionHandler = null)
	{
		IDictionary<TKey, TVal> dictionary = new Dictionary<TKey, TVal>();
		try
		{
			foreach (string item in ParseCommaDelimitedListString(settingValue))
			{
				try
				{
					string[] array = item.Split(new char[1] { '=' });
					if (array.Length == 2)
					{
						TKey key = Convert<TKey>(array[0]);
						TVal value = Convert<TVal>(array[1]);
						dictionary[key] = value;
					}
				}
				catch (Exception obj)
				{
					exceptionHandler?.Invoke(obj);
				}
			}
		}
		catch (Exception obj2)
		{
			exceptionHandler?.Invoke(obj2);
		}
		return dictionary;
	}

	public static IReadOnlyList<string> ParseCommaDelimitedListStringMaintainOrdering(string settingValue)
	{
		if (!string.IsNullOrEmpty(settingValue))
		{
			return Enumerable.ToList(Enumerable.Select(settingValue.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries), (string s) => s.Trim()));
		}
		return new List<string>();
	}

	private static T Convert<T>(string input)
	{
		return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(input);
	}
}
