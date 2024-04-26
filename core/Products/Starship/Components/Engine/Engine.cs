namespace core.Products.Starship.Components.Engine;

public class Engine
{
	public Guid Id { get; private set; } = Guid.NewGuid();
	public String Name { get; private set; }

	public Engine(String name)
	{
		Name = name;
	}
}
