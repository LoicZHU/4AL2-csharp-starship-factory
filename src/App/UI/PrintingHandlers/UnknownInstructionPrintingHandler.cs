namespace core.UI;

public static class UnknownInstructionPrintingHandler
{
	public static void PrintUnknownInstruction(String message)
	{
		UnknownInstructionPrinter.PrintUnknownInstruction(message);
		TerminalHelper.PrintLineBreak();
	}
}
