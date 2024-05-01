using core.App.Products.Starship.Components.Engine;

namespace core.In_memory.Inventory.Components;

public class InMemoryEngine
{
	private readonly Dictionary<Guid, Engine> _cache = new();

	public InMemoryEngine()
	{
		SetCache();
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

	private void SetCache()
	{
		this.Add(new Engine(EngineModel.Engine_EE1));
		this.Add(new Engine(EngineModel.Engine_EE1));
		this.Add(new Engine(EngineModel.Engine_EE1));
		this.Add(new Engine(EngineModel.Engine_ES1));
		this.Add(new Engine(EngineModel.Engine_ES1));
		this.Add(new Engine(EngineModel.Engine_ES1));
		this.Add(new Engine(EngineModel.Engine_EC1));
		this.Add(new Engine(EngineModel.Engine_EC1));
		this.Add(new Engine(EngineModel.Engine_EC1));
	}
}