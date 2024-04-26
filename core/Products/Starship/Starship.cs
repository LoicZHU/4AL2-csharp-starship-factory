namespace core.Products.Starship;

public class Starship
{
	public String Name { get; private set; }
	public String Hull { get; private set; }
	public String Engine { get; private set; }
	public String Wing { get; private set; }
	public List<String> Thrusters { get; private set; }

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
