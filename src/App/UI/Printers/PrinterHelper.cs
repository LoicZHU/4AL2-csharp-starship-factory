namespace core.UI;

public static class TerminalHelper
{
	public static void ColorizeMessageWithLinebreak(String message, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		Printer.PrintMessageWithLinebreak(message);
		Console.ResetColor();
	}

	public static void ColorizeMessageWithoutLinebreak(String message, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		Printer.PrintMessageWithoutLinebreak(message);
		Console.ResetColor();
	}

	public static void PrintLineBreak()
	{
		Printer.PrintMessageWithoutLinebreak("\n");
	}
}
