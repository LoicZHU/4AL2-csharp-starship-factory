using core.Components;
using core.Utils;

namespace core.Starships;

public sealed class StarshipBuilder : IStarshipBuilder
{
	public String Name { get; set; }
	public Hull Hull { get; set; }
	public List<Engine> Engines { get; set; }
	public (Wing, Wing) WingPair { get; set; }
	public List<Thruster> Thrusters { get; set; }

	public static IStarshipBuilder create()
	{
		return new StarshipBuilder();
	}

	public IStarshipBuilder WithName(String name)
	{
		if (!IsValidStarshipName(name))
		{
			throw new ArgumentException("Nom de vaisseau invalide");
		}

		var starshipBuilder = new StarshipBuilder
		{
			Name = name,
			Engines = this.Engines,
			Hull = this.Hull,
			WingPair = this.WingPair,
			Thrusters = this.Thrusters
		};

		return starshipBuilder;
	}

	private Boolean IsValidStarshipName(String name)
	{
		return name.Equals(StarshipName.Cargo, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(StarshipName.Explorer, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(StarshipName.Speeder, StringComparison.OrdinalIgnoreCase);
	}

	public IStarshipBuilder WithEngines(List<Engine> engines)
	{
		this.CheckEngines(engines);

		var starshipBuilder = new StarshipBuilder
		{
			Name = this.Name,
			Engines = engines,
			Hull = this.Hull,
			WingPair = this.WingPair,
			Thrusters = this.Thrusters
		};

		return starshipBuilder;
	}

	private void CheckEngines(List<Engine> engines)
	{
		if (!UtilsFunction.IsListCountBetweenOneAndMax(engines, 2))
		{
			throw new ArgumentException("Nombre de moteurs invalide");
		}

		if (!engines.TrueForAll(engine => IsValidEngineComponent(engine.Name)))
		{
			throw new ArgumentException("Moteur invalide");
		}
	}

	private Boolean IsValidEngineComponent(String name)
	{
		return name.Equals(EngineComponent.EngineEc1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(EngineComponent.EngineEe1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(EngineComponent.EngineEs1, StringComparison.OrdinalIgnoreCase);
	}

	public IStarshipBuilder WithHull(Hull hull)
	{
		if (!IsValidHullComponent(hull.Name))
		{
			throw new ArgumentException("Coque invalide");
		}

		var starshipBuilder = new StarshipBuilder
		{
			Name = this.Name,
			Engines = this.Engines,
			Hull = hull,
			WingPair = this.WingPair,
			Thrusters = this.Thrusters
		};

		return starshipBuilder;
	}

	private Boolean IsValidHullComponent(String name)
	{
		return name.Equals(HullComponent.HullHc1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(HullComponent.HullHe1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(HullComponent.HullHs1, StringComparison.OrdinalIgnoreCase);
	}

	public IStarshipBuilder WithThrusters(List<Thruster> thrusters)
	{
		this.CheckThrusters(thrusters);

		var starshipBuilder = new StarshipBuilder();
		starshipBuilder.Name = this.Name;
		starshipBuilder.Engines = this.Engines;
		starshipBuilder.Hull = this.Hull;
		starshipBuilder.WingPair = this.WingPair;
		starshipBuilder.Thrusters = thrusters;

		return starshipBuilder;
	}

	private void CheckThrusters(List<Thruster> thrusters)
	{
		if (!UtilsFunction.IsListCountBetweenOneAndMax(thrusters, 3))
		{
			throw new ArgumentException("Nombre de propulseurs invalide");
		}

		if (!thrusters.TrueForAll(thruster => IsValidThrusterComponent(thruster.Name)))
		{
			throw new ArgumentException("Propulseur invalide");
		}
	}

	private Boolean IsValidThrusterComponent(String name)
	{
		return name.Equals(ThrusterComponent.ThrusterTc1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(ThrusterComponent.ThrusterTe1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(ThrusterComponent.ThrusterTs1, StringComparison.OrdinalIgnoreCase);
	}

	public IStarshipBuilder WithWingPair((Wing, Wing) wingPair)
	{
		this.CheckWingPair(wingPair);

		var starshipBuilder = new StarshipBuilder
		{
			Name = this.Name,
			Engines = this.Engines,
			Hull = this.Hull,
			WingPair = wingPair,
			Thrusters = this.Thrusters
		};

		return starshipBuilder;
	}

	private void CheckWingPair((Wing, Wing) wingPair)
	{
		if (
			!this.IsValidWingComponent(wingPair.Item1.Name)
			|| !this.IsValidWingComponent(wingPair.Item2.Name)
		)
		{
			throw new ArgumentException("Aile invalide");
		}
	}

	private Boolean IsValidWingComponent(String name)
	{
		return name.Equals(WingComponent.WingWc1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(WingComponent.WingWe1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(WingComponent.WingWs1, StringComparison.OrdinalIgnoreCase);
	}

	public Starship Build()
	{
		return Starship.Create(
			this.Name,
			this.Hull,
			this.Engines,
			this.WingPair,
			this.Thrusters
		);
	}
}
