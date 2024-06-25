using core.Components;

namespace UnitTests.App.Shared.Constants.Stocks.Components;

public class HullComponentTests
{
	[Fact]
	public void HullHe1Constant_ShouldHaveExpectedValue()
	{
		Assert.Equal("Hull_HE1", HullComponent.HullHe1);
	}

	[Fact]
	public void HullHs1Constant_ShouldHaveExpectedValue()
	{
		Assert.Equal("Hull_HS1", HullComponent.HullHs1);
	}

	[Fact]
	public void HullHc1Constant_ShouldHaveExpectedValue()
	{
		Assert.Equal("Hull_HC1", HullComponent.HullHc1);
	}
}
