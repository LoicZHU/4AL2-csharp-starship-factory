namespace core.UI;

public static class ExitPrintingHandler
{
	public static void PrintExitMessage(String message)
	{
		ExitPrinter.PrintExitMessage(message);
		TerminalHelper.PrintLineBreak();
	}
}
