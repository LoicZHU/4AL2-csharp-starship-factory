namespace core.UI;

public static class UnknownInstructionDisplayHandler
{
	public static void PrintUnknownInstruction(String message)
	{
		UnknownInstructionTerminal.PrintUnknownInstruction(message);
		TerminalHelper.PrintLineBreak();
	}
}
