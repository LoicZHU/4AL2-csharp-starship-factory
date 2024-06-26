using core.Items.Components;

namespace core.Components;

public sealed class Engine : IComponent
{
	public Guid Id { get; private set; }
	public String Name { get; private set; }

	private Engine(Guid id, String name)
	{
		this.Id = id;
		this.Name = name;
	}

	public static Engine Create(String name)
	{
		return new Engine(Guid.NewGuid(), name);
	}
}