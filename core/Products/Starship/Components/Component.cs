namespace core.Products.Starship.Components;

public class Component
{
	public String Name { get; private set; }

	public Component(String name)
	{
		Name = name;
	}
}
