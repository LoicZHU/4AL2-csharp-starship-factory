namespace core.Components;

public class Wing
{
	public Guid Id { get; private set; }
	public String Name { get; private set; }

	private Wing(Guid id, String name)
	{
		this.Id = id;
		Name = name;
	}

	public static Wing Create(String name)
	{
		if (!isValidWingComponent(name))
		{
			throw new ArgumentException("Invalid wing name");
		}

		return new Wing(Guid.NewGuid(), name);
	}

	private static Boolean isValidWingComponent(String name)
	{
		return name.Equals(WingComponent.Wings_WC1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(WingComponent.Wings_WE1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(WingComponent.Wings_WS1, StringComparison.OrdinalIgnoreCase);
	}
}
