using core.Components;
using core.Starships;

namespace core.App.Helpers;

public sealed class StarshipFactory
{
	public static Starship Create(String starshipName)
	{
		return starshipName switch
		{
			StarshipName.Explorer => BuildExplorer(),
			StarshipName.Speeder => BuildSpeeder(),
			StarshipName.Cargo => BuildCargo(),
			_ => throw new ArgumentException($"Vaisseau inconnu: {starshipName}")
		};
	}

	private static Starship BuildExplorer()
	{
		return StarshipBuilder
			.create()
			.WithName(StarshipName.Explorer)
			.WithEngines(new List<Engine> { Engine.Create(EngineComponent.EngineEe1) })
			.WithHull(Hull.Create(HullComponent.HullHe1))
			.WithWingPair(
				(Wing.Create(WingComponent.WingWe1), Wing.Create(WingComponent.WingWe1))
			)
			.WithThrusters(
				new List<Thruster> { Thruster.Create(ThrusterComponent.ThrusterTe1) }
			)
			.Build();
	}

	private static Starship BuildSpeeder()
	{
		return StarshipBuilder
			.create()
			.WithName(StarshipName.Speeder)
			.WithEngines(new List<Engine> { Engine.Create(EngineComponent.EngineEs1) })
			.WithHull(Hull.Create(HullComponent.HullHs1))
			.WithWingPair(
				(Wing.Create(WingComponent.WingWs1), Wing.Create(WingComponent.WingWs1))
			)
			.WithThrusters(
				new List<Thruster>
				{
					Thruster.Create(ThrusterComponent.ThrusterTs1),
					Thruster.Create(ThrusterComponent.ThrusterTs1)
				}
			)
			.Build();
	}

	private static Starship BuildCargo()
	{
		return StarshipBuilder
			.create()
			.WithName(StarshipName.Cargo)
			.WithEngines(new List<Engine> { Engine.Create(EngineComponent.EngineEc1) })
			.WithHull(Hull.Create(HullComponent.HullHc1))
			.WithWingPair(
				(Wing.Create(WingComponent.WingWc1), Wing.Create(WingComponent.WingWc1))
			)
			.WithThrusters(
				new List<Thruster> { Thruster.Create(ThrusterComponent.ThrusterTc1) }
			)
			.Build();
	}
}
