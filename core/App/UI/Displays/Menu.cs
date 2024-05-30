using System.Text.RegularExpressions;
using core.In_memories;
using core.In_memories.Items;
using core.Starships;
using core.UI.constants;
using core.Utils;

namespace core.UI;

public class Menu : Singleton<Menu>
{
	public void Start()
	{
		this.PrintWelcomeMessage();
		this.PrintUserInteractionInvitation();

		this.HandleUserInstruction();
	}

	public void PrintWelcomeMessage()
	{
		MainTerminal.PrintWelcomeMessage("Bienvenue chez Capsule Corp ! 🚀");
	}

	public void PrintUserInteractionInvitation()
	{
		MainTerminal.PrintInvitationToUserInteraction(
			$"🕹 Entrez une instruction ({Command.Help} pour de l'aide) :"
		);
	}

	public void HandleUserInstruction()
	{
		while (true)
		{
			var input = Console.ReadLine()?.ToUpper();
			if (String.IsNullOrWhiteSpace(input))
			{
				MainTerminal.PrintUnknownInstruction(
					"🚫 Instruction vide, veuillez réessayer. (Tapez HELP pour de l'aide)"
				);
				continue;
			}

			switch (input)
			{
				case Command.Exit:
					MainTerminal.PrintGoodbyeMessage("👋 Merci d'avoir utilisé Capsule Corp !");
					return;
				case Command.Help:
					this.PrintHelp();
					break;
				case Command.Instructions:
					// AssembleShipsMenu ?
					break;
				case Command.Produce:
					// ProduceShipsMenu ?
					break;
				case Command.Stocks:
					StockDisplay.Instance.PrintStarshipStock();
					StockDisplay.Instance.PrintComponentStock();
					break;
				case Command.UserInstructions:
					// DisplayOrdersMenu ?
					var userInstructions = InMemoryUserInstruction.Instance.GetUserInstructions();
					foreach (var (guidKey, instructions) in userInstructions)
					{
						MainTerminal.PrintMessage($"Commande n°{guidKey}");
						foreach (var (starship, count) in instructions)
						{
							MainTerminal.PrintMessage($"{starship} : {count}");
						}

						TerminalHelper.PrintLineBreak();
					}
					break;
				case Command.Verify:
					// VerifyStocksMenu ?
					break;
				default:
					if (
						input.StartsWith(Command.UserInstruction, StringComparison.OrdinalIgnoreCase)
					)
					{
						// RegisterOrderMenu ?
						this.HandleUserInstructionCommand(input);
						break;
					}

					MainTerminal.PrintUnknownInstruction(
						$"🚫 Instruction inconnue : {input} ({Command.Help} pour de l'aide) :"
					);
					break;
			}
		}
	}

	private void PrintHelp()
	{
		MainTerminal.PrintHelp();
	}

	private void HandleUserInstructionCommand(String input)
	{
		Console.WriteLine("input: " + input);
		if (!this.IsUserInputValid(input.Split()))
		{
			this.PrintInvalidUserInstructionCommandMessage();
			return;
		}

		var splittedBySpaceInput = input.Split(new[] { ' ' }, 2);
		if (!IsCommandNameSeparatedByOneSpace(splittedBySpaceInput))
		{
			this.PrintInvalidUserInstructionCommandMessage();
			return;
		}

		var userInstructionBody = splittedBySpaceInput[1];
		var userInstruction = GetCompleteUserInstructionFrom(userInstructionBody);

		InMemoryUserInstruction.Instance.Add(userInstruction);
		// this._userInterface.PrintLineBreak();
	}

	private Boolean IsUserInputValid(String[] input)
	{
		return input.Length >= 3 && (input.Length - 1) % 2 == 0;
	}

	private void PrintInvalidUserInstructionCommandMessage()
	{
		MainTerminal.PrintMessage(
			"❌ La commande doit respecter ce format : [USER_INSTRUCTION] <quantité> <nom_du_vaisseau> [, <quantité> <nom_du_vaisseau>, ...]"
		);
	}

	private Boolean IsCommandNameSeparatedByOneSpace(String[] parts)
	{
		return parts.Length == 2;
	}

	private static readonly string QuantityWithStarshipPattern = @"(\d+)\s+(\w+)";

	private UserInstruction GetCompleteUserInstructionFrom(String starshipsPart)
	{
		var userInstruction = UserInstruction.Create(new Dictionary<String, int>());

		foreach (var quantityAndStarship in starshipsPart.Split(", "))
		{
			var match = Regex.Match(quantityAndStarship.Trim(), QuantityWithStarshipPattern);
			if (!IsMatch(match))
			{
				this.PrintInvalidUserInstructionCommandMessage();
				continue;
			}

			if (!int.TryParse(match.Groups[1].Value, out var quantity))
			{
				this.PrintInvalidUserInstructionCommandMessage();
				continue;
			}

			var starshipModelInput = match.Groups[2].Value;
			var starshipName = this.GetStarshipName(starshipModelInput);
			if (IsUnknownStarship(starshipName))
			{
				MainTerminal.PrintMessage("❌ Modèle de vaisseau inconnu...");
				continue;
			}

			userInstruction.Add(starshipName, quantity);
		}

		return userInstruction;
	}

	private Boolean IsMatch(Match match)
	{
		return match.Success;
	}

	private String GetStarshipName(String name)
	{
		if (this.IsCargoStarship(name))
		{
			return StarshipName.Cargo;
		}
		if (this.IsExplorerStarship(name))
		{
			return StarshipName.Explorer;
		}
		if (this.IsSpeederStarship(name))
		{
			return StarshipName.Speeder;
		}

		return StarshipName.Unknown;
	}

	private Boolean IsCargoStarship(String name)
	{
		return name.Equals(StarshipName.Cargo, StringComparison.OrdinalIgnoreCase);
	}

	private Boolean IsExplorerStarship(String name)
	{
		return name.Equals(StarshipName.Explorer, StringComparison.OrdinalIgnoreCase);
	}

	private Boolean IsSpeederStarship(String name)
	{
		return name.Equals(StarshipName.Speeder, StringComparison.OrdinalIgnoreCase);
	}

	private Boolean IsUnknownStarship(String name)
	{
		return name.Equals(StarshipName.Unknown, StringComparison.OrdinalIgnoreCase);
	}
}
