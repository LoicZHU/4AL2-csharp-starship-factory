using core.App.Products.Starship.Components.Wing;

namespace core.In_memory.Inventory.Components;

public class InMemoryWing
{
	private readonly Dictionary<Guid, Wing> _cache = new();

	public InMemoryWing()
	{
		this.SetCache();
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

	private void SetCache()
	{
		this.Add(new Wing(WingModel.Wings_WC1));
		this.Add(new Wing(WingModel.Wings_WC1));
		this.Add(new Wing(WingModel.Wings_WC1));
		this.Add(new Wing(WingModel.Wings_WE1));
		this.Add(new Wing(WingModel.Wings_WE1));
		this.Add(new Wing(WingModel.Wings_WE1));
		this.Add(new Wing(WingModel.Wings_WS1));
		this.Add(new Wing(WingModel.Wings_WS1));
		this.Add(new Wing(WingModel.Wings_WS1));
	}
}
