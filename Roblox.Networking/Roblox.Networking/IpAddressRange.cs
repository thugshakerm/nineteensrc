using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Roblox.Networking;

/// <summary>
/// Maintains a range of IPAdresses and can return if a given address is within the range.
/// The range is inclusive.
/// </summary>
public class IpAddressRange
{
	private readonly AddressFamily _AddressFamily;

	private readonly byte[] _LowerBytes;

	private readonly byte[] _UpperBytes;

	/// <summary>
	/// Initializes a new instance of the <see cref="T:Roblox.Networking.IpAddressRange" /> class.
	/// </summary>
	/// <param name="lower">The minimum IP for this range.</param>
	/// <param name="upper">The maximum IP for this range.</param>
	public IpAddressRange(IPAddress lower, IPAddress upper)
	{
		_AddressFamily = lower.AddressFamily;
		_LowerBytes = lower.GetAddressBytes();
		_UpperBytes = upper.GetAddressBytes();
	}

	/// <summary>
	/// Determines whether the given IPAddress is in the range set for this instance.
	/// </summary>
	/// <param name="address">The address to check.</param>
	/// <returns>
	///   <c>true</c> if the given IPAdress is in range; otherwise, <c>false</c>.
	/// </returns>
	public bool IsInRange(IPAddress address)
	{
		if (address.AddressFamily != _AddressFamily)
		{
			return false;
		}
		byte[] addressBytes = address.GetAddressBytes();
		bool lowerBoundary = true;
		bool upperBoundary = true;
		for (int i = 0; i < _LowerBytes.Length; i++)
		{
			if (!(lowerBoundary || upperBoundary))
			{
				break;
			}
			if ((lowerBoundary && addressBytes[i] < _LowerBytes[i]) || (upperBoundary && addressBytes[i] > _UpperBytes[i]))
			{
				return false;
			}
			lowerBoundary &= addressBytes[i] == _LowerBytes[i];
			upperBoundary &= addressBytes[i] == _UpperBytes[i];
		}
		return true;
	}

	/// <summary>
	/// Parses a string list of IP address ranges into a List of <see cref="T:Roblox.Networking.IpAddressRange" />
	///
	/// A range string is formatted as in this example with single IP addresses or a range indicated by a dash
	/// all separated by commas.
	/// "192.168.1.1, 127.0.0.1-127.0.0.10, 222.222.222.222, 192.168.1.200 - 192.168.1.230"
	/// </summary>
	/// <param name="ranges">The ranges.</param>
	/// <returns></returns>
	public static IReadOnlyCollection<IpAddressRange> ParseStringList(string ranges)
	{
		if (string.IsNullOrWhiteSpace(ranges))
		{
			return new List<IpAddressRange>().AsReadOnly();
		}
		return Enumerable.ToList(Enumerable.Select(Enumerable.Where(Enumerable.Select(ranges.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries), (string range) => Enumerable.ToList(Enumerable.Select(range.Split(new char[1] { '-' }, StringSplitOptions.RemoveEmptyEntries), TryGetIpAddress))), (List<IPAddress> range) => Enumerable.First(range) != null && Enumerable.Last(range) != null), (List<IPAddress> ips) => new IpAddressRange(Enumerable.First(ips), Enumerable.Last(ips)))).AsReadOnly();
	}

	private static IPAddress TryGetIpAddress(string ipString)
	{
		IPAddress.TryParse(ipString.Trim(), out IPAddress ipAddress);
		return ipAddress;
	}
}
