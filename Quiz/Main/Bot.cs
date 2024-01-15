using Quiz.Configs;
using Quiz.MessageSendingHandler;
using Quiz.State;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Quiz;

public class Bot
{
    public static TelegramBotClient BotClient { get; private set; }
    private IState[] _states;
    private GameStatistics _statistics;
    private IState _currentState;
    private bool _isInit = false;
    public Bot()
    {
        _statistics = new GameStatistics();
        _states = new IState[]
        {
            new GameState(_statistics, IsGameEnd),
            new InfoState(_statistics)
        };
        _currentState = _states[1];
    }

    private void IsGameEnd()
    {
       
    }

    public void Run()
    {
        //иницыализация бота с провверкой есть ли токен вообще
        if (string.IsNullOrEmpty(BotConfig.BOT_TOKEN)) throw new ArgumentException("Bot token is null or empty");
        
        BotClient = new TelegramBotClient(BotConfig.BOT_TOKEN);

        using CancellationTokenSource cts = new();

        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        BotClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token
        );
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken token)
    {
        //отправка ошибки в консоль
        Logger.Log(exception.Message, MessageType.Error);
        return Task.CompletedTask;
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        var data = GetMesage(update);
        string command = data.command.ToLower();
        Message message = data.message;
        
        if(message == null) return;

        if (await IsUpdateValid(command, message)) return;
        if (HandlingCommandToChangeState(command)) return ;
        
       await _currentState.Update(command , message.Chat.Id.ToString(), message);
        return;  
    }

    private bool HandlingCommandToChangeState(string command)
    {
        switch (command.ToLower())
        {
            case "start game":
                _currentState = _states[0];
                _currentState.Start();
                return true;
            case "back":
                _currentState = _states[1];
                _currentState.Start();
                break;
            case "statistics":
                _currentState = _states[1];
                _currentState.Start();

                break;
        }

        return false;
    }

    private async Task<bool> IsUpdateValid(string command, Message message)
    {
        if (command.ToLower() == BotConfig.IGNORE_COMMAND.ToLower()) return true;
        if (command.ToLower() == "/start")
        {
            await ChatHeandler.SendWireMessage(Constante.StartMessage, message.Chat.Id, InlinesKeybord.StartKeybord);
            _isInit = true;
            return true;
        }

        if (!_isInit)
        {
            await ChatHeandler.SendWireMessage(Constante.BotNotInitMessage, message.Chat.Id);
            return true;
        }

        return false;
    }

    public (Message message , string command) GetMesage(Update update)
    {
        return update.Type switch
        {
            UpdateType.Message => (update.Message , update.Message.Text),
            UpdateType.CallbackQuery => (update.CallbackQuery.Message, update.CallbackQuery.Data),
        };
    }
}