using core.Components;
using core.Starships;
using core.UI;
using core.Utils;

namespace core.In_memories.Items;

public class InMemoryStarship : AbstractSingleton<InMemoryStarship>
{
	private readonly Dictionary<Guid, Starship> _cache;

	public InMemoryStarship()
	{
		_cache = new();

		this.SetCache();
	}

	private void SetCache()
	{
		try
		{
			this.Add(BuildExplorer());
			this.Add(BuildExplorer());
			this.Add(BuildSpeeder());
			this.Add(CreateCargo());
		}
		catch (Exception e)
		{
			Terminal.PrintMessageWithLinebreak(e.Message);
		}
	}

	private Starship BuildExplorer()
	{
		// return StarshipBuilder
		// 	.create()
		// 	.WithName(StarshipName.Explorer)
		// 	.WithEngines(Engine.Create(EngineComponent.EngineEe1))
		// 	.WithHull(Hull.Create(HullComponent.HullHe1))
		// 	.WithWingPair(Wing.Create(WingComponent.WingsWe1))
		// 	.WithThrusters(new() { Thruster.Create(ThrusterComponent.ThrusterTe1), })
		// 	.Build();
		return StarshipBuilder
			.create()
			.WithName(StarshipName.Explorer)
			.WithEngines(new() { Engine.Create(EngineComponent.EngineEe1) })
			.WithHull(Hull.Create(HullComponent.HullHe1))
			.WithWingPair(
				(Wing.Create(WingComponent.WingWe1), Wing.Create(WingComponent.WingWe1))
			)
			.WithThrusters(new() { Thruster.Create(ThrusterComponent.ThrusterTe1), })
			.Build();
	}

	private Starship BuildSpeeder()
	{
		// return StarshipBuilder
		// 	.create()
		// 	.WithName(StarshipName.Speeder)
		// 	.WithEngines(Engine.Create(EngineComponent.EngineEs1))
		// 	.WithHull(Hull.Create(HullComponent.HullHs1))
		// 	.WithWingPair(Wing.Create(WingComponent.WingsWs1))
		// 	.WithThrusters(
		// 		new()
		// 		{
		// 			Thruster.Create(ThrusterComponent.ThrusterTs1),
		// 			Thruster.Create(ThrusterComponent.ThrusterTs1)
		// 		}
		// 	)
		// 	.Build();
		return StarshipBuilder
			.create()
			.WithName(StarshipName.Speeder)
			.WithEngines(new() { Engine.Create(EngineComponent.EngineEs1) })
			.WithHull(Hull.Create(HullComponent.HullHs1))
			.WithWingPair(
				(Wing.Create(WingComponent.WingWs1), Wing.Create(WingComponent.WingWs1))
			)
			.WithThrusters(
				new()
				{
					Thruster.Create(ThrusterComponent.ThrusterTs1),
					Thruster.Create(ThrusterComponent.ThrusterTs1)
				}
			)
			.Build();
	}

	private Starship CreateCargo()
	{
		return StarshipBuilder
			.create()
			.WithName(StarshipName.Cargo)
			.WithEngines(new() { Engine.Create(EngineComponent.EngineEc1) })
			.WithHull(Hull.Create(HullComponent.HullHc1))
			.WithWingPair(
				(Wing.Create(WingComponent.WingWc1), Wing.Create(WingComponent.WingWc1))
			)
			.WithThrusters(new() { Thruster.Create(ThrusterComponent.ThrusterTc1), })
			.Build();
	}

	public void Add(Starship starship)
	{
		if (_cache.ContainsKey(starship.Id))
		{
			throw new ArgumentException($"Starship with id {starship.Id} already exists.");
		}

		_cache.Add(starship.Id, starship);
	}

	public List<Starship> GetAll()
	{
		return _cache.Values.ToList();
	}

	public Boolean Exists(String name)
	{
		return _cache.Values.Any(starship =>
			starship.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
		);
	}

	public void Remove(String name)
	{
		var starship = _cache.Values.FirstOrDefault(starship =>
			starship.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
		);

		if (!UtilsFunction.IsNull(starship))
		{
			_cache.Remove(starship.Id);
		}
	}

	public Dictionary<String, Int32> GetStock()
	{
		var stock = new Dictionary<String, Int32>();

		foreach (var starship in _cache.Values)
		{
			stock[starship.Name] = !stock.ContainsKey(starship.Name)
				? 1
				: stock[starship.Name] + 1;
		}

		return stock;
	}
}
