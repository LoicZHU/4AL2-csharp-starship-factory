using core.Assemblies;
using core.UI.constants;
using static System.ConsoleColor;

namespace core.UI;

public static class InstructionsDisplay
{
	public static void PrintAssemblingComponents(
		ComponentAssembly componentAssembly,
		String componentToAdd
	)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak(Instruction.Assemble, Green);
		Console.WriteLine(
			$" [{string.Join(", ", componentAssembly.Components)}] {componentToAdd}"
		);
	}

	public static void PrintGetOutStock(Int32 quantity, String componentModel)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak(Instruction.GetOutStock, Green);
		TerminalHelper.ColorizeMessageWithoutLinebreak($" {quantity}", Yellow);
		Console.WriteLine($" {componentModel}");
	}

	public static void PrintStarshipProductionStarting(String starshipModel, Int32 i)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak(Instruction.Producing, Green);
		Console.WriteLine($" {starshipModel} {i}");
	}

	public static void PrintStarshipProductionFinishing(string starshipModel, Int32 i)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak(Instruction.Finished, Green);
		Console.Write($" {starshipModel} {i}\n");
	}
}
