using core.Products.Starship.Components.Wing;

namespace core.Inventory;

public class InMemoryWing
{
	private readonly Dictionary<Guid, Wing> _cache = new();

	public void Add(Wing wing)
	{
		_cache.Add(wing.Id, wing);
	}

	public Int32 CountByName(String name)
	{
		return _cache.Values.Count(wing => wing.Name == name);
	}

	public void Remove(String name)
	{
		var wing = _cache.Values.FirstOrDefault(wing => wing.Name == name);
		if (wing is not null)
		{
			_cache.Remove(wing.Id);
		}
	}
}
