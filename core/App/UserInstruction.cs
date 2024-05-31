namespace core;

public sealed class UserInstruction
{
	public Guid Id { get; }
	public Dictionary<String, Int32> Instructions { get; }

	private UserInstruction(Guid id, Dictionary<String, Int32> instructions)
	{
		this.Id = id;
		this.Instructions = instructions;
	}

	public static UserInstruction Create(Dictionary<String, Int32> instructions)
	{
		return new UserInstruction(Guid.NewGuid(), instructions);
	}

	public void Add(String name, Int32 quantity)
	{
		Instructions[name] = !Instructions.ContainsKey(name)
			? quantity
			: Instructions[name] + quantity;
	}
}
