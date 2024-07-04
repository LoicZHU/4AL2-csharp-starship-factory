namespace core.UI;

public static class Terminal
{
	public static void PrintMessageWithoutLinebreak(String message)
	{
		Console.Write(message);
	}

	public static void PrintMessageWithLinebreak(String message)
	{
		Console.WriteLine(message);
	}

	public static void PrintLinebreak()
	{
		PrintMessageWithoutLinebreak("\n");
	}

	public static void PrintInvalidCommand(String message)
	{
		PrintMessageWithLinebreak(message);
	}
}
