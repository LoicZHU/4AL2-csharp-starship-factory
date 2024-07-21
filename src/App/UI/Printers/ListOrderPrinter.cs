namespace core.UI;

public static class ListOrderPrinter
{
	public static void PrintInvalidCommand(String message)
	{
		Printer.PrintInvalidCommand(message);
	}

	public static void PrintMessage(String message)
	{
		Printer.PrintMessageWithoutLinebreak(message);
	}
}
