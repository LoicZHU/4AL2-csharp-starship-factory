using core.In_memories.Items;

namespace core.Repositories.StarshipRepository;

public class StarshipRepository : IStarshipRepository
{
	private readonly InMemoryStarship _inMemoryStarship;

	public StarshipRepository(InMemoryStarship inMemoryStarship)
	{
		this._inMemoryStarship = inMemoryStarship;
	}

	public Boolean Exists(String name)
	{
		return this._inMemoryStarship.Exists(name);
	}

	public Dictionary<String, Int32> GetStock()
	{
		return this._inMemoryStarship.GetStock();
	}

	public void Remove(string name)
	{
		this._inMemoryStarship.Remove(name);
	}
}
