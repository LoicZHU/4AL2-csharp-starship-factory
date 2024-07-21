using core.Components;
using core.Repositories.ComponentRepository;
using core.Services;
using core.Starships;
using NSubstitute;

namespace UnitTests.App.Services.Stocks;

public class ComponentServiceTests
{
	private readonly IComponentRepository _componentRepository;
	private readonly ComponentService _componentService;

	public ComponentServiceTests()
	{
		_componentRepository = Substitute.For<IComponentRepository>();
		_componentService = new ComponentService(_componentRepository);
	}

	[Theory]
	[InlineData(StarshipName.Cargo, HullComponent.HullHc1, 1)]
	[InlineData(StarshipName.Cargo, EngineComponent.EngineEc1, 1)]
	[InlineData(StarshipName.Cargo, WingComponent.WingWc1, 2)]
	[InlineData(StarshipName.Cargo, ThrusterComponent.ThrusterTc1, 1)]
	[InlineData(StarshipName.Explorer, HullComponent.HullHe1, 1)]
	[InlineData(StarshipName.Explorer, EngineComponent.EngineEe1, 1)]
	[InlineData(StarshipName.Explorer, WingComponent.WingWe1, 2)]
	[InlineData(StarshipName.Explorer, ThrusterComponent.ThrusterTe1, 1)]
	[InlineData(StarshipName.Speeder, HullComponent.HullHs1, 1)]
	[InlineData(StarshipName.Speeder, EngineComponent.EngineEs1, 1)]
	[InlineData(StarshipName.Speeder, WingComponent.WingWs1, 2)]
	[InlineData(StarshipName.Speeder, ThrusterComponent.ThrusterTs1, 2)]
	public void GetComponentsOutFromStock_RemovesCorrectComponents(
		String starshipName,
		String componentName,
		Int32 quantity
	)
	{
		// Act
		_componentService.GetComponentsOutFromStock(componentName, quantity);

		// Assert
		_componentRepository.Received(quantity).Remove(componentName);
	}

	[Theory]
	[InlineData(
		StarshipName.Cargo,
		EngineComponent.EngineEc1,
		HullComponent.HullHc1,
		WingComponent.WingWc1,
		ThrusterComponent.ThrusterTc1
	)]
	[InlineData(
		StarshipName.Explorer,
		EngineComponent.EngineEe1,
		HullComponent.HullHe1,
		WingComponent.WingWe1,
		ThrusterComponent.ThrusterTe1
	)]
	[InlineData(
		StarshipName.Speeder,
		EngineComponent.EngineEs1,
		HullComponent.HullHs1,
		WingComponent.WingWs1,
		ThrusterComponent.ThrusterTs1
	)]
	public void GetStarshipComponentsCountFromInventories_ValidStarshipName_ReturnsCorrectCounts(
		String starshipName,
		String engine,
		String hull,
		String wing,
		String thruster
	)
	{
		// Arrange
		_componentRepository.GetCountByName(engine).Returns(5);
		_componentRepository.GetCountByName(hull).Returns(3);
		_componentRepository.GetCountByName(wing).Returns(8);
		_componentRepository.GetCountByName(thruster).Returns(2);

		// Act
		var result = _componentService.GetComponentsCountFromInventories(starshipName);

		// Assert
		Assert.Equal(5, result.Item1);
		Assert.Equal(3, result.Item2);
		Assert.Equal(8, result.Item3);
		Assert.Equal(2, result.Item4);
	}

	[Fact]
	public void GetStarshipComponentsCountFromInventories_UnknownStarshipName_ReturnsZeros()
	{
		// Arrange
		var starshipName = "UnknownStarship";

		// Act
		var result = _componentService.GetComponentsCountFromInventories(starshipName);

		// Assert
		Assert.Equal((0, 0, 0, 0), result);
	}

	[Fact]
	public void GetStarshipComponentsCountFromInventories_ExceptionThrown_ReturnsZeros()
	{
		// Arrange
		var starshipName = StarshipName.Cargo;
		_componentRepository
			.When(repo => repo.GetCountByName(Arg.Any<string>()))
			.Throw(new Exception());

		// Act
		var result = _componentService.GetComponentsCountFromInventories(starshipName);

		// Assert
		Assert.Equal((0, 0, 0, 0), result);
	}
}
