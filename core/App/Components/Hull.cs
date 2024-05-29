namespace core.Components;

public class Hull
{
	public Guid Id { get; private set; }
	public String Name { get; private set; }

	private Hull(Guid id, String name)
	{
		this.Id = id;
		Name = name;
	}

	public static Hull Create(String name)
	{
		if (!isValidHullComponent(name))
		{
			throw new ArgumentException("Invalid hull name");
		}

		return new Hull(Guid.NewGuid(), name);
	}

	private static Boolean isValidHullComponent(String name)
	{
		return name.Equals(HullComponent.Hull_HC1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(HullComponent.Hull_HE1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(HullComponent.Hull_HS1, StringComparison.OrdinalIgnoreCase);
	}
}
