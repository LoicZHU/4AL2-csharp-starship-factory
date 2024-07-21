using core.UI.constants;
using static System.ConsoleColor;

namespace core.UI;

public static class VerifyPrinter
{
	public static void PrintError(String message)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak($"{Verification.Error}", Red);
		Printer.PrintMessageWithLinebreak($" {message}");
	}

	public static void PrintAvailableMessage()
	{
		TerminalHelper.ColorizeMessageWithLinebreak(Verification.Available, Yellow);
		Printer.PrintLinebreak();
	}

	public static void PrintUnavailableMessage()
	{
		TerminalHelper.ColorizeMessageWithLinebreak(Verification.Unavailable, Yellow);
		Printer.PrintLinebreak();
	}
}
