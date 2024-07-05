namespace core.UI;

public static class ExitDisplayHandler
{
	public static void PrintExitMessage(String message)
	{
		ExitTerminal.PrintExitMessage(message);
		TerminalHelper.PrintLineBreak();
	}
}
