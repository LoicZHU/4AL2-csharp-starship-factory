namespace core.Components;

public class Thruster
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
		if (!isValidThrusterComponent(name))
		{
			throw new ArgumentException("Invalid thruster name");
		}

		return new Thruster(Guid.NewGuid(), name);
	}

	private static Boolean isValidThrusterComponent(String name)
	{
		return name.Equals(ThrusterComponent.Thruster_TC1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(ThrusterComponent.Thruster_TE1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(ThrusterComponent.Thruster_TS1, StringComparison.OrdinalIgnoreCase);
	}
}
