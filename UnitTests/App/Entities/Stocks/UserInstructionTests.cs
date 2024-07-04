using core;

namespace UnitTests.App.Entities.Stocks;

public class UserInstructionTests
{
	[Fact]
	public void Create_ShouldReturnUserInstructionWithValidIdAndInstructions()
	{
		// Arrange
		var instructions = new Dictionary<String, Int32>
		{
			{ "Instruction1", 1 },
			{ "Instruction2", 2 }
		};

		// Act
		var userInstruction = Order.Create(instructions);

		// Assert
		Assert.NotNull(userInstruction);
		Assert.NotEqual(Guid.Empty, userInstruction.Id);
		Assert.Equal(instructions, userInstruction.Orders);
	}

	[Fact]
	public void Add_ShouldAddNewInstruction_WhenNameDoesNotExist()
	{
		// Arrange
		var instructions = new Dictionary<String, Int32>();
		var userInstruction = Order.Create(instructions);
		var newInstruction = "NewInstruction";
		var quantity = 5;

		// Act
		userInstruction.Add(newInstruction, quantity);

		// Assert
		Assert.True(userInstruction.Orders.ContainsKey(newInstruction));
		Assert.Equal(quantity, userInstruction.Orders[newInstruction]);
	}

	[Fact]
	public void Add_ShouldUpdateExistingInstruction_WhenNameExists()
	{
		// Arrange
		var instructions = new Dictionary<String, Int32> { { "Instruction1", 1 } };
		var userInstruction = Order.Create(instructions);
		var existingInstruction = "Instruction1";
		var quantity = 5;

		// Act
		userInstruction.Add(existingInstruction, quantity);

		// Assert
		Assert.True(userInstruction.Orders.ContainsKey(existingInstruction));
		Assert.Equal(6, userInstruction.Orders[existingInstruction]);
	}
}
