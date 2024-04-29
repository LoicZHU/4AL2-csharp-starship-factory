using core.Products.Starship.Components.Thruster;

namespace core.Inventory.Starships.Components;

public class InMemoryThruster
{
	private readonly Dictionary<Guid, Thruster> _cache = new();

	public InMemoryThruster()
	{
		_cache = new()
		{
			{ Guid.NewGuid(), new Thruster(ThrusterModel.Thruster_TC1) },
			{ Guid.NewGuid(), new Thruster(ThrusterModel.Thruster_TC1) },
			{ Guid.NewGuid(), new Thruster(ThrusterModel.Thruster_TC1) },
			{ Guid.NewGuid(), new Thruster(ThrusterModel.Thruster_TC1) },
			{ Guid.NewGuid(), new Thruster(ThrusterModel.Thruster_TE1) },
			{ Guid.NewGuid(), new Thruster(ThrusterModel.Thruster_TE1) },
			{ Guid.NewGuid(), new Thruster(ThrusterModel.Thruster_TE1) },
			{ Guid.NewGuid(), new Thruster(ThrusterModel.Thruster_TE1) },
			{ Guid.NewGuid(), new Thruster(ThrusterModel.Thruster_TS1) },
			{ Guid.NewGuid(), new Thruster(ThrusterModel.Thruster_TS1) },
			{ Guid.NewGuid(), new Thruster(ThrusterModel.Thruster_TS1) },
			{ Guid.NewGuid(), new Thruster(ThrusterModel.Thruster_TS1) }
		};
	}

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
