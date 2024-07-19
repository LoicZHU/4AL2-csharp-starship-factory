using core.Starships;

namespace core.Repositories.StarshipRepository;

public interface IStarshipRepository
{
	public void Add(Starship starship);
	public Boolean Exists(String name);
	public Dictionary<String, Int32> GetStock();
	public void Remove(String name);
}
