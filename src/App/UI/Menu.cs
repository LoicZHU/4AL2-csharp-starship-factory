using core.App.Handlers;
using core.App.UI;
using core.InputHandlers;
using core.UI.constants;
using core.Utils;

namespace core.UI;

public class Menu : IUserInterface
{
	private readonly Dictionary<String, IHandler> _commandHandlers;
	private readonly Dictionary<String, IHandlerWithArgs> _commandHandlersWithArgs;

	public Menu(
		Dictionary<String, IHandler> commandHandlers,
		Dictionary<String, IHandlerWithArgs> commandHandlersWithArgs
	)
	{
		this._commandHandlers = commandHandlers;
		this._commandHandlersWithArgs = commandHandlersWithArgs;
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

			var handler = this._commandHandlers.FirstOrDefault(handler =>
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

			var inputHandler = this._commandHandlersWithArgs.FirstOrDefault(inputHandler =>
				this.IsInputStartingWithCommand(input, inputHandler.Key)
			);
			if (!UtilsFunction.IsNull(inputHandler.Value))
			{
				this.Handle(inputHandler.Value, input);
				continue;
			}

			this.Handle(new UnknownInstructionHandlerWithArgs(), input);
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

	private void Handle(IHandlerWithArgs handlerWithArgs, String input)
	{
		handlerWithArgs.Handle(input);
	}
}
