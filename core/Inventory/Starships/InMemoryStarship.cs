using core.Products.Starship;
using core.Products.Starship.Components.Engine;
using core.Products.Starship.Components.Hull;
using core.Products.Starship.Components.Thruster;
using core.Products.Starship.Components.Wing;

namespace core.Inventory;

public class InMemoryStarship
{
	private readonly Dictionary<Guid, Starship> _cache;

	public InMemoryStarship()
	{
		_cache = new()
		{
			{ Guid.NewGuid(), CreateExplorer() },
			{ Guid.NewGuid(), CreateExplorer() },
			{ Guid.NewGuid(), CreateSpeeder() },
			{ Guid.NewGuid(), CreateCargo() }
		};
	}

	private Starship CreateExplorer()
	{
		return new Starship(
			StarshipModel.Explorer,
			HullModel.Hull_HE1,
			EngineModel.Engine_EE1,
			WingModel.Wings_WE1,
			new() { ThrusterModel.Thruster_TE1 }
		);
	}

	private Starship CreateSpeeder()
	{
		return new Starship(
			StarshipModel.Speeder,
			HullModel.Hull_HS1,
			EngineModel.Engine_ES1,
			WingModel.Wings_WS1,
			new() { ThrusterModel.Thruster_TS1, ThrusterModel.Thruster_TS1 }
		);
	}

	private Starship CreateCargo()
	{
		return new Starship(
			StarshipModel.Cargo,
			HullModel.Hull_HC1,
			EngineModel.Engine_EC1,
			WingModel.Wings_WC1,
			new() { ThrusterModel.Thruster_TC1 }
		);
	}

	public void Add(Starship starship)
	{
		if (!_cache.ContainsKey(starship.Id))
		{
			return;
		}

		_cache.Add(starship.Id, starship);
	}

	public List<Starship> GetAll()
	{
		return _cache.Values.ToList();
	}

	public void Remove(String name)
	{
		var starship = _cache.Values.FirstOrDefault(starship => starship.Name == name);
		if (starship is not null)
		{
			_cache.Remove(starship.Id);
		}
	}

	public Int32 CountByName(String name)
	{
		return _cache.Values.Count(starship => starship.Name == name);
	}

	public void PrintInventory()
	{
		var counts = new Dictionary<String, int>();

		foreach (var starship in _cache.Values)
		{
			var name = starship.Name;

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
