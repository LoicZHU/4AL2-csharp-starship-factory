using core.UI.constants;

namespace core.UI;

public static class MenuPrintingHandler
{
	public static void PrintWelcome()
	{
		Printer.PrintLinebreak();
		MenuPrinter.PrintWelcome("🚀 Bienvenue chez Capsule Corp ! 🚀");
		Printer.PrintLinebreak();
		Printer.PrintLinebreak();
	}

	public static void PrintUserInteractionInvitation()
	{
		MenuPrinter.PrintUserInteractionInvitation(
			$"🕹 Entrez une instruction ({Command.Help} pour de l'aide) :"
		);
		Printer.PrintLinebreak();
	}

	public static void PrintEmptyInstructionMessage(String message)
	{
		MenuPrinter.PrintEmptyInstructionMessage(message);
		Printer.PrintLinebreak();
	}
}
