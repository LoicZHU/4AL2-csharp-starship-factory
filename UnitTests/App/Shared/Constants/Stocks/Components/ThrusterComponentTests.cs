using core.Components;

namespace UnitTests.App.Shared.Constants.Stocks.Components;

public class ThrusterComponentTests
{
	[Fact]
	public void ThrusterTe1Constant_ShouldHaveExpectedValue()
	{
		Assert.Equal("Thruster_TE1", ThrusterComponent.ThrusterTe1);
	}

	[Fact]
	public void ThrusterTs1Constant_ShouldHaveExpectedValue()
	{
		Assert.Equal("Thruster_TS1", ThrusterComponent.ThrusterTs1);
	}

	[Fact]
	public void ThrusterTc1Constant_ShouldHaveExpectedValue()
	{
		Assert.Equal("Thruster_TC1", ThrusterComponent.ThrusterTc1);
	}
}
