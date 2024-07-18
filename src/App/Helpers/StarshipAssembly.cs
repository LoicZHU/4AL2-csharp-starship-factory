using core.Components;
using core.Starships;

namespace core.Assemblies;

public static class StarshipAssembly
{
	public static readonly Dictionary<String, Dictionary<String, Int32>> Components =
		new()
		{
			{
				StarshipName.Cargo,
				new Dictionary<String, Int32>
				{
					{ HullComponent.HullHc1, 1 },
					{ EngineComponent.EngineEc1, 1 },
					{ WingComponent.WingWc1, 2 },
					{ ThrusterComponent.ThrusterTc1, 1 }
				}
			},
			{
				StarshipName.Explorer,
				new Dictionary<String, Int32>
				{
					{ HullComponent.HullHe1, 1 },
					{ EngineComponent.EngineEe1, 1 },
					{ WingComponent.WingWe1, 2 },
					{ ThrusterComponent.ThrusterTe1, 1 }
				}
			},
			{
				StarshipName.Speeder,
				new Dictionary<String, Int32>
				{
					{ HullComponent.HullHs1, 1 },
					{ EngineComponent.EngineEs1, 1 },
					{ WingComponent.WingWs1, 2 },
					{ ThrusterComponent.ThrusterTs1, 2 },
				}
			}
		};
}
