namespace core.Products.Starship;

public class Starship
{
	public Guid Id { get; } = Guid.NewGuid();
	public String Name { get; set; }
	public String Hull { get; set; }
	public String Engine { get; set; }
	public String Wing { get; set; }
	public List<String> Thrusters { get; set; }

	public Starship(
		String name,
		String hull,
		String engine,
		String wing,
		List<String> thrusters
	)
	{
		Name = name;
		Hull = hull;
		Engine = engine;
		Wing = wing;
		Thrusters = thrusters;
	}
}
