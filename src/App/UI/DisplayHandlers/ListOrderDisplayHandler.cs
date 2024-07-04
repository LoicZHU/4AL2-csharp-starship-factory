namespace core.UI;

public static class ListOrderDisplayHandler
{
	public static void PrintInvalidCommand(String message)
	{
		ListOrderTerminal.PrintInvalidCommand(message);
		TerminalHelper.PrintLineBreak();
	}

	public static void PrintNoOrders(String message)
	{
		ListOrderTerminal.PrintMessage(message);
		TerminalHelper.PrintLineBreak();
	}

	public static void PrintListOrder(String message)
	{
		ListOrderTerminal.PrintMessage(message);
		TerminalHelper.PrintLineBreak();
	}
}
