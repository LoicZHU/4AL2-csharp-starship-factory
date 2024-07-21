using core.App.Handlers;
using core.UI;

namespace UnitTests.App.CommandHandlers.Handlers;

public class ExistHandlerTests
{
	[Fact]
	public void Handle_CallsPrintExitMessage()
	{
		// Arrange
		var exitHandler = new ExitHandler();
		var message = "👋 Merci d'avoir utilisé Capsule Corp !";
		var stringWriter = new StringWriter();
		Console.SetOut(stringWriter);

		// Act
		exitHandler.Handle();

		// Assert
		var output = stringWriter.ToString();
		Assert.Contains(message, output);
		Assert.Contains("\n", output);
	}

	[Fact]
	public void PrintExitMessage_PrintsCorrectMessage()
	{
		// Arrange
		var message = "Test exit message";
		var stringWriter = new StringWriter();
		Console.SetOut(stringWriter);

		// Act
		ExitDisplayHandler.PrintExitMessage(message);

		// Assert
		var output = stringWriter.ToString();
		Assert.Contains(message, output);
		Assert.Contains("\n", output);
	}

	// [Fact]
	// public void ExitTerminal_PrintExitMessage_PrintsCorrectMessage()
	// {
	// 	// Arrange
	// 	var message = "Test exit terminal message";
	// 	var stringWriter = new StringWriter();
	// 	Console.SetOut(stringWriter);
	//
	// 	// Act
	// 	ExitTerminal.PrintExitMessage(message);
	//
	// 	// Assert
	// 	var output = stringWriter.ToString();
	// 	Assert.Equal(message, output);
	// }
	//
	// [Fact]
	// public void TerminalHelper_PrintLineBreak_PrintsLineBreak()
	// {
	// 	// Arrange
	// 	var stringWriter = new StringWriter();
	// 	Console.SetOut(stringWriter);
	//
	// 	// Act
	// 	TerminalHelper.PrintLineBreak();
	//
	// 	// Assert
	// 	var output = stringWriter.ToString();
	// 	Assert.Equal("\n", output);
	// }
}
