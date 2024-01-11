using System;
using System.Net;

namespace Roblox.ApiClientBase;

public class TimeoutWebClient : WebClient
{
	private readonly TimeSpan _Timeout;

	private readonly TimeSpan? _ReadWriteTimeout;

	private readonly string _ConnectionGroupName;

	public TimeoutWebClient(TimeSpan timeout, string connectionGroupName = null, TimeSpan? readWriteTimeout = null)
	{
		_Timeout = timeout;
		_ConnectionGroupName = connectionGroupName;
		_ReadWriteTimeout = readWriteTimeout;
	}

	protected override WebRequest GetWebRequest(Uri uri)
	{
		WebRequest webRequest = base.GetWebRequest(uri);
		if (webRequest == null)
		{
			return null;
		}
		TimeSpan? readWriteTimeout = _ReadWriteTimeout;
		if (readWriteTimeout.HasValue && webRequest is HttpWebRequest httpWebRequest)
		{
			readWriteTimeout = _ReadWriteTimeout;
			httpWebRequest.ReadWriteTimeout = (int)readWriteTimeout.Value.TotalMilliseconds;
		}
		TimeSpan timeout = _Timeout;
		webRequest.Timeout = (int)timeout.TotalMilliseconds;
		if (!string.IsNullOrEmpty(_ConnectionGroupName))
		{
			webRequest.ConnectionGroupName = _ConnectionGroupName;
		}
		return webRequest;
	}
}
