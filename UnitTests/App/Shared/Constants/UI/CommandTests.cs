using core.UI.constants;

namespace UnitTests.App.Shared.Constants.UI;

public class CommandTests
{
	[Fact]
	public void HelpConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("HELP", Command.Help);
	}

	[Fact]
	public void ExitConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("EXIT", Command.Exit);
	}

	[Fact]
	public void InstructionsConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("INSTRUCTIONS", Command.Instructions);
	}

	[Fact]
	public void ListOrderConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("LIST_ORDER", Command.ListOrder);
	}

	[Fact]
	public void NeededStocksConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("NEEDED_STOCKS", Command.NeededStocks);
	}

	[Fact]
	public void OrderConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("ORDER", Command.Order);
	}

	[Fact]
	public void ProduceConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("PRODUCE", Command.Produce);
	}

	[Fact]
	public void SendConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("SEND", Command.Send);
	}

	[Fact]
	public void StocksConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("STOCKS", Command.Stocks);
	}

	[Fact]
	public void VerifyConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("VERIFY", Command.Verify);
	}
}
