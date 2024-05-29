using core.Components;

namespace core.Starships;

public sealed class StarshipBuilder : IStarshipBuilder
{
	public String Name { get; set; }
	public Hull Hull { get; set; }
	public Engine Engine { get; set; }
	public Wing Wing { get; set; }
	public List<Thruster> Thrusters { get; set; }

	public static IStarshipBuilder create()
	{
		return new StarshipBuilder();
	}

	public IStarshipBuilder WithName(String name)
	{
		if (!IsValidStarshipName(name))
		{
			throw new ArgumentException("Invalid starship name");
		}

		var starshipBuilder = new StarshipBuilder();
		starshipBuilder.Name = name;
		starshipBuilder.Engine = this.Engine;
		starshipBuilder.Hull = this.Hull;
		starshipBuilder.Wing = this.Wing;
		starshipBuilder.Thrusters = this.Thrusters;

		return starshipBuilder;
	}

	private Boolean IsValidStarshipName(String name)
	{
		return name.Equals(StarshipName.Cargo, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(StarshipName.Explorer, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(StarshipName.Speeder, StringComparison.OrdinalIgnoreCase);
	}

	public IStarshipBuilder WithEngine(Engine engine)
	{
		if (!IsValidEngineComponent(engine.Name))
		{
			throw new ArgumentException("Invalid engine name");
		}

		var starshipBuilder = new StarshipBuilder();
		starshipBuilder.Name = this.Name;
		starshipBuilder.Engine = engine;
		starshipBuilder.Hull = this.Hull;
		starshipBuilder.Wing = this.Wing;
		starshipBuilder.Thrusters = this.Thrusters;

		return starshipBuilder;
	}

	private Boolean IsValidEngineComponent(String name)
	{
		return name.Equals(EngineComponent.Engine_EC1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(EngineComponent.Engine_EE1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(EngineComponent.Engine_ES1, StringComparison.OrdinalIgnoreCase);
	}

	public IStarshipBuilder WithHull(Hull hull)
	{
		if (!IsValidHullComponent(hull.Name))
		{
			throw new ArgumentException("Invalid hull name");
		}

		var starshipBuilder = new StarshipBuilder();
		starshipBuilder.Name = this.Name;
		starshipBuilder.Engine = this.Engine;
		starshipBuilder.Hull = hull;
		starshipBuilder.Wing = this.Wing;
		starshipBuilder.Thrusters = this.Thrusters;

		return starshipBuilder;
	}

	private Boolean IsValidHullComponent(String name)
	{
		return name.Equals(HullComponent.Hull_HC1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(HullComponent.Hull_HE1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(HullComponent.Hull_HS1, StringComparison.OrdinalIgnoreCase);
	}

	public IStarshipBuilder WithThrusters(List<Thruster> thrusters)
	{
		CheckThrusters(thrusters);

		var starshipBuilder = new StarshipBuilder();
		starshipBuilder.Name = this.Name;
		starshipBuilder.Engine = this.Engine;
		starshipBuilder.Hull = this.Hull;
		starshipBuilder.Wing = this.Wing;
		starshipBuilder.Thrusters = thrusters;

		return starshipBuilder;
	}

	private void CheckThrusters(List<Thruster> thrusters)
	{
		if (thrusters.Count != 2)
		{
			throw new ArgumentException("Invalid thruster count");
		}

		if (thrusters.TrueForAll(thruster => IsValidThrusterComponent(thruster.Name)))
		{
			throw new ArgumentException("Invalid thruster name");
		}

		if (!thrusters[0].Name.Equals(thrusters[1].Name, StringComparison.OrdinalIgnoreCase))
		{
			throw new ArgumentException("Thrusters must be the same type");
		}
	}

	private Boolean IsValidThrusterComponent(String name)
	{
		return name.Equals(ThrusterComponent.Thruster_TC1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(ThrusterComponent.Thruster_TE1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(ThrusterComponent.Thruster_TS1, StringComparison.OrdinalIgnoreCase);
	}

	public IStarshipBuilder WithWing(Wing wing)
	{
		if (!IsValidWingComponent(wing.Name))
		{
			throw new ArgumentException("Invalid wing name");
		}

		var starshipBuilder = new StarshipBuilder();
		starshipBuilder.Name = this.Name;
		starshipBuilder.Engine = this.Engine;
		starshipBuilder.Hull = this.Hull;
		starshipBuilder.Wing = wing;
		starshipBuilder.Thrusters = this.Thrusters;

		return starshipBuilder;
	}

	private static Boolean IsValidWingComponent(String name)
	{
		return name.Equals(WingComponent.Wings_WC1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(WingComponent.Wings_WE1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(WingComponent.Wings_WS1, StringComparison.OrdinalIgnoreCase);
	}

	public Starship Build()
	{
		return Starship.Create(this.Name, this.Hull, this.Engine, this.Wing, this.Thrusters);
	}
}
