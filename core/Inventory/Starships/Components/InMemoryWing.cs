using core.Products.Starship.Components.Wing;

namespace core.Inventory.Starships.Components;

public class InMemoryWing
{
	private readonly Dictionary<Guid, Wing> _cache = new();

	public InMemoryWing()
	{
		_cache = new Dictionary<Guid, Wing>
		{
			{ Guid.NewGuid(), new Wing(WingModel.Wings_WC1) },
			{ Guid.NewGuid(), new Wing(WingModel.Wings_WC1) },
			{ Guid.NewGuid(), new Wing(WingModel.Wings_WC1) },
			{ Guid.NewGuid(), new Wing(WingModel.Wings_WE1) },
			{ Guid.NewGuid(), new Wing(WingModel.Wings_WE1) },
			{ Guid.NewGuid(), new Wing(WingModel.Wings_WE1) },
			{ Guid.NewGuid(), new Wing(WingModel.Wings_WS1) },
			{ Guid.NewGuid(), new Wing(WingModel.Wings_WS1) },
			{ Guid.NewGuid(), new Wing(WingModel.Wings_WS1) }
		};
	}

	public void Add(Wing wing)
	{
		_cache.Add(wing.Id, wing);
	}

	public void Remove(String name)
	{
		var wing = _cache.Values.FirstOrDefault(wing => wing.Name == name);
		if (wing is not null)
		{
			_cache.Remove(wing.Id);
		}
	}

	public Int32 CountByName(String name)
	{
		return _cache.Values.Count(wing => wing.Name == name);
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
