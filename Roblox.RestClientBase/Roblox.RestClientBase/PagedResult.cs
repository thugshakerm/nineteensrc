using System.Collections.Generic;

namespace Roblox.RestClientBase;

public class PagedResult<TCount, TPageItem>
{
	public TCount Count { get; set; }

	public IEnumerable<TPageItem> PageItems { get; set; }
}
