using core.App.Helpers;
using core.Components;
using core.Starships;

namespace UnitTests.App.Helpers.Starship;

public class StarshipFactoryTests
{
	[Fact]
	public void Create_Explorer_ReturnsCorrectStarship()
	{
		// Act
		var starship = StarshipFactory.Create(StarshipName.Explorer);

		// Assert
		Assert.NotNull(starship);
		Assert.Equal(StarshipName.Explorer, starship.Name);
		Assert.Single(starship.Engines);
		Assert.Equal(EngineComponent.EngineEe1, starship.Engines[0].Name);
		Assert.Equal(HullComponent.HullHe1, starship.Hull.Name);
		Assert.Equal(WingComponent.WingWe1, starship.WingPair.Item1.Name);
		Assert.Equal(WingComponent.WingWe1, starship.WingPair.Item2.Name);
		Assert.Single(starship.Thrusters);
		Assert.Equal(ThrusterComponent.ThrusterTe1, starship.Thrusters[0].Name);
	}

	[Fact]
	public void Create_Speeder_ReturnsCorrectStarship()
	{
		// Act
		var starship = StarshipFactory.Create(StarshipName.Speeder);

		// Assert
		Assert.NotNull(starship);
		Assert.Equal(StarshipName.Speeder, starship.Name);
		Assert.Single(starship.Engines);
		Assert.Equal(EngineComponent.EngineEs1, starship.Engines[0].Name);
		Assert.Equal(HullComponent.HullHs1, starship.Hull.Name);
		Assert.Equal(WingComponent.WingWs1, starship.WingPair.Item1.Name);
		Assert.Equal(WingComponent.WingWs1, starship.WingPair.Item2.Name);
		Assert.Equal(2, starship.Thrusters.Count);
		Assert.All(
			starship.Thrusters,
			thruster => Assert.Equal(ThrusterComponent.ThrusterTs1, thruster.Name)
		);
	}

	[Fact]
	public void Create_Cargo_ReturnsCorrectStarship()
	{
		// Act
		var starship = StarshipFactory.Create(StarshipName.Cargo);

		// Assert
		Assert.NotNull(starship);
		Assert.Equal(StarshipName.Cargo, starship.Name);
		Assert.Single(starship.Engines);
		Assert.Equal(EngineComponent.EngineEc1, starship.Engines[0].Name);
		Assert.Equal(HullComponent.HullHc1, starship.Hull.Name);
		Assert.Equal(WingComponent.WingWc1, starship.WingPair.Item1.Name);
		Assert.Equal(WingComponent.WingWc1, starship.WingPair.Item2.Name);
		Assert.Single(starship.Thrusters);
		Assert.Equal(ThrusterComponent.ThrusterTc1, starship.Thrusters[0].Name);
	}

	[Fact]
	public void Create_UnknownStarship_ThrowsArgumentException()
	{
		// Act & Assert
		var exception = Assert.Throws<ArgumentException>(
			() => StarshipFactory.Create("UnknownStarship")
		);
		Assert.Equal("Vaisseau inconnu: UnknownStarship", exception.Message);
	}
}
