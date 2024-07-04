using core.Components;
using core.Utils;

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
		return name.Equals(EngineComponent.EngineEc1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(EngineComponent.EngineEe1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(EngineComponent.EngineEs1, StringComparison.OrdinalIgnoreCase);
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
		return name.Equals(HullComponent.HullHc1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(HullComponent.HullHe1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(HullComponent.HullHs1, StringComparison.OrdinalIgnoreCase);
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
		if (UtilsFunction.IsEqualToZero(thrusters.Count))
		{
			throw new ArgumentException("Invalid thruster count");
		}

		if (!thrusters.TrueForAll(thruster => IsValidThrusterComponent(thruster.Name)))
		{
			throw new ArgumentException("Invalid thruster name");
		}
	}

	private Boolean IsValidThrusterComponent(String name)
	{
		return name.Equals(ThrusterComponent.ThrusterTc1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(ThrusterComponent.ThrusterTe1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(ThrusterComponent.ThrusterTs1, StringComparison.OrdinalIgnoreCase);
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
		return name.Equals(WingComponent.WingsWc1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(WingComponent.WingsWe1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(WingComponent.WingsWs1, StringComparison.OrdinalIgnoreCase);
	}

	public Starship Build()
	{
		return Starship.Create(this.Name, this.Hull, this.Engine, this.Wing, this.Thrusters);
	}
}
