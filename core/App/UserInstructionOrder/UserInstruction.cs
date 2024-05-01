namespace core.App.UserInstructionOrder;

public class UserInstruction
{
	public Guid Id { get; } = Guid.NewGuid();
	public Dictionary<String, Int32> Instructions { get; } = new();

	public void Add(String starshipModel, Int32 quantity)
	{
		if (!Instructions.ContainsKey(starshipModel))
		{
			Instructions[starshipModel] = quantity;
		}
		else
		{
			Instructions[starshipModel] += quantity;
		}
	}
}
