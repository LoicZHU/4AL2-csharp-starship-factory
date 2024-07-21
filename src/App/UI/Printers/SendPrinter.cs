using core.UI.constants;

namespace core.UI;

public static class SendPrinter
{
	public static void PrintInvalidCommand(String message)
	{
		Printer.PrintInvalidCommand(message);
	}

	public static void PrintCompletedMessage(String message)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak(Send.Completed, ConsoleColor.Yellow);
		Printer.PrintMessageWithLinebreak($" {message}");
	}

	public static void PrintOrderRemainingStarships(String message)
	{
		Printer.PrintMessageWithLinebreak(message);
	}
}
