using core.Components;

namespace UnitTests.App.Shared.Constants.Stocks.Components;

public class EngineComponentTests
{
	[Fact]
	public void EngineEe1Constant_ShouldHaveExpectedValue()
	{
		Assert.Equal("Engine_EE1", EngineComponent.EngineEe1);
	}

	[Fact]
	public void EngineEs1Constant_ShouldHaveExpectedValue()
	{
		Assert.Equal("Engine_ES1", EngineComponent.EngineEs1);
	}

	[Fact]
	public void EngineEc1Constant_ShouldHaveExpectedValue()
	{
		Assert.Equal("Engine_EC1", EngineComponent.EngineEc1);
	}
}
