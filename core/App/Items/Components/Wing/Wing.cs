namespace core.Components;

public sealed class Wing
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
		return new Wing(Guid.NewGuid(), name);
	}
}
