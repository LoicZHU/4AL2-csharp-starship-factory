namespace core.Repositories.ComponentRepository;

public interface IComponentRepository
{
	public Int32 GetCount(String componentName);
	public List<Dictionary<String, Int32>> GetStockOfEachComponent();
	public void Remove(String name);
}
