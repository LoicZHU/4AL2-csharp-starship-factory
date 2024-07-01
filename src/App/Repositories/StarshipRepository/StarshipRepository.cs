using core.In_memories.Items;

namespace core.Repositories.StarshipRepository;

public class StarshipRepository : IStarshipRepository
{
	private readonly InMemoryStarship _inMemoryStarship;

	public StarshipRepository(InMemoryStarship inMemoryStarship)
	{
		this._inMemoryStarship = inMemoryStarship;
	}

	public Dictionary<String, Int32> GetStock()
	{
		return this._inMemoryStarship.GetStock();
	}
}
