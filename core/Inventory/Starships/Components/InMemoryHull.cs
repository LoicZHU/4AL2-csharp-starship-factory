using core.Products.Starship.Components.Hull;

namespace core.Inventory.Starships.Components;

public class InMemoryHull
{
	private readonly Dictionary<Guid, Hull> _cache = new();

	public InMemoryHull()
	{
		_cache = new()
		{
			{ Guid.NewGuid(), new Hull(HullModel.Hull_HC1) },
			{ Guid.NewGuid(), new Hull(HullModel.Hull_HC1) },
			{ Guid.NewGuid(), new Hull(HullModel.Hull_HC1) },
			{ Guid.NewGuid(), new Hull(HullModel.Hull_HE1) },
			{ Guid.NewGuid(), new Hull(HullModel.Hull_HE1) },
			{ Guid.NewGuid(), new Hull(HullModel.Hull_HE1) },
			{ Guid.NewGuid(), new Hull(HullModel.Hull_HS1) },
			{ Guid.NewGuid(), new Hull(HullModel.Hull_HS1) },
			{ Guid.NewGuid(), new Hull(HullModel.Hull_HS1) }
		};
	}

	public void Add(Hull hull)
	{
		_cache.Add(hull.Id, hull);
	}

	public void Remove(String name)
	{
		var hull = _cache.Values.FirstOrDefault(hull => hull.Name == name);
		if (hull is not null)
		{
			_cache.Remove(hull.Id);
		}
	}

	public Int32 CountByName(String name)
	{
		return _cache.Values.Count(hull => hull.Name == name);
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
