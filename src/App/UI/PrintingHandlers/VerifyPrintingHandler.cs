namespace core.UI;

public static class VerifyPrintingHandler
{
	public static void PrintInvalidCommand(String message)
	{
		VerifyPrinter.PrintError(message);
		Printer.PrintLinebreak();
	}

	public static void PrintSufficientStock()
	{
		VerifyPrinter.PrintAvailableMessage();
	}

	public static void PrintInsufficientStock()
	{
		VerifyPrinter.PrintUnavailableMessage();
	}
}
