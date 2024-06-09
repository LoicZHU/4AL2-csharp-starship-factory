namespace core.UI;

public static class VerifyDisplayHandler
{
	public static void PrintInvalidCommand(String message)
	{
		VerifyTerminal.PrintError(message);
		Terminal.PrintLinebreak();
	}

	public static void PrintSufficientStock()
	{
		VerifyTerminal.PrintAvailableMessage();
	}

	public static void PrintInsufficientStock()
	{
		VerifyTerminal.PrintUnavailableMessage();
	}
}
