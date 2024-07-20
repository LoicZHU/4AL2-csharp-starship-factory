using core.InputHandlers;
using core.UI.constants;

namespace UnitTests.App.CommandHandlers.InputHandlers;

public class UnknownInstructionHandlerTests
{
	[Fact]
	public void Handle_PrintsUnknownInstructionMessage()
	{
		// Arrange
		var unknownInstructionHandler = new UnknownInstructionHandlerWithArgs();
		var input = "invalid_command";
		var expectedMessage =
			$"🚫 Instruction inconnue : {input} ({Command.Help} pour de l'aide) :";
		var stringWriter = new StringWriter();
		Console.SetOut(stringWriter);

		// Act
		unknownInstructionHandler.Handle(input);

		// Assert
		var output = stringWriter.ToString();
		Assert.Contains(expectedMessage, output);
	}
}
