using core.Products.Starship.Components.Engine;
using core.Products.Starship.Components.Hull;
using core.Products.Starship.Components.Thruster;
using core.Products.Starship.Components.Wing;

namespace core.Products.Starship;

public static class StarshipAssembly
{
	public static readonly Dictionary<String, List<String>> ComponentsMap =
		new()
		{
			{
				StarshipModel.Cargo,
				new List<String>
				{
					HullModel.Hull_HC1,
					EngineModel.Engine_EC1,
					WingModel.Wings_WC1,
					ThrusterModel.Thruster_TC1
				}
			},
			{
				StarshipModel.Explorer,
				new List<String>
				{
					HullModel.Hull_HE1,
					EngineModel.Engine_EE1,
					WingModel.Wings_WE1,
					ThrusterModel.Thruster_TE1
				}
			},
			{
				StarshipModel.Speeder,
				new List<String>
				{
					HullModel.Hull_HS1,
					EngineModel.Engine_ES1,
					WingModel.Wings_WS1,
					ThrusterModel.Thruster_TS1,
					ThrusterModel.Thruster_TS1
				}
			}
		};
}
