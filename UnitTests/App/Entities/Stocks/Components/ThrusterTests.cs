using core.Components;
using core.Items.Components;

namespace UnitTests.App.Entities.Stocks.Components;

public class ThrusterTests
{
	[Fact]
	public void Thruster_ShouldImplementIComponent()
	{
		// Arrange
		var name = ThrusterComponent.ThrusterTe1;

		// Act
		var thruster = Thruster.Create(name);

		// Assert
		Assert.IsAssignableFrom<IComponent>(thruster);
	}

	[Fact]
	public void Create_ShouldReturnThrusterWithValidIdAndName()
	{
		// Arrange
		var name = ThrusterComponent.ThrusterTe1;

		// Act
		var thruster = Thruster.Create(name);

		// Assert
		Assert.NotNull(thruster);
		Assert.NotEqual(Guid.Empty, thruster.Id);
		Assert.Equal(name, thruster.Name);
	}

	[Fact]
	public void Create_ShouldReturnDifferentIdsForDifferentThrusters()
	{
		// Arrange
		var name1 = ThrusterComponent.ThrusterTe1;
		var name2 = ThrusterComponent.ThrusterTs1;

		// Act
		var thruster1 = Thruster.Create(name1);
		var thruster2 = Thruster.Create(name2);

		// Assert
		Assert.NotEqual(thruster1.Id, thruster2.Id);
	}
}
