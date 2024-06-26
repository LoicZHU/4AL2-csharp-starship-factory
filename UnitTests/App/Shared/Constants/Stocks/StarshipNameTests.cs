using core.Starships;

namespace UnitTests.App.Shared.Constants.Stocks;

public class StarshipNameTests
{
	[Fact]
	public void CargoConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("Cargo", StarshipName.Cargo);
	}

	[Fact]
	public void ExplorerConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("Explorer", StarshipName.Explorer);
	}

	[Fact]
	public void SpeederConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("Speeder", StarshipName.Speeder);
	}

	[Fact]
	public void UnknownConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("Unknown", StarshipName.Unknown);
	}
}
