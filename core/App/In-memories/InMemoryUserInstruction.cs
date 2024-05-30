using core.Utils;

namespace core.In_memories;

public class InMemoryUserInstruction : Singleton<InMemoryUserInstruction>
{
	private readonly Dictionary<Guid, Dictionary<String, Int32>> _cache = new();

	public void Add(UserInstruction userInstruction)
	{
		_cache.Add(userInstruction.Id, userInstruction.Instructions);
	}

	public Dictionary<Guid, Dictionary<string, int>> GetUserInstructions()
	{
		return this._cache;
	}
}
