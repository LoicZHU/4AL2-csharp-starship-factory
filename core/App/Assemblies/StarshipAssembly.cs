using core.Components;
using core.Starships;

namespace core.Assemblies;

public static class StarshipAssembly
{
	public static readonly Dictionary<String, Dictionary<String, Int32>> ComponentsMap =
		new()
		{
			{
				StarshipName.Cargo,
				new Dictionary<String, Int32>
				{
					{ HullComponent.Hull_HC1, 1 },
					{ EngineComponent.Engine_EC1, 1 },
					{ WingComponent.Wings_WC1, 1 },
					{ ThrusterComponent.Thruster_TC1, 1 }
				}
			},
			{
				StarshipName.Explorer,
				new Dictionary<String, Int32>
				{
					{ HullComponent.Hull_HE1, 1 },
					{ EngineComponent.Engine_EE1, 1 },
					{ WingComponent.Wings_WE1, 1 },
					{ ThrusterComponent.Thruster_TE1, 1 }
				}
			},
			{
				StarshipName.Speeder,
				new Dictionary<String, Int32>
				{
					{ HullComponent.Hull_HS1, 1 },
					{ EngineComponent.Engine_ES1, 1 },
					{ WingComponent.Wings_WS1, 1 },
					{ ThrusterComponent.Thruster_TS1, 2 },
				}
			}
		};
}
