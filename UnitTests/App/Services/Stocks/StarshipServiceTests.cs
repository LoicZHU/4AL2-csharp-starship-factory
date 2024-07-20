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
		_starshipService = new StarshipService(_starshipRepository, _componentRepository);
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
		var result = _starshipService.GetStarshipComponentsCountFromInventories(starshipName);

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
		var result = _starshipService.GetStarshipComponentsCountFromInventories(starshipName);

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
		var result = _starshipService.GetStarshipComponentsCountFromInventories(starshipName);

		// Assert
		Assert.Equal((0, 0, 0, 0), result);
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
