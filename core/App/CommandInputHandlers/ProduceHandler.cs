using System.Text.RegularExpressions;
using core.Assemblies;
using core.Components;
using core.In_memories;
using core.In_memories.Items;
using core.UI;
using core.UI.constants;
using core.Utils;
using static System.ConsoleColor;

namespace core.InputHandlers;

public static class ProduceHandler
{
	private const string QuantityWithStarshipPattern = @"(\d+)\s+(\w+)";
	private const String InvalidCommandMessage = "La commande est invalide.";

	public static void HandleInput(String input)
	{
		if (!HandlerHelper.IsCommandInputValid(input.Split()))
		{
			PrintInvalidCommand(InvalidCommandMessage);
			return;
		}

		var splittedBySpaceInput = input.Split(new[] { ' ' }, 2);
		if (!HandlerHelper.IsCommandNameSeparatedByOneSpace(splittedBySpaceInput))
		{
			PrintInvalidCommand(InvalidCommandMessage);
			return;
		}

		var instructionBody = splittedBySpaceInput[1];
		foreach (var quantityAndStarship in instructionBody.Split(", "))
		{
			var match = Regex.Match(quantityAndStarship.Trim(), QuantityWithStarshipPattern);
			if (!HandlerHelper.IsMatch(match))
			{
				PrintInvalidCommand(InvalidCommandMessage);
				break;
			}

			if (!int.TryParse(match.Groups[1].Value, out var quantity))
			{
				PrintInvalidCommand(InvalidCommandMessage);
				break;
			}

			var starshipNameInput = match.Groups[2].Value;
			var starshipName = HandlerHelper.GetStarshipName(starshipNameInput);
			if (HandlerHelper.IsUnknownStarship(starshipName))
			{
				PrintUnknownStarship($"❌ Vaisseau '{starshipNameInput}' inconnu.");
				break;
			}

			var (hullCount, engineCount, wingCount, thrusterCount) =
				GetStarshipComponentsCountFromInventories();

			if (
				IsMoreInventoryRequired(
					starshipName,
					quantity,
					hullCount,
					engineCount,
					wingCount,
					thrusterCount
				)
			)
			{
				PrintInsufficientStock();
				return;
			}

			try
			{
				HandleStarshipAssembly(starshipName, quantity);
				PrintStockUpdatedMessage();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}

	private static void PrintInvalidCommand(String message)
	{
		VerifyTerminal.PrintError(message);
		TerminalHelper.PrintLineBreak();
	}

	private static void PrintUnknownStarship(String message)
	{
		MainTerminal.PrintMessage(message);
	}

	private static (Int32, Int32, Int32, Int32) GetStarshipComponentsCountFromInventories()
	{
		var inMemoryComponent = InMemoryComponent.Instance;

		try
		{
			return (
				inMemoryComponent.CountByName(EngineComponent.Engine_EC1),
				inMemoryComponent.CountByName(HullComponent.Hull_HC1),
				inMemoryComponent.CountByName(ThrusterComponent.Thruster_TC1),
				inMemoryComponent.CountByName(WingComponent.Wings_WC1)
			);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return (0, 0, 0, 0);
		}
	}

	private static Boolean IsMoreInventoryRequired(
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
			return IsMoreInventoryRequiredForCargoStarship(
				quantity,
				hullCount,
				engineCount,
				wingCount,
				thrusterCount
			);
		}

		if (HandlerHelper.IsExplorerOrSpeederStarship(starshipName))
		{
			return IsMoreInventoryRequiredForExplorerOrSpeederStarship(
				quantity,
				hullCount,
				engineCount,
				wingCount,
				thrusterCount
			);
		}

		return false;
	}

	private static Boolean IsMoreInventoryRequiredForCargoStarship(
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

	private static Boolean IsMoreInventoryRequiredForExplorerOrSpeederStarship(
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

	private static void PrintInsufficientStock()
	{
		VerifyTerminal.PrintUnavailableMessage();
		TerminalHelper.PrintLineBreak();
	}

	private static void HandleStarshipAssembly(String starshipName, Int32 quantity)
	{
		var getOutComponentsFromStock = CallbackToGetOutComponentsFromStock(starshipName);
		if (UtilsFunction.IsNull(getOutComponentsFromStock))
		{
			throw new InvalidOperationException(
				"Callback to get out components from inventories is null."
			);
		}

		for (var i = 1; i <= quantity; i++)
		{
			InstructionsDisplay.PrintStarshipProductionStarting(starshipName, i);
			getOutComponentsFromStock();

			var componentAssembly = ComponentAssembly.Create(String.Empty, new List<String>());
			InMemoryComponentAssembly.Instance.Add(componentAssembly);

			foreach (var componentName in StarshipAssembly.ComponentsMap[starshipName])
			{
				AddComponentAssemblyToItsInventory(componentAssembly, componentName);
			}

			InstructionsDisplay.PrintStarshipProductionFinishing(starshipName, i);
		}

		TerminalHelper.PrintLineBreak();
	}

	private static Action? CallbackToGetOutComponentsFromStock(String starshipName)
	{
		if (HandlerHelper.IsCargoStarship(starshipName))
		{
			return GetOutCargoComponentsFromStock;
		}
		else if (HandlerHelper.IsExplorerStarship(starshipName))
		{
			return GetOutExplorerComponentsFromStock;
		}
		else if (HandlerHelper.IsSpeederStarship(starshipName))
		{
			return GetOutSpeederComponentsFromStock;
		}

		return null;
	}

	private static void AddComponentAssemblyToItsInventory(
		ComponentAssembly componentAssembly,
		String componentName
	)
	{
		InstructionsDisplay.PrintAssemblingComponents(componentAssembly, componentName);
		InMemoryComponentAssembly.Instance.AddComponent(componentAssembly.Id, componentName);
	}

	private static void GetOutCargoComponentsFromStock()
	{
		GetOutComponentsFromStock(HullComponent.Hull_HC1, 1);
		GetOutComponentsFromStock(EngineComponent.Engine_EC1, 1);
		GetOutComponentsFromStock(WingComponent.Wings_WC1, 1);
		GetOutComponentsFromStock(ThrusterComponent.Thruster_TC1, 1);
	}

	private static void GetOutExplorerComponentsFromStock()
	{
		GetOutComponentsFromStock(HullComponent.Hull_HE1, 1);
		GetOutComponentsFromStock(EngineComponent.Engine_EE1, 1);
		GetOutComponentsFromStock(WingComponent.Wings_WE1, 1);
		GetOutComponentsFromStock(ThrusterComponent.Thruster_TE1, 1);
	}

	private static void GetOutSpeederComponentsFromStock()
	{
		GetOutComponentsFromStock(HullComponent.Hull_HS1, 1);
		GetOutComponentsFromStock(EngineComponent.Engine_ES1, 1);
		GetOutComponentsFromStock(WingComponent.Wings_WS1, 1);
		GetOutComponentsFromStock(ThrusterComponent.Thruster_TS1, 2);
	}

	private static void GetOutComponentsFromStock(String componentName, Int32 quantity)
	{
		InstructionsDisplay.PrintGetOutStock(quantity, componentName);

		for (var i = 1; i <= quantity; i++)
		{
			InMemoryComponent.Instance.Remove(componentName);
		}
	}

	private static void PrintStockUpdatedMessage()
	{
		TerminalHelper.ColorizeMessageWithLinebreak(Production.StockUpdated, Yellow);
		TerminalHelper.PrintLineBreak();
	}
}
