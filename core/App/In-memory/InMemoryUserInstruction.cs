using core.App.UserInstructionOrder;

namespace core.In_memory;

public class InMemoryUserInstruction
{
	private readonly Dictionary<Guid, Dictionary<String, Int32>> _cache = new();

	public void Add(UserInstruction userInstruction)
	{
		_cache.Add(userInstruction.Id, userInstruction.Instructions);
	}

	public void PrintAll()
	{
		foreach (var instructions in _cache)
		{
			foreach (var instruction in instructions.Value)
			{
				Console.WriteLine($"{instruction.Key} {instruction.Value}");
			}
		}
	}
}
