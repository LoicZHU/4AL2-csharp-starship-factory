namespace core.UI;

public static class TerminalHelper
{
	public static void ColorizeMessageWithLinebreak(String message, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		Console.WriteLine(message);
		Console.ResetColor();
	}

	public static void ColorizeMessageWithoutLinebreak(String message, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		Console.Write(message);
		Console.ResetColor();
	}

	public static void PrintLineBreak()
	{
		Console.Write("\n");
	}
}
