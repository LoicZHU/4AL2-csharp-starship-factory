namespace core.UI;

public static class ListOrderTerminal
{
	public static void PrintInvalidCommand(String message)
	{
		Terminal.PrintInvalidCommand(message);
	}

	public static void PrintMessage(String message)
	{
		Terminal.PrintMessageWithoutLinebreak(message);
	}
}
