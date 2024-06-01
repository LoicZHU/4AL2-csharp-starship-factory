namespace core.Assemblies;

public class ComponentAssembly
{
	public Guid Id { get; set; }
	public String Name { get; set; }
	public List<String> Components { get; set; }

	private ComponentAssembly(Guid id, String name, List<String> components)
	{
		this.Id = id;
		this.Name = name;
		this.Components = components;
	}

	public static ComponentAssembly Create(String name, List<String> components)
	{
		return new ComponentAssembly(Guid.NewGuid(), name, components);
	}
}
