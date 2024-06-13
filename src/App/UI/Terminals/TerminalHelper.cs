namespace core.UI;

public static class TerminalHelper
{
	public static void ColorizeMessageWithLinebreak(String message, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		Terminal.PrintMessageWithLinebreak(message);
		Console.ResetColor();
	}

	public static void ColorizeMessageWithoutLinebreak(String message, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		Terminal.PrintMessageWithoutLinebreak(message);
		Console.ResetColor();
	}

	public static void PrintLineBreak()
	{
		Terminal.PrintMessageWithoutLinebreak("\n");
	}
}
