using core.Items.Components;

namespace core.Components;

public sealed class Hull : IComponent
{
	public Guid Id { get; private set; }
	public String Name { get; private set; }

	private Hull(Guid id, String name)
	{
		this.Id = id;
		this.Name = name;
	}

	public static Hull Create(String name)
	{
		return new Hull(Guid.NewGuid(), name);
	}
}
