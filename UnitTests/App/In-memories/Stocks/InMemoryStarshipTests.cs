using core.Components;
using core.In_memories.Items;
using core.Starships;

namespace UnitTests.Stocks;

public class InMemoryStarshipTests
{
	private readonly InMemoryStarship _repository;

	public InMemoryStarshipTests()
	{
		_repository = new InMemoryStarship();
	}

	[Fact]
	public void Add_AddsStarshipToCache()
	{
		// Arrange
		var starship = Starship.Create(
			StarshipName.Cargo,
			Hull.Create(HullComponent.HullHc1),
			new List<Engine> { Engine.Create(EngineComponent.EngineEc1) },
			(Wing.Create(WingComponent.WingWc1), Wing.Create(WingComponent.WingWc1)),
			new List<Thruster> { Thruster.Create(ThrusterComponent.ThrusterTc1) }
		);

		// Act
		_repository.Add(starship);

		// Assert
		var cachedStarship = _repository.GetAll().FirstOrDefault(s => s.Id == starship.Id);
		Assert.NotNull(cachedStarship);
		Assert.Equal(starship.Name, cachedStarship.Name);
	}

	[Fact]
	public void Add_ThrowsExceptionForDuplicateId()
	{
		// Arrange
		var starship = Starship.Create(
			StarshipName.Cargo,
			Hull.Create(HullComponent.HullHc1),
			new List<Engine> { Engine.Create(EngineComponent.EngineEc1) },
			(Wing.Create(WingComponent.WingWc1), Wing.Create(WingComponent.WingWc1)),
			new List<Thruster> { Thruster.Create(ThrusterComponent.ThrusterTc1) }
		);
		_repository.Add(starship);

		// Act & Assert
		var ex = Assert.Throws<ArgumentException>(() => _repository.Add(starship));
		Assert.Equal($"Vaisseau ID {starship.Id} déjà existant", ex.Message);
	}

	[Fact]
	public void GetAll_ReturnsAllStarships()
	{
		// Act
		var starships = _repository.GetAll();

		// Assert
		Assert.Equal(4, starships.Count);
	}

	[Fact]
	public void Exists_ReturnsTrueForExistingStarshipName()
	{
		// Act
		var exists = _repository.Exists(StarshipName.Explorer);

		// Assert
		Assert.True(exists);
	}

	[Fact]
	public void Exists_ReturnsFalseForNonExistingStarshipName()
	{
		// Act
		var exists = _repository.Exists("NonExistingStarship");

		// Assert
		Assert.False(exists);
	}

	// [Fact]
	// public void Remove_RemovesStarshipByName()
	// {
	// 	// Act
	// 	_repository.Remove(StarshipName.Explorer);
	//
	// 	// Assert
	// 	var exists = _repository.Exists(StarshipName.Explorer);
	// 	Assert.False(exists);
	// }

	[Fact]
	public void GetStock_ReturnsCorrectStock()
	{
		// Act
		var stock = _repository.GetStock();

		// Assert
		Assert.Equal(2, stock[StarshipName.Explorer]);
		Assert.Equal(1, stock[StarshipName.Speeder]);
		Assert.Equal(1, stock[StarshipName.Cargo]);
	}
}
