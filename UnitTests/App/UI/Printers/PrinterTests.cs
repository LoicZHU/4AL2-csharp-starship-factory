using core.UI;

namespace UnitTests.App.UI.Printers;

public class PrinterTests
{
	private readonly StringWriter _stringWriter;
	private readonly TextWriter _originalOutput;

	public PrinterTests()
	{
		_stringWriter = new StringWriter();
		_originalOutput = Console.Out;
		Console.SetOut(_stringWriter);
	}

	~PrinterTests()
	{
		Console.SetOut(_originalOutput);
	}

	[Fact]
	public void PrintMessageWithoutLinebreak_WritesMessageToConsole()
	{
		// Arrange
		var message = "Test message";

		// Act
		Printer.PrintMessageWithoutLinebreak(message);

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
		Printer.PrintMessageWithLinebreak(message);

		// Assert
		var output = _stringWriter.ToString();
		Assert.Equal(message + Environment.NewLine, output);
	}

	[Fact]
	public void PrintLinebreak_WritesLinebreakToConsole()
	{
		// Act
		Printer.PrintLinebreak();

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
		Printer.PrintInvalidCommand(message);

		// Assert
		var output = _stringWriter.ToString();
		Assert.Equal(message + Environment.NewLine, output);
	}
}
