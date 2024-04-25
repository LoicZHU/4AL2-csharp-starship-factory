using core.Products.Starship.Components;

namespace core.Products.Starship;

public class Starship
{
	public String Name { get; private set; }
	public Component Hull { get; private set; }
	public Component Engine { get; private set; }
	public Component Wing { get; private set; }
	public List<Component> Thrusters { get; private set; }

	public Starship(
		String name,
		Component hull,
		Component engine,
		Component wing,
		List<Component> thrusters
	)
	{
		Name = name;
		Hull = hull;
		Engine = engine;
		Wing = wing;
		Thrusters = thrusters;
	}
}
