using core.Components;
using core.Repositories.ComponentRepository;
using core.Repositories.StarshipRepository;
using core.Services;
using core.Starships;
using NSubstitute;

namespace UnitTests.App.Services.Stocks;

public class StarshipServiceTests
{
	private readonly IComponentRepository _componentRepository;
	private readonly IStarshipRepository _starshipRepository;
	private readonly StarshipService _starshipService;

	public StarshipServiceTests()
	{
		_componentRepository = Substitute.For<IComponentRepository>();
		_starshipRepository = Substitute.For<IStarshipRepository>();
		_starshipService = new StarshipService(_starshipRepository);
	}

	[Fact]
	public void GetStarshipSumsFromInput_ValidInput_ReturnsCorrectSums()
	{
		// Arrange
		var input = "1 Explorer, 2 Cargo, 3 Explorer";
		Action<String> printInvalidCommand = Substitute.For<Action<String>>();

		// Act
		var result = _starshipService.GetStarshipSumsFromInput(input, printInvalidCommand);

		// Assert
		Assert.Equal(2, result[StarshipName.Cargo]);
		Assert.Equal(4, result[StarshipName.Explorer]);
	}

	[Fact]
	public void GetStarshipSumsFromInput_InvalidInput_PrintsErrorMessageAndReturnsEmpty()
	{
		// Arrange
		var input = "1 InvalidStarship";
		var invalidCommandMessage = String.Empty;
		Action<String> printInvalidCommand = msg => invalidCommandMessage = msg;

		// Act
		var result = _starshipService.GetStarshipSumsFromInput(input, printInvalidCommand);

		// Assert
		Assert.Equal("Vaisseau inconnu : Invalidstarship", invalidCommandMessage);
		Assert.Empty(result);
	}

	[Fact]
	public void AddStarship_CallsRepositoryAddMethod()
	{
		// Arrange
		var starship = StarshipBuilder
			.create()
			.WithName(StarshipName.Cargo)
			.WithEngines(new() { Engine.Create(EngineComponent.EngineEc1) })
			.WithHull(Hull.Create(HullComponent.HullHc1))
			.WithWingPair(
				(Wing.Create(WingComponent.WingWc1), Wing.Create(WingComponent.WingWc1))
			)
			.WithThrusters(new() { Thruster.Create(ThrusterComponent.ThrusterTc1) })
			.Build();

		// Act
		_starshipService.AddStarship(starship);

		// Assert
		_starshipRepository.Received(1).Add(starship);
	}
}
