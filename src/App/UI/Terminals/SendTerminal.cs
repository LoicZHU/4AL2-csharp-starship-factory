using core.UI.constants;

namespace core.UI;

public static class SendTerminal
{
	public static void PrintInvalidCommand(String message)
	{
		Terminal.PrintInvalidCommand(message);
	}

	public static void PrintCompletedMessage(String message)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak(Send.Completed, ConsoleColor.Yellow);
		Terminal.PrintMessageWithLinebreak($" {message}");
	}

	public static void PrintOrderRemainingStarships(String message)
	{
		Terminal.PrintMessageWithLinebreak(message);
	}
}
