using core.In_memories.Items;

namespace core.Repositories.ComponentRepository;

public class ComponentRepository : IComponentRepository
{
	private readonly InMemoryComponent _inMemoryComponent;

	public ComponentRepository(InMemoryComponent inMemoryComponent)
	{
		this._inMemoryComponent = inMemoryComponent;
	}

	public Int32 GetCount(String componentName)
	{
		return this._inMemoryComponent.CountByName(componentName);
	}

	public List<Dictionary<String, Int32>> GetStockOfEachComponent()
	{
		return this._inMemoryComponent.GetStockOfEachComponent();
	}

	public void Remove(String name)
	{
		this._inMemoryComponent.Remove(name);
	}
}
