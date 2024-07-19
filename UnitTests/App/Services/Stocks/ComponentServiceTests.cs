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
}
