using core.Components;

namespace core.Starships;

public sealed class Starship
{
	public Guid Id { get; set; }
	public String Name { get; set; }
	public Hull Hull { get; set; }
	public List<Engine> Engines { get; set; }
	public (Wing, Wing) WingPair { get; set; }
	public List<Thruster> Thrusters { get; set; }

	private Starship(
		Guid id,
		String name,
		Hull hull,
		List<Engine> engines,
		(Wing, Wing) wingPair,
		List<Thruster> thrusters
	)
	{
		this.Id = id;
		this.Name = name;
		this.Hull = hull;
		this.Engines = engines;
		this.WingPair = wingPair;
		this.Thrusters = thrusters;
	}

	public static Starship Create(
		String name,
		Hull hull,
		List<Engine> engines,
		(Wing, Wing) wingPair,
		List<Thruster> thrusters
	)
	{
		return new Starship(Guid.NewGuid(), name, hull, engines, wingPair, thrusters);
	}
}
