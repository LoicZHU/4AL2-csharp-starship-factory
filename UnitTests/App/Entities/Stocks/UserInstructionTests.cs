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
		var userInstruction = UserInstruction.Create(instructions);

		// Assert
		Assert.NotNull(userInstruction);
		Assert.NotEqual(Guid.Empty, userInstruction.Id);
		Assert.Equal(instructions, userInstruction.Instructions);
	}

	[Fact]
	public void Add_ShouldAddNewInstruction_WhenNameDoesNotExist()
	{
		// Arrange
		var instructions = new Dictionary<String, Int32>();
		var userInstruction = UserInstruction.Create(instructions);
		var newInstruction = "NewInstruction";
		var quantity = 5;

		// Act
		userInstruction.Add(newInstruction, quantity);

		// Assert
		Assert.True(userInstruction.Instructions.ContainsKey(newInstruction));
		Assert.Equal(quantity, userInstruction.Instructions[newInstruction]);
	}

	[Fact]
	public void Add_ShouldUpdateExistingInstruction_WhenNameExists()
	{
		// Arrange
		var instructions = new Dictionary<String, Int32> { { "Instruction1", 1 } };
		var userInstruction = UserInstruction.Create(instructions);
		var existingInstruction = "Instruction1";
		var quantity = 5;

		// Act
		userInstruction.Add(existingInstruction, quantity);

		// Assert
		Assert.True(userInstruction.Instructions.ContainsKey(existingInstruction));
		Assert.Equal(6, userInstruction.Instructions[existingInstruction]);
	}
}
