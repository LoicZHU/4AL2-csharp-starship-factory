namespace core.UI;

public static class ListOrderDisplayHandler
{
	public static void PrintInvalidCommand(String message)
	{
		Terminal.PrintInvalidCommand(message);
	}

	public static void PrintStarshipCountsForEachInstruction(
		Dictionary<Guid, Dictionary<String, Int32>> orders
	)
	{
		foreach (var (guidKey, instructions) in orders)
		{
			Terminal.PrintMessageWithoutLinebreak($"Commande {guidKey} : ");

			for (var i = 0; i < instructions.Count; i++)
			{
				var starshipName = instructions.Keys.ElementAt(i);
				var count = instructions.Values.ElementAt(i);
				var message = $"{count} {starshipName}";

				Terminal.PrintMessageWithLinebreak(message);
			}
		}

		Terminal.PrintLinebreak();
	}
}
