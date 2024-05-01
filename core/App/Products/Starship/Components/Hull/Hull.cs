namespace core.App.Products.Starship.Components.Hull;

public class Hull
{
	public Guid Id { get; private set; } = Guid.NewGuid();
	public String Name { get; private set; }

	public Hull(String name)
	{
		Name = name;
	}
}
