namespace core.Products.Starship.Components.Wing;

public class Wing
{
	public Guid Id { get; private set; } = Guid.NewGuid();
	public String Name { get; private set; }

	public Wing(String name)
	{
		Name = name;
	}
}
