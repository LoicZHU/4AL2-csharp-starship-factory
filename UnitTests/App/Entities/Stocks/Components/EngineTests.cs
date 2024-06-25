using core.Components;
using core.Items.Components;

namespace UnitTests.App.Entities.Stocks.Components;

public class EngineTests
{
	[Fact]
	public void Engine_ShouldImplementIComponent()
	{
		// Arrange
		var name = EngineComponent.EngineEe1;

		// Act
		var engine = Engine.Create(name);

		// Assert
		Assert.IsAssignableFrom<IComponent>(engine);
	}

	[Fact]
	public void Create_ShouldReturnEngineWithValidIdAndName()
	{
		// Arrange
		var name = EngineComponent.EngineEe1;

		// Act
		var engine = Engine.Create(name);

		// Assert
		Assert.NotNull(engine);
		Assert.NotEqual(Guid.Empty, engine.Id);
		Assert.Equal(name, engine.Name);
	}

	[Fact]
	public void Create_ShouldReturnDifferentIdsForDifferentEngines()
	{
		// Arrange
		var name1 = EngineComponent.EngineEe1;
		var name2 = EngineComponent.EngineEs1;

		// Act
		var engine1 = Engine.Create(name1);
		var engine2 = Engine.Create(name2);

		// Assert
		Assert.NotEqual(engine1.Id, engine2.Id);
	}
}
