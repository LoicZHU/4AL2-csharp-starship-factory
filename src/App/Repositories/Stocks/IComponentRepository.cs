namespace core.Repositories.ComponentRepository;

public interface IComponentRepository
{
	public Int32 GetCountByName(String componentName);
	public List<Dictionary<String, Int32>> GetStockOfEachComponent();
	public void Remove(String name);
}
