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
	public void WithEngines_ValidEngines_SetsEngines()
	{
		// Arrange
		var builder = StarshipBuilder.create();
		var engines = new List<Engine> { Engine.Create(EngineComponent.EngineEc1) };

		// Act
		var starship = builder.WithEngines(engines).Build();

		// Assert
		Assert.Equal(engines, starship.Engines);
	}

	[Fact]
	public void WithEngines_InvalidEngines_ThrowsArgumentException()
	{
		// Arrange
		var builder = StarshipBuilder.create();
		var invalidEngines = new List<Engine> { Engine.Create("InvalidEngine") };

		// Act & Assert
		var exception = Assert.Throws<ArgumentException>(
			() => builder.WithEngines(invalidEngines)
		);
		Assert.Equal("Moteur invalide", exception.Message);
	}

	[Fact]
	public void WithEngines_EmptyEngines_ThrowsArgumentException()
	{
		// Arrange
		var builder = StarshipBuilder.create();
		var emptyEngines = new List<Engine>();

		// Act & Assert
		var exception = Assert.Throws<ArgumentException>(
			() => builder.WithEngines(emptyEngines)
		);
		Assert.Equal("Nombre de moteurs invalide", exception.Message);
	}

	// [Fact]
	// public void WithEngines_MoreThanMaxEngines_ThrowsArgumentException()
	// {
	// 	// Arrange
	// 	var builder = StarshipBuilder.create();
	//
	// 	const Int32 MAX_ENGINES = 2;
	// 	var engines = new List<Engine>();
	// 	for (var i = 1; i <= MAX_ENGINES + 1; i++)
	// 	{
	// 		engines.Add(Engine.Create(EngineComponent.EngineEc1));
	// 	}
	//
	// 	// Act & Assert
	// 	var exception = Assert.Throws<ArgumentException>(() => builder.WithEngines(engines));
	// 	Assert.Equal("Nombre de moteurs invalide", exception.Message);
	// }

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
		var exception = Assert.Throws<ArgumentException>(() => builder.WithHull(invalidHull));
		Assert.Equal("Coque invalide", exception.Message);
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
		Assert.Equal("Nombre de propulseurs invalide", exception.Message);
	}

	[Fact]
	public void WithThrusters_InvalidThrusters_ThrowsArgumentException()
	{
		// Arrange
		var builder = StarshipBuilder.create();
		var invalidThrusters = new List<Thruster> { Thruster.Create("InvalidThruster") };

		// Act & Assert
		var exception = Assert.Throws<ArgumentException>(
			() => builder.WithThrusters(invalidThrusters)
		);
		Assert.Equal("Propulseur invalide", exception.Message);
	}

	[Fact]
	public void WithThrusters_MoreThanMaxThrusters_ThrowsArgumentException()
	{
		// Arrange
		var builder = StarshipBuilder.create();
		const Int32 MAX_THRUSTERS = 3;
		var thrusters = new List<Thruster>();
		for (var i = 1; i <= MAX_THRUSTERS + 1; i++)
		{
			thrusters.Add(Thruster.Create(ThrusterComponent.ThrusterTc1));
		}

		// Act & Assert
		var exception = Assert.Throws<ArgumentException>(
			() => builder.WithThrusters(thrusters)
		);
		Assert.Equal("Nombre de propulseurs invalide", exception.Message);
	}

	[Fact]
	public void WithWingPair_ValidWingPair_SetsWingPair()
	{
		// Arrange
		var builder = StarshipBuilder.create();
		var wingPair = (
			Wing.Create(WingComponent.WingWe1),
			Wing.Create(WingComponent.WingWe1)
		);

		// Act
		var starship = builder.WithWingPair(wingPair).Build();

		// Assert
		Assert.Equal(wingPair, starship.WingPair);
	}

	[Fact]
	public void WithWingPair_InvalidWingPair_ThrowsArgumentException()
	{
		// Arrange
		var builder = StarshipBuilder.create();
		var invalidWingPair = (Wing.Create("InvalidWing"), Wing.Create("InvalidWing"));

		// Act & Assert
		var exception = Assert.Throws<ArgumentException>(
			() => builder.WithWingPair(invalidWingPair)
		);
		Assert.Equal("Aile invalide", exception.Message);
	}

	[Fact]
	public void Build_ValidStarship_ReturnsStarship()
	{
		// Arrange
		var builder = StarshipBuilder
			.create()
			.WithName(StarshipName.Cargo)
			.WithHull(Hull.Create(HullComponent.HullHc1))
			.WithEngines(new List<Engine> { Engine.Create(EngineComponent.EngineEc1) })
			.WithWingPair(
				(Wing.Create(WingComponent.WingWc1), Wing.Create(WingComponent.WingWe1))
			)
			.WithThrusters(
				new List<Thruster> { Thruster.Create(ThrusterComponent.ThrusterTc1) }
			);

		// Act
		var starship = builder.Build();

		// Assert
		Assert.NotNull(starship);
		Assert.Equal(StarshipName.Cargo, starship.Name);
		Assert.Equal(HullComponent.HullHc1, starship.Hull.Name);
		Assert.Equal(EngineComponent.EngineEc1, starship.Engines[0].Name);
		Assert.Equal(WingComponent.WingWc1, starship.WingPair.Item1.Name);
		Assert.Equal(WingComponent.WingWe1, starship.WingPair.Item2.Name);
		Assert.Single(starship.Thrusters);
		Assert.Equal(ThrusterComponent.ThrusterTc1, starship.Thrusters[0].Name);
	}
}
