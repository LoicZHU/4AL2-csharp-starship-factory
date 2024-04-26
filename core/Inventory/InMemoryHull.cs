using core.Products.Starship.Components.Hull;

namespace core.Inventory;

public class InMemoryHull
{
	private readonly Dictionary<Guid, Hull> _cache = new();

	public void Add(Hull hull)
	{
		_cache.Add(hull.Id, hull);
	}

	public Int32 CountByName(String name)
	{
		return _cache.Values.Count(hull => hull.Name == name);
	}

	public void Remove(String name)
	{
		var hull = _cache.Values.FirstOrDefault(hull => hull.Name == name);
		if (hull is not null)
		{
			_cache.Remove(hull.Id);
		}
	}
}
