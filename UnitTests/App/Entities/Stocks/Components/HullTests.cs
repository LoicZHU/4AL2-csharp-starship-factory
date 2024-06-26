using core.Components;
using core.Items.Components;

namespace UnitTests.App.Entities.Stocks.Components;

public class HullTests
{
	[Fact]
	public void Hull_ShouldImplementIComponent()
	{
		// Arrange
		var name = HullComponent.HullHe1;

		// Act
		var hull = Hull.Create(name);

		// Assert
		Assert.IsAssignableFrom<IComponent>(hull);
	}

	[Fact]
	public void Create_ShouldReturnHullWithValidIdAndName()
	{
		// Arrange
		var name = HullComponent.HullHe1;

		// Act
		var hull = Hull.Create(name);

		// Assert
		Assert.NotNull(hull);
		Assert.NotEqual(Guid.Empty, hull.Id);
		Assert.Equal(name, hull.Name);
	}

	[Fact]
	public void Create_ShouldReturnDifferentIdsForDifferentHulls()
	{
		// Arrange
		var name1 = HullComponent.HullHe1;
		var name2 = HullComponent.HullHs1;

		// Act
		var hull1 = Hull.Create(name1);
		var hull2 = Hull.Create(name2);

		// Assert
		Assert.NotEqual(hull1.Id, hull2.Id);
	}
}
