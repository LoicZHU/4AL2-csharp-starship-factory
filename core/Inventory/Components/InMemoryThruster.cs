using core.Products.Starship.Components.Thruster;

namespace core.Inventory;

public class InMemoryThruster
{
	private readonly Dictionary<Guid, Thruster> _cache = new();

	public void Add(Thruster thruster)
	{
		_cache.Add(thruster.Id, thruster);
	}

	public void Remove(String name)
	{
		var thruster = _cache.Values.FirstOrDefault(thruster => thruster.Name == name);
		if (thruster is not null)
		{
			_cache.Remove(thruster.Id);
		}
	}

	public Int32 CountByName(String name)
	{
		return _cache.Values.Count(thruster => thruster.Name == name);
	}
}
