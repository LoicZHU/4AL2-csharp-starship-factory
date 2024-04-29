using core.Products.Starship.Components.Engine;

namespace core.Inventory.Starships.Components;

public class InMemoryEngine
{
	private readonly Dictionary<Guid, Engine> _cache = new();

	public InMemoryEngine()
	{
		_cache = new()
		{
			{ Guid.NewGuid(), new Engine(EngineModel.Engine_EE1) },
			{ Guid.NewGuid(), new Engine(EngineModel.Engine_EE1) },
			{ Guid.NewGuid(), new Engine(EngineModel.Engine_EE1) },
			{ Guid.NewGuid(), new Engine(EngineModel.Engine_ES1) },
			{ Guid.NewGuid(), new Engine(EngineModel.Engine_ES1) },
			{ Guid.NewGuid(), new Engine(EngineModel.Engine_ES1) },
			{ Guid.NewGuid(), new Engine(EngineModel.Engine_EC1) },
			{ Guid.NewGuid(), new Engine(EngineModel.Engine_EC1) },
			{ Guid.NewGuid(), new Engine(EngineModel.Engine_EC1) }
		};
	}

	public void Add(Engine engine)
	{
		_cache.Add(engine.Id, engine);
	}

	public void Remove(String name)
	{
		var engine = _cache.Values.FirstOrDefault(engine => engine.Name == name);
		if (engine is not null)
		{
			_cache.Remove(engine.Id);
		}
	}

	public Int32 CountByName(String name)
	{
		return _cache.Values.Count(engine => engine.Name == name);
	}

	public void PrintInventory()
	{
		var counts = new Dictionary<String, int>();

		foreach (var component in _cache.Values)
		{
			var name = component.Name;

			if (!counts.ContainsKey(name))
			{
				counts[name] = 1;
			}
			else
			{
				counts[name]++;
			}
		}

		foreach (var count in counts)
		{
			Console.WriteLine($"{count.Value} {count.Key}");
		}
	}
}
