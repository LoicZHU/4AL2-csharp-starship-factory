using core.App.Helpers;
using core.Repositories.StarshipRepository;
using core.Starships;
using core.UI;
using core.Utils;

namespace core.In_memories.Items;

public class InMemoryStarship : IStarshipRepository
{
	private readonly Dictionary<Guid, Starship> _cache;

	public InMemoryStarship()
	{
		_cache = new();

		this.SetCache();
	}

	private void SetCache()
	{
		try
		{
			this.Add(CreateCargo());
			this.Add(BuildExplorer());
			this.Add(BuildSpeeder());
		}
		catch (Exception e)
		{
			Terminal.PrintMessageWithLinebreak(e.Message);
		}
	}

	private Starship BuildExplorer()
	{
		return StarshipFactory.Create(StarshipName.Explorer);
	}

	private Starship BuildSpeeder()
	{
		return StarshipFactory.Create(StarshipName.Speeder);
	}

	private Starship CreateCargo()
	{
		return StarshipFactory.Create(StarshipName.Cargo);
	}

	public void Add(Starship starship)
	{
		if (_cache.ContainsKey(starship.Id))
		{
			throw new ArgumentException($"Vaisseau ID {starship.Id} déjà existant");
		}

		_cache.Add(starship.Id, starship);
	}

	public List<Starship> GetAll()
	{
		return _cache.Values.ToList();
	}

	public Boolean Exists(String name)
	{
		return _cache.Values.Any(starship =>
			starship.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
		);
	}

	public void Remove(String name)
	{
		var starship = _cache.Values.FirstOrDefault(starship =>
			starship.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
		);

		if (!UtilsFunction.IsNull(starship))
		{
			_cache.Remove(starship.Id);
		}
	}

	public Dictionary<String, Int32> GetStock()
	{
		var stock = new Dictionary<String, Int32>();

		foreach (var starship in _cache.Values)
		{
			stock[starship.Name] = !stock.ContainsKey(starship.Name)
				? 1
				: stock[starship.Name] + 1;
		}

		return stock;
	}
}
