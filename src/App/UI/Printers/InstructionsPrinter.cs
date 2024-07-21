using core.UI.constants;
using static System.ConsoleColor;

namespace core.UI;

public static class InstructionsPrinter
{
	public static void PrintUnknownStarship(String message)
	{
		Printer.PrintMessageWithLinebreak(message);
	}

	public static void PrintAssemblingComponents(
		List<String> components,
		String componentToAdd
	)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak(Instruction.Assemble, Green);
		Printer.PrintMessageWithLinebreak(
			$" [{String.Join(", ", components)}] {componentToAdd}"
		);
	}

	public static void PrintGetOutStock(Int32 quantity, String componentModel)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak(Instruction.GetOutStock, Green);
		TerminalHelper.ColorizeMessageWithoutLinebreak($" {quantity}", Yellow);
		Printer.PrintMessageWithLinebreak($" {componentModel}");
	}

	public static void PrintStarshipProductionStarting(String starshipModel, Int32 i)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak(Instruction.Producing, Green);
		Printer.PrintMessageWithLinebreak($" {starshipModel} {i}");
	}

	public static void PrintStarshipProductionFinishing(String starshipModel, Int32 i)
	{
		TerminalHelper.ColorizeMessageWithoutLinebreak(Instruction.Finished, Green);
		Printer.PrintMessageWithLinebreak($" {starshipModel} {i}");
	}
}
