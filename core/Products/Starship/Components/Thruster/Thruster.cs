namespace core.Products.Starship.Components.Thruster;

public class Thruster
{
	public Guid Id { get; private set; } = Guid.NewGuid();
	public String Name { get; private set; }

	public Thruster(String name)
	{
		Name = name;
	}
}
