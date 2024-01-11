using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Roblox.Http;

public abstract class HttpHeaders : IHttpHeaders
{
	protected readonly System.Net.Http.Headers.HttpHeaders Headers;

	protected readonly HttpContentHeaders ContentHeaders;

	public IReadOnlyList<string> Keys => Enumerable.ToList(Enumerable.Select(Headers, (KeyValuePair<string, IEnumerable<string>> kvp) => kvp.Key));

	public string ContentType
	{
		get
		{
			return ContentHeaders.ContentType?.MediaType;
		}
		set
		{
			if (!string.IsNullOrWhiteSpace(value))
			{
				string[] source = value.Split(new char[1] { ';' });
				ContentHeaders.ContentType = new MediaTypeHeaderValue(Enumerable.First(source));
			}
		}
	}

	public HttpHeaders(System.Net.Http.Headers.HttpHeaders httpHeaders, HttpContentHeaders contentHeaders)
	{
		Headers = httpHeaders ?? throw new ArgumentNullException("httpHeaders");
		ContentHeaders = contentHeaders ?? CreateContentHeaders();
	}

	public HttpHeaders(System.Net.Http.Headers.HttpHeaders httpHeaders)
		: this(httpHeaders, null)
	{
	}

	public void Add(string name, string value)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentException(string.Format("{0} cannot be null or whitespace.", "name"), "name");
		}
		Headers.TryAddWithoutValidation(name, value);
	}

	public void AddOrUpdate(string name, string value)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentException(string.Format("{0} cannot be null or whitespace.", "name"), "name");
		}
		if (Enumerable.Contains<string>(Keys, name, StringComparer.OrdinalIgnoreCase))
		{
			Remove(name);
		}
		Add(name, value);
	}

	public ICollection<string> Get(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentException(string.Format("{0} cannot be null or whitespace.", "name"), "name");
		}
		if (!Enumerable.Contains<string>(Keys, name, StringComparer.OrdinalIgnoreCase))
		{
			return new string[0];
		}
		return Enumerable.ToArray(Headers.GetValues(name));
	}

	public bool Remove(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentException(string.Format("{0} cannot be null or whitespace.", "name"), "name");
		}
		return Headers.Remove(name);
	}

	private HttpContentHeaders CreateContentHeaders()
	{
		return new ByteArrayContent(new byte[0]).Headers;
	}
}
