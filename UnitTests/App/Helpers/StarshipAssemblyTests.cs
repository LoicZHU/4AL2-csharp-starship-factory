using core.Assemblies;
using core.Components;
using core.Starships;

namespace UnitTests.App.Helpers;

public class StarshipAssemblyTests
{
	[Fact]
	public void Components_ShouldContainCargoStarshipComponents()
	{
		// Arrange
		var expectedComponents = new Dictionary<String, Int32>
		{
			{ HullComponent.HullHc1, 1 },
			{ EngineComponent.EngineEc1, 1 },
			{ WingComponent.WingWc1, 2 },
			{ ThrusterComponent.ThrusterTc1, 1 }
		};

		// Act
		var actualComponents = StarshipAssembly.Components[StarshipName.Cargo];

		// Assert
		Assert.Equal(expectedComponents, actualComponents);
	}

	[Fact]
	public void Components_ShouldContainExplorerStarshipComponents()
	{
		// Arrange
		var expectedComponents = new Dictionary<String, Int32>
		{
			{ HullComponent.HullHe1, 1 },
			{ EngineComponent.EngineEe1, 1 },
			{ WingComponent.WingWe1, 2 },
			{ ThrusterComponent.ThrusterTe1, 1 }
		};

		// Act
		var actualComponents = StarshipAssembly.Components[StarshipName.Explorer];

		// Assert
		Assert.Equal(expectedComponents, actualComponents);
	}

	[Fact]
	public void Components_ShouldContainSpeederStarshipComponents()
	{
		// Arrange
		var expectedComponents = new Dictionary<String, Int32>
		{
			{ HullComponent.HullHs1, 1 },
			{ EngineComponent.EngineEs1, 1 },
			{ WingComponent.WingWs1, 2 },
			{ ThrusterComponent.ThrusterTs1, 2 }
		};

		// Act
		var actualComponents = StarshipAssembly.Components[StarshipName.Speeder];

		// Assert
		Assert.Equal(expectedComponents, actualComponents);
	}
}
