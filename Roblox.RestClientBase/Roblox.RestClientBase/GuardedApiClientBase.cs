using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Roblox.Instrumentation;

namespace Roblox.RestClientBase;

public abstract class GuardedApiClientBase : GuardedRestClientBase
{
	private readonly ICounterRegistry _CounterRegistry;

	private const string _UploadValuesContentType = "application/x-www-form-urlencoded";

	protected abstract string ApiKey { get; }

	protected virtual string ApiKeyParameterName => "apiKey";

	[Obsolete("Use GuardedApiClientBase(ICounterRegistry counterRegistry)")]
	protected GuardedApiClientBase()
		: this(StaticCounterRegistry.Instance)
	{
	}

	protected GuardedApiClientBase(ICounterRegistry counterRegistry)
		: base(counterRegistry)
	{
		_CounterRegistry = counterRegistry;
	}

	private IEnumerable<KeyValuePair<string, object>> BuildApiKeyParameter()
	{
		yield return new KeyValuePair<string, object>(ApiKeyParameterName, ApiKey);
	}

	protected override void BuildHeaders(HttpMethod method, WebClient webClient, IEnumerable<KeyValuePair<string, string>> headers)
	{
		if (method == HttpMethod.Post || method == HttpMethod.Put)
		{
			webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
		}
		base.BuildHeaders(method, webClient, headers);
	}

	protected override string SerializeSendData(object bodyData)
	{
		StringBuilder sb = new StringBuilder();
		if (bodyData is IEnumerable<KeyValuePair<string, object>> formKeyValuePairs)
		{
			bool addDelimiter = false;
			if (bodyData != null)
			{
				foreach (KeyValuePair<string, object> formParameter in formKeyValuePairs)
				{
					if (addDelimiter)
					{
						sb.Append('&');
					}
					sb.Append(formParameter.Key);
					sb.Append("=");
					if (formParameter.Value != null)
					{
						RestClientBase.AppendEscapedUriDataString(sb, formParameter.Value.ToString());
					}
					addDelimiter = true;
				}
			}
		}
		return sb.ToString();
	}

	/// <summary>
	/// Converts the response data into whatever type the calling code desires.
	/// This base method assumes the responseString is in JSON format and deserialize it.
	/// </summary>
	/// <typeparam name="T">The type of object(s) to deserialize the responseString into</typeparam>
	/// <param name="responseString">The response string to deserialize.</param>
	/// <returns>The object instances generated by deserializing the response string</returns>
	protected override T DeserializeResponseData<T>(string responseString)
	{
		return JsonConvert.DeserializeObject<Payload<T>>(responseString).Data;
	}

	/// <summary>
	/// Builds and returns full uri including query string parameters.
	/// This method will also add the ROBLOX ApiKey if it is set.
	/// </summary>
	/// <param name="actionPath">Action name to be executed</param>
	/// <param name="queryStringParameters">Query string parameter</param>
	/// <returns>Complete URI with action name and query string parameters.</returns>
	protected override string BuildUri(string actionPath, IEnumerable<KeyValuePair<string, object>> queryStringParameters)
	{
		string baseUrl = Endpoint + actionPath;
		if (queryStringParameters == null)
		{
			queryStringParameters = new List<KeyValuePair<string, object>>();
		}
		if (ApiKey != null)
		{
			queryStringParameters = Enumerable.Concat(queryStringParameters, BuildApiKeyParameter());
		}
		return new UrlBuilder(baseUrl, () => queryStringParameters).ToString();
	}

	protected void Post(string actionPath, IEnumerable<KeyValuePair<string, object>> queryStringParameters = null, IEnumerable<KeyValuePair<string, object>> formParameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
	{
		base.Post(actionPath, queryStringParameters, formParameters, headers);
	}

	protected async Task PostAsync(string actionPath, CancellationToken cancellationToken, IEnumerable<KeyValuePair<string, object>> queryStringParameters = null, IEnumerable<KeyValuePair<string, object>> formParameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
	{
		await base.PostAsync(actionPath, cancellationToken, queryStringParameters, formParameters, headers);
	}

	protected T Post<T>(string actionPath, IEnumerable<KeyValuePair<string, object>> queryStringParameters = null, IEnumerable<KeyValuePair<string, object>> formParameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
	{
		return base.Post<T>(actionPath, queryStringParameters, formParameters, headers);
	}

	protected async Task<T> PostAsync<T>(string actionPath, CancellationToken cancellationToken, IEnumerable<KeyValuePair<string, object>> queryStringParameters = null, IEnumerable<KeyValuePair<string, object>> formParameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
	{
		return await base.PostAsync<T>(actionPath, cancellationToken, queryStringParameters, formParameters, headers);
	}
}
