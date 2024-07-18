using core.Components;
using core.Items.Components;

namespace UnitTests.App.Entities.Stocks.Components;

public class WingTests
{
	[Fact]
	public void Wing_ShouldImplementIComponent()
	{
		// Arrange
		var name = WingComponent.WingWe1;

		// Act
		var wing = Wing.Create(name);

		// Assert
		Assert.IsAssignableFrom<IComponent>(wing);
	}

	[Fact]
	public void Create_ShouldReturnWingWithValidIdAndName()
	{
		// Arrange
		var name = WingComponent.WingWe1;

		// Act
		var wing = Wing.Create(name);

		// Assert
		Assert.NotNull(wing);
		Assert.NotEqual(Guid.Empty, wing.Id);
		Assert.Equal(name, wing.Name);
	}

	[Fact]
	public void Create_ShouldReturnDifferentIdsForDifferentWings()
	{
		// Arrange
		var name1 = WingComponent.WingWe1;
		var name2 = WingComponent.WingWs1;

		// Act
		var wing1 = Wing.Create(name1);
		var wing2 = Wing.Create(name2);

		// Assert
		Assert.NotEqual(wing1.Id, wing2.Id);
	}
}
