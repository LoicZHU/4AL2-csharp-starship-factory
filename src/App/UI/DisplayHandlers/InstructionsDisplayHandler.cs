using core.Assemblies;

namespace core.UI;

public static class InstructionsDisplayHandler
{
	public static void PrintInvalidCommand(String message)
	{
		Terminal.PrintInvalidCommand(message);
	}

	public static void PrintAssemblingComponents(
		ComponentAssembly componentAssembly,
		String componentToAdd
	)
	{
		InstructionsTerminal.PrintAssemblingComponents(componentAssembly, componentToAdd);
	}

	public static void PrintGetOutStock(Int32 quantity, String componentModel)
	{
		InstructionsTerminal.PrintGetOutStock(quantity, componentModel);
	}

	public static void PrintStarshipProductionStarting(String starshipModel, Int32 i)
	{
		InstructionsTerminal.PrintStarshipProductionStarting(starshipModel, i);
	}

	public static void PrintStarshipProductionFinishing(String starshipModel, Int32 i)
	{
		InstructionsTerminal.PrintStarshipProductionFinishing(starshipModel, i);
	}
}
