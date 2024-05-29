using core.Components;

namespace core.Starships;

public sealed class Starship
{
	public Guid Id { get; set; }
	public String Name { get; set; }
	public Hull Hull { get; set; }
	public Engine Engine { get; set; }
	public Wing Wing { get; set; }
	public List<Thruster> Thrusters { get; set; }

	private Starship(
		Guid id,
		String name,
		Hull hull,
		Engine engine,
		Wing wing,
		List<Thruster> thrusters
	)
	{
		Id = id;
		Name = name;
		Hull = hull;
		Engine = engine;
		Wing = wing;
		Thrusters = thrusters;
	}

	public static Starship Create(
		String name,
		Hull hull,
		Engine engine,
		Wing wing,
		List<Thruster> thrusters
	)
	{
		return new Starship(Guid.NewGuid(), name, hull, engine, wing, thrusters);
	}
}
