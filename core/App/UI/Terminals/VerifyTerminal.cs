using core.UI.constants;
using static System.ConsoleColor;

namespace core.UI;

public static class VerifyTerminal
{
	public static void PrintError(String message)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak($"{Verification.Error}", Red);
		Console.WriteLine($" {message}");
	}

	public static void PrintAvailableMessage()
	{
		TerminalHelper.ColorizeMessageWithLinebreak(Verification.Available, Yellow);
	}

	public static void PrintUnavailableMessage()
	{
		TerminalHelper.ColorizeMessageWithLinebreak(Verification.Unavailable, Yellow);
	}
}
