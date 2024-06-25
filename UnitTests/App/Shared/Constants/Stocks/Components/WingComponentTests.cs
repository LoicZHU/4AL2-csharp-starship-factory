using core.Components;

namespace UnitTests.App.Shared.Constants.Stocks.Components;

public class WingComponentTests
{
	[Fact]
	public void WingsWe1Constant_ShouldHaveExpectedValue()
	{
		Assert.Equal("Wings_WE1", WingComponent.WingsWe1);
	}

	[Fact]
	public void WingsWs1Constant_ShouldHaveExpectedValue()
	{
		Assert.Equal("Wings_WS1", WingComponent.WingsWs1);
	}

	[Fact]
	public void WingsWc1Constant_ShouldHaveExpectedValue()
	{
		Assert.Equal("Wings_WC1", WingComponent.WingsWc1);
	}
}
