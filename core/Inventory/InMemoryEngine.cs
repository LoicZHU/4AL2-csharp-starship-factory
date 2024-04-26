using core.Products.Starship.Components.Engine;

namespace core.Inventory;

public class InMemoryEngine
{
	private readonly Dictionary<Guid, Engine> _cache = new();

	public void Add(Engine engine)
	{
		_cache.Add(engine.Id, engine);
	}

	public Int32 CountByName(String name)
	{
		return _cache.Values.Count(engine => engine.Name == name);
	}

	public void Remove(String name)
	{
		var engine = _cache.Values.FirstOrDefault(engine => engine.Name == name);
		if (engine is not null)
		{
			_cache.Remove(engine.Id);
		}
	}
}
