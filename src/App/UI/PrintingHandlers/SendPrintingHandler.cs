namespace core.UI;

public static class SendPrintingHandler
{
	public static void PrintInvalidCommand(String message)
	{
		SendPrinter.PrintInvalidCommand(message);
		TerminalHelper.PrintLineBreak();
	}

	public static void PrintCompletedMessage(String message)
	{
		SendPrinter.PrintCompletedMessage(message);
		TerminalHelper.PrintLineBreak();
	}

	public static void PrintOrderRemainingStarships(String message)
	{
		SendPrinter.PrintOrderRemainingStarships(message);
		TerminalHelper.PrintLineBreak();
	}
}
