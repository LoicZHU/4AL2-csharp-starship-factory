namespace core.UI;

public class HelpPrinter
{
	public static void PrintHelpMenu()
	{
		Printer.PrintLinebreak();
		Printer.PrintMessageWithLinebreak("Commandes disponibles :");
		MenuPrinter.PrintHelp();
		Printer.PrintLinebreak();
	}
}
