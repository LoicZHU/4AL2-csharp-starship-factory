namespace core.Components;

public class Engine
{
	public Guid Id { get; private set; }
	public String Name { get; private set; }

	private Engine(Guid id, String name)
	{
		this.Id = id;
		Name = name;
	}

	public static Engine Create(String name)
	{
		if (!isValidEngineComponent(name))
		{
			throw new ArgumentException("Invalid engine name");
		}

		return new Engine(Guid.NewGuid(), name);
	}

	private static Boolean isValidEngineComponent(String name)
	{
		return name.Equals(EngineComponent.Engine_EC1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(EngineComponent.Engine_EE1, StringComparison.OrdinalIgnoreCase)
			|| name.Equals(EngineComponent.Engine_ES1, StringComparison.OrdinalIgnoreCase);
	}
}
