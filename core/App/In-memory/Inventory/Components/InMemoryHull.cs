using core.App.Products.Starship.Components.Hull;

namespace core.In_memory.Inventory.Components;

public class InMemoryHull
{
	private readonly Dictionary<Guid, Hull> _cache = new();

	public InMemoryHull()
	{
		this.SetCache();
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

	private void SetCache()
	{
		this.Add(new Hull(HullModel.Hull_HC1));
		this.Add(new Hull(HullModel.Hull_HC1));
		this.Add(new Hull(HullModel.Hull_HC1));
		this.Add(new Hull(HullModel.Hull_HE1));
		this.Add(new Hull(HullModel.Hull_HE1));
		this.Add(new Hull(HullModel.Hull_HE1));
		this.Add(new Hull(HullModel.Hull_HS1));
		this.Add(new Hull(HullModel.Hull_HS1));
		this.Add(new Hull(HullModel.Hull_HS1));
	}
}
