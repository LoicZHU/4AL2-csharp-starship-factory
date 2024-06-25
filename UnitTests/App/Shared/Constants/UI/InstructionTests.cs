using core.UI.constants;

namespace UnitTests.App.Shared.Constants.UI;

public class InstructionTests
{
	[Fact]
	public void AssembleConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("ASSEMBLE", Instruction.Assemble);
	}

	[Fact]
	public void GetOutStockConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("GET_OUT_STOCK", Instruction.GetOutStock);
	}

	[Fact]
	public void FinishedConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("FINISHED", Instruction.Finished);
	}

	[Fact]
	public void ProducingConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("PRODUCING", Instruction.Producing);
	}
}
