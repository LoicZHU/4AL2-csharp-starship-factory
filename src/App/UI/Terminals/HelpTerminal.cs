namespace core.UI;

public class HelpTerminal
{
	public static void PrintHelpMenu()
	{
		Terminal.PrintLinebreak();
		Terminal.PrintMessageWithLinebreak("Commandes disponibles :");
		MenuTerminal.PrintHelp();
		Terminal.PrintLinebreak();
	}
}
