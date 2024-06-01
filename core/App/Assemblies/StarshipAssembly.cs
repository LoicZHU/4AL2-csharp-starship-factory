using core.Components;
using core.Starships;

namespace core.Assemblies;

public static class StarshipAssembly
{
	public static readonly Dictionary<String, List<String>> ComponentsMap =
		new()
		{
			{
				StarshipName.Cargo,
				new List<String>
				{
					HullComponent.Hull_HC1,
					EngineComponent.Engine_EC1,
					WingComponent.Wings_WC1,
					ThrusterComponent.Thruster_TC1
				}
			},
			{
				StarshipName.Explorer,
				new List<String>
				{
					HullComponent.Hull_HE1,
					EngineComponent.Engine_EE1,
					WingComponent.Wings_WE1,
					ThrusterComponent.Thruster_TE1
				}
			},
			{
				StarshipName.Speeder,
				new List<String>
				{
					HullComponent.Hull_HS1,
					EngineComponent.Engine_ES1,
					WingComponent.Wings_WS1,
					ThrusterComponent.Thruster_TS1,
					ThrusterComponent.Thruster_TS1
				}
			}
		};
}
