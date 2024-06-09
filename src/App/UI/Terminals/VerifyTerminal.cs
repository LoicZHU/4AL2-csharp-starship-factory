using core.UI.constants;
using static System.ConsoleColor;

namespace core.UI;

public static class VerifyTerminal
{
	public static void PrintError(String message)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak($"{Verification.Error}", Red);
		Terminal.PrintMessageWithLinebreak($" {message}");
	}

	public static void PrintAvailableMessage()
	{
		TerminalHelper.ColorizeMessageWithLinebreak(Verification.Available, Yellow);
		Terminal.PrintLinebreak();
	}

	public static void PrintUnavailableMessage()
	{
		TerminalHelper.ColorizeMessageWithLinebreak(Verification.Unavailable, Yellow);
		Terminal.PrintLinebreak();
	}
}
