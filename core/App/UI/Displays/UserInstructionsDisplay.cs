using core.In_memories;

namespace core.UI;

public static class UserInstructionsDisplay
{
	public static void PrintStarshipCountsForEachInstruction()
	{
		var userInstructions = InMemoryUserInstruction.Instance.GetUserInstructions();

		foreach (var (guidKey, instructions) in userInstructions)
		{
			MainTerminal.PrintMessage($"Commande n°{guidKey}");
			foreach (var (starship, count) in instructions)
			{
				MainTerminal.PrintMessage($"{starship} : {count}");
			}

			TerminalHelper.PrintLineBreak();
		}
	}
}
