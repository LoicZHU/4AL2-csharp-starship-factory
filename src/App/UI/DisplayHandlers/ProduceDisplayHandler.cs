namespace core.UI;

public static class ProduceDisplayHandler
{
	public static void PrintInvalidCommand(String message)
	{
		ProduceTerminal.PrintError(message);
	}

	public static void PrintStockUpdated()
	{
		ProduceTerminal.PrintStockUpdated();
	}
}
