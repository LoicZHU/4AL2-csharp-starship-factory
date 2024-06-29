using core.In_memories;

namespace core.UI;

public static class UserInstructionsDisplayHandler
{
	public static void PrintInvalidCommand(String message)
	{
		Terminal.PrintInvalidCommand(message);
	}

	public static void PrintStarshipCountsForEachInstruction()
	{
		var userInstructions = InMemoryUserInstruction.Instance.GetUserInstructions();

		foreach (var (guidKey, instructions) in userInstructions)
		{
			Terminal.PrintMessageWithLinebreak($"Commande : {guidKey}");
			foreach (var (starship, count) in instructions)
			{
				Terminal.PrintMessageWithLinebreak($"{starship} : {count}");
			}

			Terminal.PrintLinebreak();
		}
	}
}
