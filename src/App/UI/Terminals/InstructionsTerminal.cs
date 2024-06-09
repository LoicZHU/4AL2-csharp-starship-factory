using core.Assemblies;
using core.UI.constants;
using static System.ConsoleColor;

namespace core.UI;

public static class InstructionsTerminal
{
	public static void PrintUnknownStarship(String message)
	{
		Terminal.PrintMessageWithLinebreak(message);
	}

	public static void PrintAssemblingComponents(
		ComponentAssembly componentAssembly,
		String componentToAdd
	)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak(Instruction.Assemble, Green);
		Terminal.PrintMessageWithLinebreak(
			$" [{string.Join(", ", componentAssembly.Components)}] {componentToAdd}"
		);
	}

	public static void PrintGetOutStock(Int32 quantity, String componentModel)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak(Instruction.GetOutStock, Green);
		TerminalHelper.ColorizeMessageWithoutLinebreak($" {quantity}", Yellow);
		Terminal.PrintMessageWithLinebreak($" {componentModel}");
	}

	public static void PrintStarshipProductionStarting(String starshipModel, Int32 i)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak(Instruction.Producing, Green);
		Terminal.PrintMessageWithLinebreak($" {starshipModel} {i}");
	}

	public static void PrintStarshipProductionFinishing(String starshipModel, Int32 i)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak(Instruction.Finished, Green);
		Terminal.PrintMessageWithLinebreak($" {starshipModel} {i}");
	}
}
