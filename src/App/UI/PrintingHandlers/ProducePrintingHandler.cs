namespace core.UI;

public static class ProducePrintingHandler
{
	public static void PrintInvalidCommand(String message)
	{
		ProducePrinter.PrintError(message);
	}

	public static void PrintStockUpdated()
	{
		ProducePrinter.PrintStockUpdated();
	}
}
