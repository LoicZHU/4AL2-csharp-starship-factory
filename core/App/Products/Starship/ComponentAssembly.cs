namespace core.App.Products.Starship;

public class ComponentAssembly
{
	public Guid Id { get; } = Guid.NewGuid();
	public String Name { get; set; }
	public List<String> Components { get; set; }

	public ComponentAssembly(String name, List<String> components)
	{
		Name = name;
		Components = components;
	}
}
