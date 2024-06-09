using System.Text.RegularExpressions;
using core.Assemblies;
using core.Components;
using core.In_memories;
using core.In_memories.Items;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public class InstructionsHandler : IInputHandler
{
	private const String QuantityWithStarshipPattern = @"(\d+)\s+(\w+)";
	private const String InvalidCommandMessage =
		"❌ La commande doit respecter ce format : INSTRUCTIONS <quantité> <nom_de_l'élément> [<quantité> <nom_de_l'élément> ...]";

	public void HandleInput(String input)
	{
		if (!HandlerHelper.IsCommandInputValid(input.Split()))
		{
			this.PrintInvalidCommand(InvalidCommandMessage);
			return;
		}

		var splitBySpaceInput = input.Split(new[] { ' ' }, 2);
		if (!HandlerHelper.IsCommandNameSeparatedByOneSpace(splitBySpaceInput))
		{
			this.PrintInvalidCommand(InvalidCommandMessage);
			return;
		}

		var instructionsBody = splitBySpaceInput[1];
		foreach (var quantityAndStarship in instructionsBody.Split(", "))
		{
			var match = Regex.Match(quantityAndStarship.Trim(), QuantityWithStarshipPattern);
			if (!HandlerHelper.IsMatch(match))
			{
				this.PrintInvalidCommand(InvalidCommandMessage);
				break;
			}

			if (!int.TryParse(match.Groups[1].Value, out var quantity))
			{
				this.PrintInvalidCommand(InvalidCommandMessage);
				break;
			}

			var starshipNameInput = match.Groups[2].Value;
			var starshipName = HandlerHelper.GetStarshipName(starshipNameInput);
			if (HandlerHelper.IsUnknownStarship(starshipName))
			{
				this.PrintUnknownStarship($"❌ Vaisseau '{starshipNameInput}' inconnu.");
				break;
			}

			var (hullCount, engineCount, wingCount, thrusterCount) =
				this.GetStarshipComponentsCountFromInventories();

			if (
				this.IsMoreInventoryRequired(
					starshipName,
					quantity,
					hullCount,
					engineCount,
					wingCount,
					thrusterCount
				)
			)
			{
				this.PrintInsufficientStock();
				return;
			}

			try
			{
				this.HandleStarshipAssembly(starshipName, quantity);
			}
			catch (Exception e)
			{
				Terminal.PrintMessageWithLinebreak(e.Message);
			}
		}
	}

	private void PrintUnknownStarship(String message)
	{
		InstructionsDisplayHandler.PrintUnknownStarship(message);
	}

	private void PrintInvalidCommand(String message)
	{
		InstructionsDisplayHandler.PrintInvalidCommand(message);
	}

	private (Int32, Int32, Int32, Int32) GetStarshipComponentsCountFromInventories()
	{
		var inMemoryComponent = InMemoryComponent.Instance;

		try
		{
			return (
				inMemoryComponent.CountByName(EngineComponent.EngineEc1),
				inMemoryComponent.CountByName(HullComponent.HullHc1),
				inMemoryComponent.CountByName(ThrusterComponent.ThrusterTc1),
				inMemoryComponent.CountByName(WingComponent.WingsWc1)
			);
		}
		catch (Exception e)
		{
			Terminal.PrintMessageWithLinebreak(e.Message);
			return (0, 0, 0, 0);
		}
	}

	private Boolean IsMoreInventoryRequired(
		String starshipName,
		Int32 quantity,
		Int32 hullCount,
		Int32 engineCount,
		Int32 wingCount,
		Int32 thrusterCount
	)
	{
		if (HandlerHelper.IsCargoStarship(starshipName))
		{
			return this.IsMoreInventoryRequiredForCargoStarship(
				quantity,
				hullCount,
				engineCount,
				wingCount,
				thrusterCount
			);
		}

		if (HandlerHelper.IsExplorerOrSpeederStarship(starshipName))
		{
			return this.IsMoreInventoryRequiredForExplorerOrSpeederStarship(
				quantity,
				hullCount,
				engineCount,
				wingCount,
				thrusterCount
			);
		}

		return false;
	}

	private Boolean IsMoreInventoryRequiredForCargoStarship(
		Int32 quantity,
		Int32 hullCount,
		Int32 engineCount,
		Int32 wingCount,
		Int32 thrusterCount
	)
	{
		return hullCount < 1 * quantity
			&& engineCount < 1 * quantity
			&& wingCount < 1 * quantity
			&& thrusterCount < 1 * quantity;
	}

	private Boolean IsMoreInventoryRequiredForExplorerOrSpeederStarship(
		Int32 quantity,
		Int32 hullCount,
		Int32 engineCount,
		Int32 wingCount,
		Int32 thrusterCount
	)
	{
		return hullCount < 1 * quantity
			&& engineCount < 1 * quantity
			&& wingCount < 1 * quantity
			&& thrusterCount < 2 * quantity;
	}

	private void PrintInsufficientStock()
	{
		Terminal.PrintMessageWithLinebreak("Stock insuffisant.");
	}

	private void HandleStarshipAssembly(String starshipName, Int32 quantity)
	{
		var getComponentsOutFromStock = this.GetComponentsOutFromStockDelegate(starshipName);
		if (UtilsFunction.IsNull(getComponentsOutFromStock))
		{
			throw new InvalidOperationException(
				"Callback to get out components from inventories is null."
			);
		}

		for (var i = 1; i <= quantity; i++)
		{
			InstructionsDisplayHandler.PrintStarshipProductionStarting(starshipName, i);
			getComponentsOutFromStock();

			var componentAssembly = ComponentAssembly.Create(String.Empty, new List<String>());
			InMemoryComponentAssembly.Instance.Add(componentAssembly);

			foreach (var (componentName, count) in StarshipAssembly.Components[starshipName])
			{
				for (var j = 1; j <= count; j++)
				{
					this.AddComponentAssemblyToItsInventory(componentAssembly, componentName);
				}
			}

			InstructionsDisplayHandler.PrintStarshipProductionFinishing(starshipName, i);
		}

		Terminal.PrintLinebreak();
	}

	private Action? GetComponentsOutFromStockDelegate(String starshipName)
	{
		if (HandlerHelper.IsCargoStarship(starshipName))
		{
			return this.GetCargoComponentsOutFromStock;
		}

		if (HandlerHelper.IsExplorerStarship(starshipName))
		{
			return this.GetExplorerComponentsOutFromStock;
		}

		if (HandlerHelper.IsSpeederStarship(starshipName))
		{
			return this.GetSpeederComponentsOutFromStock;
		}

		return null;
	}

	private void AddComponentAssemblyToItsInventory(
		ComponentAssembly componentAssembly,
		String componentName
	)
	{
		InstructionsDisplayHandler.PrintAssemblingComponents(
			componentAssembly,
			componentName
		);
		InMemoryComponentAssembly.Instance.AddComponent(componentAssembly.Id, componentName);
	}

	private void GetCargoComponentsOutFromStock()
	{
		this.GetComponentsOutFromStock(HullComponent.HullHc1, 1);
		this.GetComponentsOutFromStock(EngineComponent.EngineEc1, 1);
		this.GetComponentsOutFromStock(WingComponent.WingsWc1, 1);
		this.GetComponentsOutFromStock(ThrusterComponent.ThrusterTc1, 1);
	}

	private void GetExplorerComponentsOutFromStock()
	{
		this.GetComponentsOutFromStock(HullComponent.HullHe1, 1);
		this.GetComponentsOutFromStock(EngineComponent.EngineEe1, 1);
		this.GetComponentsOutFromStock(WingComponent.WingsWe1, 1);
		this.GetComponentsOutFromStock(ThrusterComponent.ThrusterTe1, 1);
	}

	private void GetSpeederComponentsOutFromStock()
	{
		this.GetComponentsOutFromStock(HullComponent.HullHs1, 1);
		this.GetComponentsOutFromStock(EngineComponent.EngineEs1, 1);
		this.GetComponentsOutFromStock(WingComponent.WingsWs1, 1);
		this.GetComponentsOutFromStock(ThrusterComponent.ThrusterTs1, 2);
	}

	private void GetComponentsOutFromStock(String componentName, Int32 quantity)
	{
		InstructionsDisplayHandler.PrintGetOutStock(quantity, componentName);

		for (var i = 1; i <= quantity; i++)
		{
			InMemoryComponent.Instance.Remove(componentName);
		}
	}
}
