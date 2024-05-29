using core.Components;
using core.Starships;
using core.Utils;

namespace core.In_memories.Items;

public class InMemoryStarship : Singleton<InMemoryStarship>
{
	private readonly Dictionary<Guid, Starship> _cache;

	public InMemoryStarship()
	{
		_cache = new()
		{
			{ Guid.NewGuid(), BuildExplorer() },
			{ Guid.NewGuid(), BuildExplorer() },
			{ Guid.NewGuid(), BuildSpeeder() },
			{ Guid.NewGuid(), CreateCargo() }
		};
	}

	private Starship BuildExplorer()
	{
		return StarshipBuilder
			.create()
			.WithName(StarshipName.Explorer)
			.WithEngine(Engine.Create(EngineComponent.Engine_EE1))
			.WithHull(Hull.Create(HullComponent.Hull_HE1))
			.WithWing(Wing.Create(WingComponent.Wings_WE1))
			.WithThrusters(new() { Thruster.Create(ThrusterComponent.Thruster_TE1), })
			.Build();
	}

	private Starship BuildSpeeder()
	{
		return StarshipBuilder
			.create()
			.WithName(StarshipName.Speeder)
			.WithEngine(Engine.Create(EngineComponent.Engine_ES1))
			.WithHull(Hull.Create(HullComponent.Hull_HS1))
			.WithWing(Wing.Create(WingComponent.Wings_WS1))
			.WithThrusters(
				new()
				{
					Thruster.Create(ThrusterComponent.Thruster_TS1),
					Thruster.Create(ThrusterComponent.Thruster_TS1)
				}
			)
			.Build();
	}

	private Starship CreateCargo()
	{
		return StarshipBuilder
			.create()
			.WithName(StarshipName.Cargo)
			.WithEngine(Engine.Create(EngineComponent.Engine_EC1))
			.WithHull(Hull.Create(HullComponent.Hull_HC1))
			.WithWing(Wing.Create(WingComponent.Wings_WC1))
			.WithThrusters(new() { Thruster.Create(ThrusterComponent.Thruster_TC1), })
			.Build();
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
		var starship = _cache.Values.FirstOrDefault(starship =>
			starship.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
		);

		if (starship is not null)
		{
			_cache.Remove(starship.Id);
		}
	}

	public Int32 CountByName(String name)
	{
		return _cache.Values.Count(starship => starship.Name == name);
	}
}
