using core.Products.Starship;

namespace core.Inventory;

public class InMemoryStarship
{
	private readonly Dictionary<Guid, Starship> _cache = new();

	public void Add(Starship starship)
	{
		_cache.Add(starship.Id, starship);
	}

	public Int32 CountByName(String name)
	{
		return _cache.Values.Count(starship => starship.Name == name);
	}

	public void Remove(String name)
	{
		var starship = _cache.Values.FirstOrDefault(starship => starship.Name == name);
		if (starship is not null)
		{
			_cache.Remove(starship.Id);
		}
	}
}
