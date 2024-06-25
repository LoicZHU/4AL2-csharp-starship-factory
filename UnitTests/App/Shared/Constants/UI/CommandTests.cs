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
	public void NeededStocksConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("NEEDED_STOCKS", Command.NeededStocks);
	}

	[Fact]
	public void ProduceConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("PRODUCE", Command.Produce);
	}

	[Fact]
	public void StocksConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("STOCKS", Command.Stocks);
	}

	[Fact]
	public void UserInstructionConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("[USER_INSTRUCTION]", Command.UserInstruction);
	}

	[Fact]
	public void UserInstructionsConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("USER_INSTRUCTIONS", Command.UserInstructions);
	}

	[Fact]
	public void VerifyConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("VERIFY", Command.Verify);
	}
}
