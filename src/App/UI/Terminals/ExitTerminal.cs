namespace core.UI;

public static class ExitTerminal
{
	public static void PrintExitMessage(String message)
	{
		Terminal.PrintMessageWithoutLinebreak(message);
	}
}
