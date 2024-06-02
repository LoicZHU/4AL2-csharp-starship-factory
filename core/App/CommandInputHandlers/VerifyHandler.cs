using System.Text.RegularExpressions;
using core.Components;
using core.In_memories.Items;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public static class VerifyHandler
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
		}

		PrintSufficientStock();
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

	private static void PrintSufficientStock()
	{
		VerifyTerminal.PrintAvailableMessage();
		TerminalHelper.PrintLineBreak();
	}

	private static void PrintInsufficientStock()
	{
		VerifyTerminal.PrintUnavailableMessage();
		TerminalHelper.PrintLineBreak();
	}

	private static void PrintInvalidCommand(String message)
	{
		VerifyTerminal.PrintError(message);
		TerminalHelper.PrintLineBreak();
	}
}
