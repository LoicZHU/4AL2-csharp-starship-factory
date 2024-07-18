using core.Components;
using core.Starships;

namespace UnitTests.App.Helpers.Builder;

public class StarshipBuilderTests
{
	[Fact]
	public void WithName_ValidName_SetsName()
	{
		// Arrange
		var builder = StarshipBuilder.create();
		var name = StarshipName.Cargo;

		// Act
		var starship = builder.WithName(name).Build();

		// Assert
		Assert.Equal(name, starship.Name);
	}

	[Fact]
	public void WithName_InvalidName_ThrowsArgumentException()
	{
		// Arrange
		var builder = StarshipBuilder.create();
		var invalidName = "InvalidName";

		// Act & Assert
		Assert.Throws<ArgumentException>(() => builder.WithName(invalidName));
	}

	[Fact]
	public void WithEngine_ValidEngine_SetsEngine()
	{
		// Arrange
		var builder = StarshipBuilder.create();
		var engine = Engine.Create(EngineComponent.EngineEc1);

		// Act
		var starship = builder.WithEngines(engine).Build();

		// Assert
		Assert.Equal(engine, starship.Engines);
	}

	[Fact]
	public void WithEngine_InvalidEngine_ThrowsArgumentException()
	{
		// Arrange
		var builder = StarshipBuilder.create();
		var invalidEngine = Engine.Create("InvalidEngine");

		// Act & Assert
		Assert.Throws<ArgumentException>(() => builder.WithEngines(invalidEngine));
	}

	[Fact]
	public void WithHull_ValidHull_SetsHull()
	{
		// Arrange
		var builder = StarshipBuilder.create();
		var hull = Hull.Create(HullComponent.HullHc1);

		// Act
		var starship = builder.WithHull(hull).Build();

		// Assert
		Assert.Equal(hull, starship.Hull);
	}

	[Fact]
	public void WithHull_InvalidHull_ThrowsArgumentException()
	{
		// Arrange
		var builder = StarshipBuilder.create();
		var invalidHull = Hull.Create("InvalidHull");

		// Act & Assert
		Assert.Throws<ArgumentException>(() => builder.WithHull(invalidHull));
	}

	[Fact]
	public void WithThrusters_ValidThrusters_SetsThrusters()
	{
		// Arrange
		var builder = StarshipBuilder.create();
		var thrusters = new List<Thruster> { Thruster.Create(ThrusterComponent.ThrusterTc1) };

		// Act
		var starship = builder.WithThrusters(thrusters).Build();

		// Assert
		Assert.Equal(thrusters, starship.Thrusters);
	}

	[Fact]
	public void WithThrusters_EmptyThrusters_ThrowsArgumentException()
	{
		// Arrange
		var builder = StarshipBuilder.create();
		var emptyThrusters = new List<Thruster>();

		// Act & Assert
		var exception = Assert.Throws<ArgumentException>(
			() => builder.WithThrusters(emptyThrusters)
		);
		Assert.Equal("Invalid thruster count", exception.Message);
	}

	[Fact]
	public void WithThrusters_InvalidThrusters_ThrowsArgumentException()
	{
		// Arrange
		var builder = StarshipBuilder.create();
		var invalidThrusters = new List<Thruster> { Thruster.Create("InvalidThruster") };

		// Act & Assert
		Assert.Throws<ArgumentException>(() => builder.WithThrusters(invalidThrusters));
	}

	[Fact]
	public void WithWing_ValidWing_SetsWing()
	{
		// Arrange
		var builder = StarshipBuilder.create();
		var wing = Wing.Create(WingComponent.WingWe1);

		// Act
		var starship = builder.WithWingPair(wing).Build();

		// Assert
		Assert.Equal(wing, starship.WingPair);
	}

	[Fact]
	public void WithWing_InvalidWing_ThrowsArgumentException()
	{
		// Arrange
		var builder = StarshipBuilder.create();
		var invalidWing = Wing.Create("InvalidWing");

		// Act & Assert
		Assert.Throws<ArgumentException>(() => builder.WithWingPair(invalidWing));
	}

	[Fact]
	public void Build_ValidStarship_ReturnsStarship()
	{
		// Arrange
		var builder = StarshipBuilder
			.create()
			.WithName(StarshipName.Cargo)
			.WithHull(Hull.Create(HullComponent.HullHc1))
			.WithEngines(Engine.Create(EngineComponent.EngineEc1))
			.WithWingPair(Wing.Create(WingComponent.WingWc1))
			.WithThrusters(
				new List<Thruster> { Thruster.Create(ThrusterComponent.ThrusterTc1) }
			);

		// Act
		var starship = builder.Build();

		// Assert
		Assert.NotNull(starship);
		Assert.Equal(StarshipName.Cargo, starship.Name);
		Assert.Equal(HullComponent.HullHc1, starship.Hull.Name);
		Assert.Equal(EngineComponent.EngineEc1, starship.Engines.Name);
		Assert.Equal(WingComponent.WingWc1, starship.WingPair.Name);
		Assert.Single(starship.Thrusters);
		Assert.Equal(ThrusterComponent.ThrusterTc1, starship.Thrusters[0].Name);
	}
}
