namespace core.Components;

public sealed class Thruster
{
	public Guid Id { get; private set; }
	public String Name { get; private set; }

	private Thruster(Guid id, String name)
	{
		this.Id = id;
		Name = name;
	}

	public static Thruster Create(String name)
	{
		return new Thruster(Guid.NewGuid(), name);
	}
}
