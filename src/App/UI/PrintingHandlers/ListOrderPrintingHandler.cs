namespace core.UI;

public static class ListOrderPrintingHandler
{
	public static void PrintInvalidCommand(String message)
	{
		ListOrderPrinter.PrintInvalidCommand(message);
		TerminalHelper.PrintLineBreak();
	}

	public static void PrintNoOrders(String message)
	{
		ListOrderPrinter.PrintMessage(message);
		TerminalHelper.PrintLineBreak();
	}

	public static void PrintListOrder(String message)
	{
		ListOrderPrinter.PrintMessage(message);
		TerminalHelper.PrintLineBreak();
	}
}
