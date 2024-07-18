using core.Components;

namespace UnitTests.App.Shared.Constants.Stocks.Components;

public class WingComponentTests
{
	[Fact]
	public void WingsWe1Constant_ShouldHaveExpectedValue()
	{
		Assert.Equal("Wings_WE1", WingComponent.WingWe1);
	}

	[Fact]
	public void WingsWs1Constant_ShouldHaveExpectedValue()
	{
		Assert.Equal("Wings_WS1", WingComponent.WingWs1);
	}

	[Fact]
	public void WingsWc1Constant_ShouldHaveExpectedValue()
	{
		Assert.Equal("Wings_WC1", WingComponent.WingWc1);
	}
}
