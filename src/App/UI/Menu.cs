using core.App.Handlers;
using core.App.UI;
using core.InputHandlers;
using core.UI.constants;
using core.Utils;

namespace core.UI;

public class Menu : IUserInterface
{
	private readonly Dictionary<String, IHandler> _handlers;
	private readonly Dictionary<String, IInputHandler> _inputHandlers;

	public Menu(
		Dictionary<String, IHandler> handlers,
		Dictionary<String, IInputHandler> inputHandlers
	)
	{
		this._handlers = handlers;
		this._inputHandlers = inputHandlers;
	}

	public void Start()
	{
		this.PrintStartingMessages();
		this.HandleTerminalUserInteractions();
	}

	private void PrintStartingMessages()
	{
		MenuDisplayHandler.PrintWelcome();
		MenuDisplayHandler.PrintUserInteractionInvitation();
	}

	private void HandleTerminalUserInteractions()
	{
		while (true)
		{
			var input = GetUserInput()?.Trim().ToUpper();
			if (UtilsFunction.IsNullOrWhiteSpace(input))
			{
				this.PrintEmptyInstructionMessage("🚫 Instruction vide. ('HELP' pour de l'aide)");
				continue;
			}

			var handler = this._handlers.FirstOrDefault(handler =>
				this.IsInputStartingWithCommand(input, handler.Key)
			);
			if (!UtilsFunction.IsNull(handler.Value))
			{
				this.Handle(handler.Value);

				if (IsInputEqualsToCommand(input, Command.Exit))
				{
					return;
				}
				continue;
			}

			var inputHandler = this._inputHandlers.FirstOrDefault(inputHandler =>
				this.IsInputStartingWithCommand(input, inputHandler.Key)
			);
			if (!UtilsFunction.IsNull(inputHandler.Value))
			{
				this.Handle(inputHandler.Value, input);
				continue;
			}

			this.Handle(new UnknownInstructionHandler(), input);
		}
	}

	private String? GetUserInput()
	{
		return Console.ReadLine();
	}

	private Boolean IsInputEqualsToCommand(String input, String command)
	{
		return String.Equals(input, command, StringComparison.OrdinalIgnoreCase);
	}

	private void PrintEmptyInstructionMessage(String message)
	{
		MenuDisplayHandler.PrintEmptyInstructionMessage(message);
	}

	private Boolean IsInputStartingWithCommand(String input, String command)
	{
		return input.StartsWith(command, StringComparison.OrdinalIgnoreCase);
	}

	private void Handle(IHandler handler)
	{
		handler.Handle();
	}

	private void Handle(IInputHandler inputHandler, String input)
	{
		inputHandler.Handle(input);
	}
}
