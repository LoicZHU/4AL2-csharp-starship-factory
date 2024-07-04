namespace core.UI;

public static class SendDisplayHandler
{
	public static void PrintInvalidCommand(String message)
	{
		SendTerminal.PrintInvalidCommand(message);
		TerminalHelper.PrintLineBreak();
	}

	public static void PrintCompletedMessage(String message)
	{
		SendTerminal.PrintCompletedMessage(message);
		TerminalHelper.PrintLineBreak();
	}

	public static void PrintOrderRemainingStarships(String message)
	{
		SendTerminal.PrintOrderRemainingStarships(message);
		TerminalHelper.PrintLineBreak();
	}
}
