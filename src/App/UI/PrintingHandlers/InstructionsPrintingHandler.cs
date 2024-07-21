namespace core.UI;

public static class InstructionsPrintingHandler
{
	public static void PrintInvalidCommand(String message)
	{
		Printer.PrintInvalidCommand(message);
	}

	public static void PrintAssemblingComponents(
		List<String> components,
		String componentToAdd
	)
	{
		InstructionsPrinter.PrintAssemblingComponents(components, componentToAdd);
	}

	public static void PrintGetOutStock(Int32 quantity, String componentModel)
	{
		InstructionsPrinter.PrintGetOutStock(quantity, componentModel);
	}

	public static void PrintStarshipProductionStarting(String starshipModel, Int32 i)
	{
		InstructionsPrinter.PrintStarshipProductionStarting(starshipModel, i);
	}

	public static void PrintStarshipProductionFinishing(String starshipModel, Int32 i)
	{
		InstructionsPrinter.PrintStarshipProductionFinishing(starshipModel, i);
	}
}
