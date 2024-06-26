using core.UI.constants;

namespace UnitTests.App.Shared.Constants.UI;

public class ProductionTests
{
	[Fact]
	public void StockUpdatedConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("STOCK_UPDATED", Production.StockUpdated);
	}
}
