using core.UI;

namespace UnitTests.App.UI.Terminals;

public class TerminalTests
{
	private readonly StringWriter _stringWriter;
	private readonly TextWriter _originalOutput;

	public TerminalTests()
	{
		_stringWriter = new StringWriter();
		_originalOutput = Console.Out;
		Console.SetOut(_stringWriter);
	}

	~TerminalTests()
	{
		Console.SetOut(_originalOutput);
	}

	[Fact]
	public void PrintMessageWithoutLinebreak_WritesMessageToConsole()
	{
		// Arrange
		var message = "Test message";

		// Act
		Terminal.PrintMessageWithoutLinebreak(message);

		// Assert
		var output = _stringWriter.ToString();
		Assert.Equal(message, output);
	}

	[Fact]
	public void PrintMessageWithLinebreak_WritesMessageToConsoleWithLinebreak()
	{
		// Arrange
		var message = "Test message";

		// Act
		Terminal.PrintMessageWithLinebreak(message);

		// Assert
		var output = _stringWriter.ToString();
		Assert.Equal(message + Environment.NewLine, output);
	}

	[Fact]
	public void PrintLinebreak_WritesLinebreakToConsole()
	{
		// Act
		Terminal.PrintLinebreak();

		// Assert
		var output = _stringWriter.ToString();
		Assert.Equal(Environment.NewLine, output);
	}

	[Fact]
	public void PrintInvalidCommand_WritesMessageToConsoleWithLinebreak()
	{
		// Arrange
		var message = "Invalid command";

		// Act
		Terminal.PrintInvalidCommand(message);

		// Assert
		var output = _stringWriter.ToString();
		Assert.Equal(message + Environment.NewLine, output);
	}
}
