namespace core.App.UserInstructionOrder;

public class InMemoryUserInstruction
{
	private readonly Dictionary<Guid, Dictionary<String, Int32>> _cache = new();

	public void Add(UserInstruction userInstruction)
	{
		_cache.Add(userInstruction.Id, userInstruction.Instructions);
	}
}
